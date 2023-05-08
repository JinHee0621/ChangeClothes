using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckResultManager : MonoBehaviour
{
    public GameObject uiManager;
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
    }
    public void startCheckResult()
    {
        SoundManager.PlaySFX(5);
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
        yield return new WaitForSeconds(0.2f);
        foreach(GameObject i in patterns)
        {
            i.SetActive(true);
            i.GetComponent<BackGroundPattern>().FadeInPattern();
        }
        yield return new WaitForSeconds(1.5f);
        uiManager.GetComponent<UIMovingManager>().OpenGameSetUI();
        yield return new WaitForSeconds(1.5f);
        uiManager.GetComponent<UIMovingManager>().MoveStatusGuage();
    }
}
