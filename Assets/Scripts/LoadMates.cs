using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadMates : MonoBehaviour
{
    public GameObject NerdPrefab;
    Transform WhereToInstantiate;
    public Text CourseID;

    private string LoadNerdsURL = "54.219.132.101/loadUsersInCourse.php";
    //ec2-user@54.219.132.101
    public string[] Kids;

    public void GetNerds()
    {
        StartCoroutine(GetData());
    }

    public IEnumerator GetData()
    {
        GameObject appManager = GameObject.FindWithTag("AppManager");
        string course = CourseID.text;
        WWWForm form = new WWWForm();
        form.AddField("course_id", course);

        WWW MateData = new WWW(LoadNerdsURL, form);
        yield return MateData;
        string MateStringData = MateData.text;
        if (MateStringData == "No users enrolled in this course")
        {
            appManager.GetComponent<AlertSystem>().CreateAlert("You're the only one enrolled in this course.");
        }
        else
        {
            WhereToInstantiate = GameObject.FindWithTag("SpawnHere").GetComponent<Transform>();
            Debug.Log(MateStringData);
            Kids = MateStringData.Split(';');
            //if wanting to save as text
            //GetCourseInfo(courses[i], "CourseID")
            MakeAllTasks();
        }
    }

    string GetNerdInfo(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
            value = value.Remove(value.IndexOf("|"));
        return value;
    }

    private void MakeAllTasks()
    {
        Debug.Log(Kids.Length);
        for (int i = 0; i < (Kids.Length - 1); i++)
        {
            GameObject TaskTag = (GameObject)Instantiate(NerdPrefab, WhereToInstantiate);
            TaskTag.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(GetNerdInfo(Kids[i], "CourseName:"));
            TaskTag.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(GetNerdInfo(Kids[i], "FirstName:"));
            TaskTag.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(GetNerdInfo(Kids[i], "LastName:"));
            //TaskTag.transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(GetTaskInfo(Tasks[i], "OccurrenceDate:"));
            TaskTag.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(GetNerdInfo(Kids[i], "Username:"));
            TaskTag.transform.GetChild(4).GetComponent<Text>().text = GetNerdInfo(Kids[i], "Username:");
        }
    }
}
