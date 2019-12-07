using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TaskDateHelper : MonoBehaviour
{
    public Text TextHelper;
    public TextMeshProUGUI TMPHelper;
    public Text CourseIdHelper;
    public TextMeshProUGUI CourseNameHelper;

    public void SetThatText(string day)
    {
        TextHelper.text = day;
        TMPHelper.SetText(day);
    }

    public void SetThatText2(Text setIt)
    {
        TextHelper.text = setIt.text.ToString();
        TMPHelper.SetText(setIt.text.ToString());
    }

    public void UpdateTaskSelectedCourse(Text id, Text name)
    {
        CourseIdHelper.text = id.text.ToString();
        CourseNameHelper.SetText(name.text.ToString());
    }
}
