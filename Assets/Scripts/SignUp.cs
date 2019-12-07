using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUp : MonoBehaviour
{
    public GameObject appManager;
	public InputField username;
	public InputField password;
    public InputField passwordCheck;
    public InputField firstName;
    public InputField lastName;
    public InputField year;
    public InputField securityQuestion;
    public InputField securityQuestionAnswer;

    //Things that should go away on successful sign up
    public GameObject signUpMenu;
    public GameObject signUpCloseButton;


    private string CreateUserURL = "54.219.132.101/insertUser.php";
    //csusm is defaulted to 1
    //not going to have other universities for this project
    //private int schoolID = 1;
    //admins 1 normies 0
    //private int userType = 0;

    public void CreateUser()
    {
        StartCoroutine(AttemptCreateUser());
    }


    IEnumerator AttemptCreateUser()
    {
        string user = username.text;
        string pass = password.text;
        string passCheck = passwordCheck.text;
        string fname = firstName.text;
        string lname = lastName.text;
        string ear = year.text;
        string secQ = securityQuestion.text;
        string secQA = securityQuestionAnswer.text;

        if (NullCheck(user, pass, passCheck, fname, lname, ear, secQ, secQA) && PasswordConfimation(pass, passCheck))
        {
            WWWForm form = new WWWForm();
            form.AddField("username", user);
            form.AddField("password", pass);
            form.AddField("firstname", fname);
            form.AddField("lastname", lname);
            form.AddField("year", ear);
            form.AddField("securityQuestion", secQ);
            form.AddField("securityQuestionAnswer", secQA);

            WWW www = new WWW(CreateUserURL, form);
            yield return www;
            string notif = www.text;
            Debug.Log(notif);
            //this is how we return our error codes as drbug lines.
            if (notif == "User Creation Successful")
            {
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
                signUpMenu.GetComponent<SubMenu>().ChangeState();
                signUpCloseButton.SetActive(false);
            }
            else
            {
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
                //username already exists
            }


        }
        else
        {
            Debug.Log("Signup Failed");
        }
    }

    private bool NullCheck(string u, string p, string pc, string fn, string ln, string y, string sq, string sqa)
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
        if (pc == "")
        {
            string notif = "Need to Fill in Password Confirmation";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Password Check Input Field Empty");
            return false;
        }
        if (fn == "")
        {
            string notif = "Need to Fill in First Name";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("First Name Input Field Empty");
            return false;
        }
        if (ln == "")
        {
            string notif = "Need to Fill in Last Name";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Last Name Input Field Empty");
            return false;
        }
        if (y == "")
        {
            string notif = "Need to Fill in Year";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Year Input Field Empty");
            return false;
        }
        if (sq == "")
        {
            string notif = "Need to Fill in Security Question";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Security Question Input Field Empty");
            return false;
        }
        if (sqa == "")
        {
            string notif = "Need to Fill in Security Question Answer";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Security Question Answer Input Field Empty");
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
