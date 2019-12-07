using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminAddClass : MonoBehaviour
{
    public GameObject appManager;
    public InputField CourseName;
    public InputField InstructorName;
    public InputField MeetDays;


    //Things that should go away on successful sign up
    public GameObject AddCourseMenu;
    public GameObject AddCourseCloseButton;


    private string CreateCourseURL = "54.219.132.101/insertCourse.php";


    public void CreateCourse()
    {
        StartCoroutine(AttemptCreateCourse());
    }


    IEnumerator AttemptCreateCourse()
    {
        string CourseN = CourseName.text;
        string Instruct = InstructorName.text;
        string Meet = MeetDays.text;


        if (NullCheck(CourseN, Instruct, Meet))
        {
            WWWForm form = new WWWForm();
            form.AddField("courseName", CourseN);
            form.AddField("instructor", Instruct);
            form.AddField("meetingDays", Meet);

            WWW www = new WWW(CreateCourseURL, form);
            yield return www;
            string notif = www.text;
            Debug.Log(notif);
            //this is how we return our error codes as drbug lines.
            if (notif == "Course Creation Successful")
            {
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
                AddCourseMenu.GetComponent<SubMenu>().ChangeState();
                AddCourseCloseButton.SetActive(false);
            }
            else
            {
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
                //Course already exists
            }


        }
        else
        {
            Debug.Log("Course Failed");
        }
    }

    private bool NullCheck(string u, string p, string pc)
    {
        if (u == "")
        {
            string notif = "Need to Fill in Course Name";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Course Name Input Field Empty");
            return false;
        }
        if (p == "")
        {
            string notif = "Need to Fill in Instructor Name";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Instructor Name Input Field Empty");
            return false;
        }
        if (pc == "")
        {
            string notif = "Need to Fill in Meet Days";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Meet Days Input Field Empty");
            return false;
        }
        else
        {
            Debug.Log("Passed the Null Check Test");
            return true;
        }
    }
}
