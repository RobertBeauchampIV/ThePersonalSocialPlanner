using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangePassword : MonoBehaviour
{
    public GameObject appManager;
    public Text username;
    public InputField password;
    public InputField passwordCheck;
    public UnityEngine.Events.UnityEvent BackToMain;

    private string ChangePasswordURL = "54.219.132.101/changePassword.php";

    public void ChangePasswordPls()
    {
        StartCoroutine(AttemptChangePassword());
    }


    IEnumerator AttemptChangePassword()
    {
        string user = username.text;
        string pass = password.text;
        string passCheck = passwordCheck.text;

        if (NullCheck(pass, passCheck) && PasswordConfimation(pass, passCheck))
        {
            WWWForm form = new WWWForm();
            form.AddField("username", user);
            form.AddField("updated_password", pass);

            WWW www = new WWW(ChangePasswordURL, form);
            yield return www;
            string notif = www.text;
            Debug.Log(notif);
            //this is how we return our error codes as drbug lines.
            if (notif == "Update Successful!")
            {
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
                BackToMain.Invoke();
            }
            else
            {
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
                //an error occured
            }


        }
        else
        {
            Debug.Log("Signup Failed");
        }
    }

    private bool NullCheck(string p, string pc)
    {
        if (p == "")
        {
            string notif = "Need to Fill in Password";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Password Input Field Empty");
            return false;
        }
        if (pc == "")
        {
            string notif = "Need to Fill in Password Confirmation";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Password Check Input Field Empty");
            return false;
        }
        else
        {
            Debug.Log("Passed the Null Check Test");
            return true;
        }
    }

    //checks if password matches
    private bool PasswordConfimation(string p, string pc)
    {
        if (p == pc)
        {
            Debug.Log("Password Confimation Passed");
            return true;
        }
        else
        {
            string notif = "Password & Password Confirmation did not match";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Password Check Failed");
            return false;
        }
    }
}
