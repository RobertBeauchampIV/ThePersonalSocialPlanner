using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForgotPassword : MonoBehaviour
{
    public GameObject appManager;
    public InputField username;
    public Text UsernameText;
    public Text usernameForChangePW;
    public TextMeshProUGUI SecurtiyQuestionBoxTMP;
    public UnityEngine.Events.UnityEvent ForgotIt;

    private string forgotPasswordURL = "54.219.132.101/forgotPassword.php";

    public void AttemptForgotPassword()
    {
        StartCoroutine(forgotThatPassword());
    }

    private IEnumerator forgotThatPassword()
    {
        string user = username.text;
        if (NullCheck(user))
        {
            WWWForm form = new WWWForm();
            form.AddField("username", user);

            WWW www = new WWW(forgotPasswordURL, form);
            yield return www;
            string notif = www.text;
            Debug.Log(notif);
            //this is how we return our error codes as drbug lines.
            if (notif == "Username Does Not Exist")
            {
                Debug.Log("Username Does Not Exist");
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            }
            else
            {
                Debug.Log("Here is your Recovery Question");
                string success = "Here is your Recovery Question";
                ForgotIt.Invoke();
                UsernameText.text = username.text;
                usernameForChangePW.text = username.text;
                SecurtiyQuestionBoxTMP.SetText(notif);
                appManager.GetComponent<AlertSystem>().CreateAlert(success);
            }


        }
        else
        {
            Debug.Log("Login Failed");
        }
    }

    private bool NullCheck(string u)
    {
        if (u == "")
        {
            string notif = "Need to Fill in Username";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Username Input Field Empty");
            return false;
        }
        else
        {
            Debug.Log("Passed the Null Check Test");
            return true;
        }
    }
}
