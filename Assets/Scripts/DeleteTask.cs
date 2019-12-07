using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeleteTask : MonoBehaviour
{
    private Text UsernameHolder;
    public Text TaskIdHolder;

    private string removeTaskURL = "54.219.132.101/removeTask.php";

    public void RemoveATask()
    {
        StartCoroutine(AtteptDeleteTask());
    }

    public IEnumerator AtteptDeleteTask()
    {
        GameObject appManager = GameObject.FindWithTag("AppManager");
        GameObject UN = GameObject.FindWithTag("Username");
        UsernameHolder = UN.GetComponent<Text>();
        string user = UsernameHolder.text.ToString();
        //Debug.Log(user);
        string TaskId = TaskIdHolder.text.ToString();
        //Debug.Log(courseId);

        WWWForm form = new WWWForm();
        form.AddField("task_id", TaskId);
        form.AddField("username", user);


        WWW www = new WWW(removeTaskURL, form);
        yield return www;
        string notif = www.text;
        Debug.Log(notif);
        //this is how we return our error codes as debug lines.
        if (notif == "Task Successfully Removed")
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
