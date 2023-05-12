using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartStreamManager : MonoBehaviour
{
    public GameObject streamStartBtn;
    public CharStateManager charStateManager;
    public SelectGameManager selectGameManager;
    public StatusUIManager statUi;
    public UIMovingManager uiManager;
    public GameObject streamTimeGuage;
    public GameObject streamTimeNum;
    public GameObject streamViewerNum;

    public int test_time;
    private int new_checks = 0;

    GameObject btnText;
    bool isStartStream = false;

    public void PushStreamStartBtn()
    {
        if(!isStartStream)
        {
            SoundManager.PlaySFX(7);
            uiManager.MoniterOnOff(1);
            btnText = streamStartBtn.transform.GetChild(0).transform.GetChild(0).gameObject;
            StartCoroutine("NowStreamText");
            CheckStreamTime(test_time);
            isStartStream = true;
            StreamStart();
        } else
        {
            //방송 종료 후 
            SoundManager.PlaySFX(7);
            uiManager.MoniterOnOff(0);
            btnText = streamStartBtn.transform.GetChild(0).transform.GetChild(0).gameObject;
            btnText.GetComponent<Text>().text = "방송준비";
            streamStartBtn.transform.GetChild(0).GetComponent<Button>().interactable = true;
            StopCoroutine("NowStreamText");
            isStartStream = false;
        }
    }

    public void StreamStart()
    {
        GameObject selectedGame = selectGameManager.GetSelectedGameInfo();
        Debug.Log(selectedGame.GetComponent<SelectGameObject>().gameName);
    }


    IEnumerator NowStreamText()
    {
        btnText.GetComponent<Text>().text = "▷";
        yield return new WaitForSeconds(1f);
        btnText.GetComponent<Text>().text = "▷▷";
        yield return new WaitForSeconds(1f);
        btnText.GetComponent<Text>().text = "▷▷▷";
        yield return new WaitForSeconds(1f);
        StartCoroutine("NowStreamText");
    }
    public void CheckStreamTime(int ticks)
    {
        new_checks = 0;
        streamTimeGuage.GetComponent<Image>().fillAmount = 0f;
        StartCoroutine(TimeStatChange(ticks, new_checks));
    }

    IEnumerator TimeStatChange(int ticks, int check)
    {
        if(check > ticks)
        {
            yield return null;
        } else
        {
            new_checks = check + 1;
            streamTimeGuage.GetComponent<Image>().fillAmount = (float)(check) / ticks;
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(TimeStatChange(ticks, new_checks));
        }
    }
}
