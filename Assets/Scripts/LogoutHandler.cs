using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoutHandler : MonoBehaviour
{
	public UnityEngine.Events.UnityEvent Logout;

	//for testing
	public void TestLogout()
	{
		Logout.Invoke();
	}
}
