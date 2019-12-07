using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUpScript : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent StartUp;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine("Example");
    }

    void CalendarFix()
    {
        StartUp.Invoke();
    }


    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(.1f);
        CalendarFix();
        print(Time.time);
    }
}
