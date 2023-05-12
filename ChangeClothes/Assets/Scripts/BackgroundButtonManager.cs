using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundButtonManager : MonoBehaviour
{
    public GameObject button;
    public Sprite common_button_img;
    public Sprite hovered_button_img;
    public GameObject button_target_obj;

    private bool enteredMouse = false;
    private bool openedMenu = false;

    private bool moving = false;
    private void OnMouseEnter()
    {
        enteredMouse = true;
        button.GetComponent<SpriteRenderer>().sprite = hovered_button_img;
    }

    private void OnMouseExit()
    {
        enteredMouse = false;
        button.GetComponent<SpriteRenderer>().sprite = common_button_img;
    }

    private void OnMouseDown()
    {
        if (enteredMouse && !moving)
        {
            if (!openedMenu)
            {
                StartCoroutine("Moving");
                OpenMenu();
                moving = true;
                openedMenu = true;
            }
            else
            {
                StartCoroutine("Moving");
                CloseMenu();
                moving = true;
                openedMenu = false;
            }
        }
    }

    public void CloseMenuPub()
    {
        if(openedMenu)
        {
            StartCoroutine("Moving");
            CloseMenu();
            moving = true;
            openedMenu = false;
        }
    }

    private void OpenMenu()
    {
        SoundManager.PlaySFX(9);
        button_target_obj.transform.DOMoveY(button_target_obj.transform.localPosition.y + 5, 1.5f);
    }

    private void CloseMenu()
    {
        SoundManager.PlaySFX(9);
        button_target_obj.transform.DOMoveY(button_target_obj.transform.localPosition.y - 5, 1.5f);
    }

    IEnumerator Moving()
    {
        yield return new WaitForSeconds(1.5f);
        moving = false;
    }
}
