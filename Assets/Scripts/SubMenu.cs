using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenu : MonoBehaviour
{
	private bool isOpen = false;
    [SerializeField]
    private GameObject ClickAnywhereToClose = null; //used for button thats invisable to make it more natural.
    public Animator animator;


    public void ChangeState()
	{
        if(isOpen == false)
		{
			OpenSubMenu();
            ClickAnywhereToClose.SetActive(true);
			isOpen = true;
		}
        else
		{
			CloseSubMenu();
            ClickAnywhereToClose.SetActive(false);
            isOpen = false;
		}

	}

    void OpenSubMenu()
    {
		animator.SetBool("IsOpen", true);
	}

    void CloseSubMenu()
    {
		animator.SetBool("IsOpen", false);
	}
}
