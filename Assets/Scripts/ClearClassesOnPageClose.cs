using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearClassesOnPageClose : MonoBehaviour
{
    public Transform CoursesContent;

    public void ClearCourses()
    {
        foreach(Transform child in CoursesContent)
        {
            Destroy(child.gameObject);
        }
    }
}
