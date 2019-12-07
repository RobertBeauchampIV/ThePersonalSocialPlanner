using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewMyCourses : MonoBehaviour
{
    public GameObject appManager;
    public GameObject CoursesPrefab;
    public Transform WhereToInstantiate;
    private Text UsernameHolder;

    //ec2-user@54.219.132.101
    public string[] courses;

    public void GetCourses()
    {
        StartCoroutine(GetMyCourses());
    }

    public IEnumerator GetMyCourses()
    {
        GameObject UN = GameObject.FindWithTag("Username");
        UsernameHolder = UN.GetComponent<Text>();
        WWWForm form = new WWWForm();
        string user = UsernameHolder.text.ToString();
        Debug.Log(user);
        form.AddField("username", user);
        WWW CourseData = new WWW("54.219.132.101/enrolledCourses.php",form);
        yield return CourseData;
        string courseStringData = CourseData.text;
        Debug.Log(courseStringData);
        //this is how we return our error codes as drbug lines.
        if (courseStringData == "You are not enrolled in any courses")
        {
            Debug.Log("You are not enrolled in any courses");
            appManager.GetComponent<AlertSystem>().CreateAlert(courseStringData);
        }
        else
        {
            courses = courseStringData.Split(';');
            //if wanting to save as text
            //GetCourseInfo(courses[i], "CourseID")
            MakeAllCourses();
        }
    }

    string GetCourseInfo(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
            value = value.Remove(value.IndexOf("|"));
        return value;
    }

    private void MakeAllCourses()
    {
        Debug.Log(courses.Length);
        for (int i = 0; i < (courses.Length - 1); i++)
        {
            GameObject CourseTag = (GameObject)Instantiate(CoursesPrefab, WhereToInstantiate);
            CourseTag.transform.GetChild(0).GetComponent<Text>().text = GetCourseInfo(courses[i], "CourseId:");
            CourseTag.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(GetCourseInfo(courses[i], "CourseName:"));
            CourseTag.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(GetCourseInfo(courses[i], "CourseInstructor:"));
            CourseTag.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(GetCourseInfo(courses[i], "MeetDays:"));
            int Red1 = int.Parse(GetCourseInfo(courses[i], "Red:"));
            Debug.Log(Red1);
            int Blue1 = int.Parse(GetCourseInfo(courses[i], "Blue:"));
            Debug.Log(Blue1);
            int Green1 = int.Parse(GetCourseInfo(courses[i], "Green:"));
            Debug.Log(Green1);
            CourseTag.transform.GetChild(4).GetComponent<Image>().color = new Color32((byte)Red1, (byte)Blue1, (byte)Green1, 255);
        }
    }

}
