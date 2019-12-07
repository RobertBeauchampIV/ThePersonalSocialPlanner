using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecoveryQuestion : MonoBehaviour
{
    public GameObject appManager;
    public InputField SecAnswer;
    public Text username;
    public UnityEngine.Events.UnityEvent Proceed;

    private string recoveryQuestURL = "54.219.132.101/recoveryQuestion.php";

    public void AttemptRecoveryQuestion()
    {
        StartCoroutine(recoverIt());
    }

    private IEnumerator recoverIt()
    {
        string user = username.text;
        string Answer = SecAnswer.text;
        if (NullCheck(Answer))
        {
            WWWForm form = new WWWForm();
            form.AddField("username", user);
            form.AddField("security_ans", Answer);

            WWW www = new WWW(recoveryQuestURL, form);
            yield return www;
            string notif = www.text;
            Debug.Log(notif);
            //this is how we return our error codes as drbug lines.
            if (notif == "Answer Incorrect")
            {
                Debug.Log("Recovery Question Answer is Incorrect");
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            }
            else
            {
                Debug.Log("Recovery Question is Correct");
                Proceed.Invoke();
                SecAnswer.text = "";
                appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            }


        }
        else
        {
            Debug.Log("Login Failed");
        }
    }

    private bool NullCheck(string a)
    {
        if (a == "")
        {
            string notif = "Need to Fill in Answer";
            appManager.GetComponent<AlertSystem>().CreateAlert(notif);
            Debug.Log("Answer Input Field Empty");
            return false;
        }
        else
        {
            Debug.Log("Passed the Null Check Test");
            return true;
        }
    }
}
