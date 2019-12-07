using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskNameAndIdPasser : MonoBehaviour
{
    public Text TaskIdOnPrefab;
    public Text TaskTitleOnPrefab;

    public void UpdateTaskIdSelected()
    {
        GameObject TaskShare = GameObject.FindWithTag("TaskShare");
        Text ShareText = GameObject.FindWithTag("TaskId").GetComponent<Text>();
        TextMeshProUGUI ShareName = GameObject.FindWithTag("TaskName").GetComponent<TextMeshProUGUI>();
        ShareText.text = TaskIdOnPrefab.text.ToString();
        ShareName.SetText(TaskTitleOnPrefab.text.ToString());
        TaskShare.GetComponent<SubMenu>().ChangeState();
    }

}
