using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllCourses : MonoBehaviour
{

    public GameObject CoursesPrefab;
    public Transform WhereToInstantiate;

    //ec2-user@54.219.132.101
    public string[] courses;

    public void GetCourses()
    {
        StartCoroutine(GetData());
    }

    public IEnumerator GetData()
    {
        WWW CourseData = new WWW("54.219.132.101/allCourses.php");
        yield return CourseData;
        string courseStringData = CourseData.text;
        Debug.Log(courseStringData);
        courses = courseStringData.Split(';');
        //if wanting to save as text
        //GetCourseInfo(courses[i], "CourseID")
        MakeAllCourses();
    }

    string GetCourseInfo(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if(value.Contains("|"))
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
        }
    }

}
