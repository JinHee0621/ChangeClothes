using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckResultManager : MonoBehaviour
{
    public GameObject uiManager;
    public GameObject cover1;
    public GameObject cover2;
    public void startCheckResult()
    {
        uiManager.GetComponent<UIMovingManager>().RemoveUI();
        StartCoroutine("ScreenClose");
    }
    IEnumerator ScreenClose()
    {
        yield return new WaitForSeconds(1.5f);
        SoundManager.OffBGM();
        cover1.GetComponent<Animator>().SetTrigger("StartCheck");
        cover2.GetComponent<Animator>().SetTrigger("StartCheck");
        SoundManager.PlaySFX(3);
        yield return new WaitForSeconds(1.5f);
        uiManager.GetComponent<UIMovingManager>().OpenGameSetUI();
        uiManager.GetComponent<UIMovingManager>().MoveStatusGuage();
    }
}
