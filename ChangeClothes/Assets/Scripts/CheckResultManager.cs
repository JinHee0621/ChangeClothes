using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckResultManager : MonoBehaviour
{
    public StartStreamManager streamManager;
    public GameObject uiManager;
    public GameObject objectBox;
    public GameObject cover1;
    public GameObject cover2;
    GameObject[] patterns;
    private void Start()
    {
        patterns = GameObject.FindGameObjectsWithTag("Pattern");
        foreach (GameObject i in patterns)
        {
            i.SetActive(false);
        }
        uiManager.GetComponent<UIMovingManager>().FadeInCover();
    }

    public void NextDay()
    {
        StartCoroutine(ScreenOpen());
    }

    public void startCheckResult()
    {
        SoundManager.PlaySFX(5);
        uiManager.GetComponent<UIMovingManager>().RemoveUI();
        StartCoroutine(ScreenClose());
        streamManager.ReadyStream();
    }

    IEnumerator ScreenClose()
    {
        yield return new WaitForSeconds(1.5f);
        SoundManager.OffBGM();
        cover1.GetComponent<Animator>().SetTrigger("StartCheck");
        cover2.GetComponent<Animator>().SetTrigger("StartCheck");
        SoundManager.PlaySFX(3);
        yield return new WaitForSeconds(0.2f);
        foreach(GameObject i in patterns)
        {
            i.SetActive(true);
            i.GetComponent<BackGroundPattern>().FadeInPattern();
        }
        yield return new WaitForSeconds(0.5f);
        uiManager.GetComponent<UIMovingManager>().PopUpRankUI();
        yield return new WaitForSeconds(3.5f);
        uiManager.GetComponent<UIMovingManager>().BackRankUI();
        uiManager.GetComponent<UIMovingManager>().OpenGameSetUI();
        yield return new WaitForSeconds(1.5f);
        uiManager.GetComponent<UIMovingManager>().MoveStatusGuage();
    }

    IEnumerator ScreenOpen()
    {
        yield return new WaitForSeconds(2.5f);
        uiManager.GetComponent<UIMovingManager>().ReMoveRankUI();
        uiManager.GetComponent<UIMovingManager>().CloseGameSetUI();
        uiManager.GetComponent<UIMovingManager>().FadeOutCover();
        yield return new WaitForSeconds(3.5f);
        foreach (GameObject i in patterns)
        {
            i.SetActive(false);
        }
        //SoundManager.OffBGM();
        //SoundManager.PlaySFX(3);
        yield return new WaitForSeconds(1f);
        cover1.GetComponent<Animator>().SetTrigger("StartCheck");
        cover2.GetComponent<Animator>().SetTrigger("StartCheck");
        yield return new WaitForSeconds(3f);
        uiManager.GetComponent<UIMovingManager>().FadeInCover();
        yield return new WaitForSeconds(3f);
        uiManager.GetComponent<UIMovingManager>().ResetUI();
        uiManager.GetComponent<UIMovingManager>().MoveRankUI();
        streamManager.StreamStateChange(0);
    }
}
