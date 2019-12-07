using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadAllTasksForDay : MonoBehaviour
{
    public GameObject TaskPrefab;
    public Transform WhereToInstantiate;
    public Text Username;
    public Text Date;
    private string LoadTasksURL = "54.219.132.101/loadTask.php";
    //ec2-user@54.219.132.101
    public string[] Tasks;

    public void GetTasks()
    {
        StartCoroutine(GetData());
    }

    public IEnumerator GetData()
    {
        string user = Username.text;
        string date = Date.text;
        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("date", date);
        WWW TaskData = new WWW(LoadTasksURL, form);
        yield return TaskData;
        string courseStringData = TaskData.text;
        Debug.Log(courseStringData);
        Tasks = courseStringData.Split(';');
        //if wanting to save as text
        //GetCourseInfo(courses[i], "CourseID")
        MakeAllTasks();
    }

    string GetTaskInfo(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
            value = value.Remove(value.IndexOf("|"));
        return value;
    }

    private void MakeAllTasks()
    {
        Debug.Log(Tasks.Length);
        for (int i = 0; i < (Tasks.Length - 1); i++)
        {
            GameObject TaskTag = (GameObject)Instantiate(TaskPrefab, WhereToInstantiate);
            TaskTag.transform.GetChild(0).GetComponent<Text>().text = GetTaskInfo(Tasks[i], "TaskId:");
            TaskTag.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(GetTaskInfo(Tasks[i], "Title:"));
            TaskTag.transform.GetChild(2).GetComponent<Text>().text = GetTaskInfo(Tasks[i], "Title:");
            TaskTag.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(GetTaskInfo(Tasks[i], "Priority:"));
            TaskTag.transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(GetTaskInfo(Tasks[i], "Details:"));
            //TaskTag.transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(GetTaskInfo(Tasks[i], "OccurrenceDate:"));
            TaskTag.transform.GetChild(5).GetComponent<TextMeshProUGUI>().SetText(GetTaskInfo(Tasks[i], "CourseName:"));
            TaskTag.transform.GetChild(6).GetComponent<Text>().text = GetTaskInfo(Tasks[i], "CourseId:");
        }
    }
}
