using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlertSystem : MonoBehaviour
{

    private int fadeTime = 2;
    public GameObject visualAchievement;
    public Transform notificationCanvas;

    public void CreateAlert(string alertMessage)
    {
        GameObject achievement = (GameObject)Instantiate(visualAchievement,notificationCanvas);
        achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(alertMessage);
        StartCoroutine(FadeAchievement(achievement));
    }

    private IEnumerator FadeAchievement(GameObject achievement)
    {
        CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup>();

        float rate = 1.0f / fadeTime;

        int startAlpha = 0;
        int endAlpha = 1;

        for (int i = 0; i < 2; i++)
        {
            float progress = 0.0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }

            yield return new WaitForSeconds(2);
            startAlpha = 1;
            endAlpha = 0;
        }
        Destroy(achievement);
    }
}
