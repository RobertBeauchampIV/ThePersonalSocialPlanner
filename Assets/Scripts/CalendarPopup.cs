using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections;
using System.Globalization;

public class CalendarPopup : MonoBehaviour
{

    public GameObject[] dayLabels;         //Holds 42 labels
    public string[] months;             //Holds the months
    public TextMeshProUGUI headerLabel;         //The label used to show the Month
    public GameObject dayLabel;
    public GameObject calendar;
    [SerializeField]
    private int monthCounter; // = DateTime.Now.Month - 1;
    [SerializeField]
    private int yearCounter = 0;

    private string taskCalendarURL = "54.219.132.101/loadTaskCalendar.php";

    [SerializeField]
    private DateTime iMonth;
    [SerializeField]
    private DateTime curDisplay;
    private static DateTime numberOfToday;
    //counter for skip days
    private static int CforSD = 0;

    void Start()
    {
        CreateLabels();
        //Debug.Log("Monthcounter = " + monthCounter);
        CreateMonths();
        CreateCalendar();
        monthCounter = DateTime.Now.Month - 1;
        numberOfToday = (DateTime.Today);
        //Debug.Log(numberOfToday);
    }

    /*Adds all the months to the Months Array and sets the current month
    in the header label*/
    void CreateMonths()
    {
        months = new string[12];
        iMonth = new DateTime(2000, 1, 1);

        for (int i = 0; i < 12; ++i)
        {
            iMonth = new DateTime(DateTime.Now.Year, i + 1, 1);
            months[i] = iMonth.ToString("MMMM");
        }
        iMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // The magical Line :)
        headerLabel.SetText(months[DateTime.Now.Month - 1] + " " + DateTime.Now.Year);
    }

    /*Sets the days to their correct labels*/
    public void CreateCalendar()
    {
        int test = dow(iMonth.Year, iMonth.Month, iMonth.Day);
        //Debug.Log(test);
        curDisplay = iMonth;
        //Debug.Log(iMonth);

        while (curDisplay.Month+test == iMonth.Month+test)
        {
            //Debug.Log(test);
            if (test > CforSD)
            {
                //Debug.Log("entered");
                dayLabels[CforSD].SetActive(true);
                dayLabels[CforSD].GetComponent<Button>().interactable = false;
                CforSD++;
            }
            else
            {
                dayLabels[curDisplay.Day + test - 1].SetActive(true);
                dayLabels[curDisplay.Day + test - 1].GetComponent<Button>().interactable = true;
                dayLabels[curDisplay.Day + test - 1].GetComponentInChildren<TextMeshProUGUI>().SetText(curDisplay.Day.ToString());
                dayLabels[curDisplay.Day + test - 1].GetComponent<DayTasks>().AddTheDate(curDisplay.ToString());
                int holder = curDisplay.Day;
                LoadTaskNum(curDisplay.ToString(), holder, test);
                //Debug.Log(curDisplay);
                //Debug.Log(numberOfToday);
                if (curDisplay == numberOfToday)
                {
                    TextMeshProUGUI color = dayLabels[curDisplay.Day + test - 1].GetComponentInChildren<TextMeshProUGUI>();
                    color.faceColor = new Color32(255, 0, 0, 255);
                }
                curDisplay = curDisplay.AddDays(1);
                //Debug.Log(curDisplay);
            }
        }
    }

    /// <summary>
    /// Instantiates the day labels and then deactivates them.
    /// </summary>
    void CreateLabels()
    {
        dayLabels = new GameObject[42];
        for(int i = 0; i < 42; i++)
        {
            //Debug.Log(i);
            dayLabels[i] = Instantiate(dayLabel, calendar.transform);
            dayLabels[i].GetComponent<Button>().interactable = false;
            dayLabels[i].SetActive(true);
        }

    }


    /*when right arrow clicked go to next month */
    public void NextMonth()
    {
        monthCounter++;
        if (monthCounter > 11)
        {
            monthCounter = 0;
            yearCounter++;
        }

        headerLabel.SetText(months[monthCounter] + " " + (DateTime.Now.Year + yearCounter));
        ClearLabels();
        iMonth = iMonth.AddMonths(1);
        CreateCalendar();
    }

    /*when left arrow clicked go to previous month */
    public void PreviousMonth()
    {
        monthCounter--;
        if (monthCounter < 0)
        {
            monthCounter = 11;
            yearCounter--;
        }

        headerLabel.SetText(months[monthCounter] + " " + (DateTime.Now.Year + yearCounter));
        ClearLabels();
        iMonth = iMonth.AddMonths(-1);
        CreateCalendar();
    }

    //determins start day of month
    int dow(int y, int m, int d)
    {
        int[] t = { 0, 3, 2, 5, 0, 3, 5, 1, 4, 6, 2, 4 };
        if (m < 3)
            y -= 1;
        else
        { //do nothing
        }
        return (y + y / 4 - y / 100 + y / 400 + t[m - 1] + d) % 7;
    }

    /*clears all the day labels*/
    void ClearLabels()
    {
        for (int x = 0; x < dayLabels.Length; x++)
        {
            dayLabels[x].GetComponentInChildren<TextMeshProUGUI>().SetText(" ");
            TextMeshProUGUI color = dayLabels[x].GetComponentInChildren<TextMeshProUGUI>();
            color.faceColor = new Color32(0, 0, 0, 255);
            dayLabels[x].GetComponent<DayTasks>().AddTheDate("NULL");
            dayLabels[x].GetComponent<DayTasks>().AddTheTaskNumber("");
            dayLabels[x].GetComponent<Button>().interactable = false;
        }
    }

    public void LoadTaskNum(string x, int day, int test)
    {
        StartCoroutine(AttemptLoadTaskNum(x, day, test));
    }

    private IEnumerator AttemptLoadTaskNum(string x,int day, int test)
    {
        Text username = GameObject.FindWithTag("Username").GetComponent<Text>();
        string user = username.text;
        //Debug.Log("." + x + ".");
        string date = x;
        if (NullCheck(user, x))
        {
            WWWForm form = new WWWForm();
            form.AddField("username", user);
            form.AddField("date", date);

            WWW www = new WWW(taskCalendarURL, form);
            yield return www;
            string notif = www.text;
            Debug.Log(notif);

            //this is how we return our error codes as debug lines.
            dayLabels[day + test - 1].GetComponent<DayTasks>().AddTheTaskNumber(notif);

        }
        else
        {
            //Debug.Log("Load Task Failed");
        }
    }

    private bool NullCheck(string u, string p)
    {
        if (u == "")
        {
            //string notif = "Need to Fill in Username";
            //appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            //Debug.Log("UsernameEmpty");
            return false;
        }
        if (p == "")
        {
            //string notif = "Need to Fill in Password";
            //appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            //Debug.Log("DateEmpty");
            return false;
        }
        else
        {
            //Debug.Log("Passed the Null Check Test");
            return true;
        }
    }
}
