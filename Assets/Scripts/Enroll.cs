using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enroll : MonoBehaviour
{
    private Text RedText;
    private Text GreenText;
    private Text BlueText;
    private Text UsernameHolder;
    public Text CourseIdHolder;

    private string enrollURL = "54.219.132.101/enroll.php";

    public void EnrollInClass()
    {
        StartCoroutine(EnrollCourse());
    }

    public IEnumerator EnrollCourse()
    {
        GameObject appManager = GameObject.FindWithTag("AppManager");
        GameObject R = GameObject.FindWithTag("Red1");
        GameObject G = GameObject.FindWithTag("Green1");
        GameObject B = GameObject.FindWithTag("Blue1");
        GameObject UN = GameObject.FindWithTag("Username");
        RedText = R.GetComponent<Text>();
        GreenText = G.GetComponent<Text>();
        BlueText = B.GetComponent<Text>();
        UsernameHolder = UN.GetComponent<Text>();
        string user = UsernameHolder.text.ToString();
        Debug.Log(user);
        string courseId = CourseIdHolder.text.ToString();
        Debug.Log(courseId);
        string red = RedText.text.ToString();
        Debug.Log(red);
        string blue = BlueText.text.ToString();
        Debug.Log(blue);
        string green = GreenText.text.ToString();
        Debug.Log(green);

        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("courseId", courseId);
        form.AddField("red", red);
        form.AddField("blue", blue);
        form.AddField("green", green);


        WWW www = new WWW(enrollURL, form);
        yield return www;
        string notif = www.text;
        Debug.Log(notif);
        //this is how we return our error codes as debug lines.
        if (notif == "User Successfully Enrolled")
        {
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
        }
        else
        {
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            //unsuccessful enroll
        }
    }

}
