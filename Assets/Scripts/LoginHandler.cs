using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginHandler : MonoBehaviour
{
    public GameObject appManager;
    public Text studentusername;
    public InputField username;
    public InputField password;
    public Text userType;
    private string LoginURL = "54.219.132.101/login.php";
    public UnityEngine.Events.UnityEvent Login;

    /*
    //for testing
    public void TestLogin()
    {
        string test1 = username.text;
        string test2 = password.text;
        if (test1 == "test" && test2 == "test")
        {
            Debug.Log("Login Success");
            Login.Invoke();
            studentusername.SetText(test1);
        }
    }
    */

    public void LoginIntoAccount()
    {
        StartCoroutine(AttemptLogin());
    }

    private IEnumerator AttemptLogin()
    {
        string user = username.text;
        string pass = password.text;
        if(NullCheck(user, pass))
        {
            WWWForm form = new WWWForm();
            form.AddField("username", user);
            form.AddField("password", pass);

            WWW www = new WWW(LoginURL, form);
            yield return www;
            string notif = www.text;
            Debug.Log(notif);
            //this is how we return our error codes as drbug lines.
            if (notif == "Admin Login Success")
            {
                Debug.Log("Admin Login Success");
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
                Login.Invoke();
				username.text = "";
				password.text = "";
				studentusername.text = user;
                userType.text = "1";
            }
            else if (notif == "Login Success")
            {
                Debug.Log("Login Success");
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
                Login.Invoke();
				username.text = "";
				password.text = "";
                studentusername.text = user;
                userType.text = "0";
            }
            else
            {
                Debug.Log("Login Failed: Wrong Password");
                string failed = "Login Failed";
                appManager.GetComponent<AlertSystem>().CreateAlert(failed);
            }


        }
        else
        {
            Debug.Log("Login Failed");
        }
    }

    private bool NullCheck(string u, string p)
    {
        if (u == "")
        {
            string notif = "Need to Fill in Username";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Username Input Field Empty");
            return false;
        }
        if (p == "")
        {
            string notif = "Need to Fill in Password";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Password Input Field Empty");
            return false;
        }
        else
        {
            Debug.Log("Passed the Null Check Test");
            return true;
        }
    }
}