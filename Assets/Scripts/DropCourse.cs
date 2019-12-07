using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DropCourse : MonoBehaviour
{
    private Text UsernameHolder;
    public Text CourseIdHolder;

    private string dropCourseURL = "54.219.132.101/dropCourse.php";

    public void DropACourse()
    {
        StartCoroutine(AtteptDropCourse());
    }

    public IEnumerator AtteptDropCourse()
    {
        GameObject appManager = GameObject.FindWithTag("AppManager");
        GameObject UN = GameObject.FindWithTag("Username");
        UsernameHolder = UN.GetComponent<Text>();
        string user = UsernameHolder.text.ToString();
        Debug.Log(user);
        string courseId = CourseIdHolder.text.ToString();
        Debug.Log(courseId);

        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("courseId", courseId);

        WWW www = new WWW(dropCourseURL, form);
        yield return www;
        string notif = www.text;
        Debug.Log(notif);
        //this is how we return our error codes as debug lines.
        if (notif == "Course Successfully Dropped")
        {
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            yield return new WaitForSeconds(.5f);
            Destroy(this.gameObject);
        }
        else
        {
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            //unsuccessful enroll
        }
    }

}
