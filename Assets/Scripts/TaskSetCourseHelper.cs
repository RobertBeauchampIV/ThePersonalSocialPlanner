using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskSetCourseHelper : MonoBehaviour
{
    public Text CourseId;
    public Text CourseName;

    public void HelpThisPoorTaskAdd()
    {
        GameObject TaskScreen = GameObject.FindWithTag("TaskAdd");
        TaskScreen.GetComponent<TaskDateHelper>().UpdateTaskSelectedCourse(CourseId, CourseName);
    }
}
