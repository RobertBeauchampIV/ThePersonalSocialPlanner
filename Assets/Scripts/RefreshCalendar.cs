using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshCalendar : MonoBehaviour
{
	public UnityEngine.Events.UnityEvent RefreshIt;

    // Update is called once per frame
    public void AttemptRefresh()
    {
		StartCoroutine(AttemptRef());
	}

	private IEnumerator AttemptRef()
	{
        yield return new WaitForSecondsRealtime(.5f);
		RefreshIt.Invoke();
	}
}
