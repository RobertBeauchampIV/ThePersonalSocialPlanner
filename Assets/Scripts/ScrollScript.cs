using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 3; i++)
        {
            GenerateItem();
        }

        //foreach (Transform child in scrollContent.transform)
        //{
        //    child.Find("CalendarScriptHolder").GetComponent<CalendarPopup>().NextMonth();
        //}
        scrollView.verticalNormalizedPosition = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateItem()
    {
        GameObject scrollItemObj = Instantiate(scrollItemPrefab, scrollContent.transform);

        //scrollItemObj.transform.Find("CalendarScriptHolder").GetComponent<CalendarPopup>().PreviousMonth();
    }
}
