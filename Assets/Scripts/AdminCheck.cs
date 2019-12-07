using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AdminCheck : MonoBehaviour
{
    private Text check;
    public GameObject appManager;
    public UnityEngine.Events.UnityEvent OpenAddCourses;
    // Start is called before the first frame update
    public void checkIt()
    {
        GameObject UN = GameObject.FindWithTag("UserType");
        check = UN.GetComponent<Text>();
        string userType = check.text.ToString();
        Debug.Log(userType);
        //this is how we return our error codes as drbug lines.
        if (userType == "1")
        {
            Debug.Log("Admin Access");
            OpenAddCourses.Invoke();
            appManager.GetComponent<AlertSystem>().CreateAlert("Access Granted");
        }
        else
        {
            Debug.Log("Normie Denied");
            appManager.GetComponent<AlertSystem>().CreateAlert("Admin Access Only");
        }
    }

}