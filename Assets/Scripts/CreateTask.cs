using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTask : MonoBehaviour
{
    public GameObject appManager;
    public Text username;
    public Text CourseID;
    public InputField TaskTitle;
    public InputField Priority;
    public InputField Details;
    public Text DueDate;
    public GameObject clearReloadTasks;

    //Things that should go away on successful sign up
    public GameObject AddTask;
    public GameObject AddTaskCloseButton;


    private string CreateTaskURL = "54.219.132.101/addTask.php";


    public void CreateTheTask()
    {
        StartCoroutine(AttemptCreateTask());
    }


    IEnumerator AttemptCreateTask()
    {
        string user = username.text;
        string course = CourseID.text;
        string taskName = TaskTitle.text;
        string priority = Priority.text;
        string details = Details.text;
        string dueDate = DueDate.text;


        if (NullCheck(user, course, taskName, priority, details, dueDate))
        {
            WWWForm form = new WWWForm();
            form.AddField("username", user);
            form.AddField("course_id", course);
            form.AddField("title", taskName);
            form.AddField("details", details);
            form.AddField("priority", priority);
            form.AddField("occurrence_date", dueDate);

            WWW www = new WWW(CreateTaskURL, form);
            yield return www;
            string notif = www.text;
            Debug.Log(notif);
            //this is how we return our error codes as drbug lines.
            if (notif == "Successfully Added")
            {
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
                TaskTitle.text = "";
                Priority.text = "";
                Details.text = "";
                clearReloadTasks.GetComponent<ClearClassesOnPageClose>().ClearCourses();
                clearReloadTasks.GetComponent<LoadAllTasksForDay>().GetTasks();
                AddTask.GetComponent<SubMenu>().ChangeState();
                AddTaskCloseButton.SetActive(false);

            }
            else
            {
                //shouldn't occur honestly
                //appManager.GetComponent<AlertSystem>().CreateAlert(notif);
                //Course already exists
            }


        }
        else
        {
            Debug.Log("Course Failed");
        }
    }

    private bool NullCheck(string u, string c, string t, string p, string d, string dd)
    {
        if (u == "")
        {
            string notif = "Username is Blank";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("U:This should not happen");
            return false;
        }
        if (c == "")
        {
            string notif = "Course Id is Blank";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("C:IThis should not happen");
            return false;
        }
        if (t == "")
        {
            string notif = "Need to Fill in Task Name";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Task Name Is Empty");
            return false;
        }
        if (p == "")
        {
            string notif = "Need to Put a numeber in priority";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Prioirty is Empty");
            return false;
        }
        if (p == "0")
        {
            string notif = "Priority Cannot be Zero";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Priority cannot be 0");
            return false;
        }
        if (d == "")
        {
            string notif = "Need To Fill in Some Details";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Details cannot be empty");
            return false;
        }
        if (dd == "")
        {
            string notif = "Due Date was empty";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("dd: should not occur");
            return false;
        }
        else
        {
            Debug.Log("Passed the Null Check Test");
            return true;
        }
    }
}
