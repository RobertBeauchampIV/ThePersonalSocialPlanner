using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayTasks : MonoBehaviour
{

    public Text DayHolder;
    public TextMeshProUGUI NumOfTasks;

    public void ChangeThatState()
    {
        GameObject TaskScreen = GameObject.FindWithTag("TaskScreen");
        TaskScreen.GetComponent<TaskDateHelper>().SetThatText(DayHolder.text.ToString());
        TaskScreen.GetComponent<LoadAllTasksForDay>().GetTasks();
        TaskScreen.GetComponent<SubMenu>().ChangeState();
    }

    public void AddTheDate(string day)
    {
        DayHolder.text = day;
    }

    public void AddTheTaskNumber(string num)
    {
        NumOfTasks.SetText(num);
    }
}
