using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImJustGonnaSendIt : MonoBehaviour
{
    GameObject appManager;
    public Text studentUsername;
    private string SendItURL = "54.219.132.101/sendYourTask.php";
    //public UnityEngine.Events.UnityEvent Login;

    /*
    //for testing
    public void TestLogin()
    {
        string test1 = username.text;
        string test2 = password.text;
        if (test1 == "test" && test2 == "test")
        {
            Debug.Log("Login Success");
            Login.Invoke();
            studentusername.SetText(test1);
        }
    }
    */
    public void Start()
    {
        
    }

    public void ShareTask()
    {
        StartCoroutine(AttemptShareTask());
    }

    private IEnumerator AttemptShareTask()
    {
        appManager = GameObject.FindWithTag("AppManager");
        Text sendersUsername = GameObject.FindWithTag("Username").GetComponent<Text>();
        Text taskID = GameObject.FindWithTag("TaskId").GetComponent<Text>();
        string task = taskID.text;
        string user = studentUsername.text;
        string sender = sendersUsername.text;
        if (NullCheck(user, task, sender))
        {
            WWWForm form = new WWWForm();
            form.AddField("task_id", task);
            form.AddField("username", user);


            WWW www = new WWW(SendItURL, form);
            yield return www;
            string notif = www.text;
            Debug.Log(notif);
            //this is how we return our error codes as drbug lines.
            if (notif == "Task has already been shared!")
            {
                Debug.Log("Task has already been shared!");
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            }
            else
            {
                Debug.Log("Task has been successfully shared");
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            }


        }
        else
        {
            Debug.Log("Tried to send to self");
        }
    }

    private bool NullCheck(string u, string p, string s)
    {
        if (u == "")
        {
            string notif = "Need to Fill in Username";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Should Not Occur");
            return false;
        }
        else if (p == "")
        {
            string notif = "Need to Fill in Task Id";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Should Not Occur");
            return false;
        }
        else if (s == u)
        {
            string notif = "You cannot send a task to yourself";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Silly user tricks are for kids");
            return false;
        }
        else
        {
            Debug.Log("Passed the Null Check Test");
            return true;
        }
    }
}
