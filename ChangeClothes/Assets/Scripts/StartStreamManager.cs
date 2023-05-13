using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartStreamManager : MonoBehaviour
{
    public bool nowStream = false;

    public CharStateManager charStateManager;
    public SelectGameManager selectGameManager;
    public UIMovingManager uiManager;
    public StatusUIManager statUi;

    public GameObject scoreObj;
    public GameObject scoreObjEle;

    public GameObject streamStartBtn;
    public GameObject streamTimeGuage;
    public GameObject streamTimeNum;
    public GameObject streamViewerNum;

    public int test_time;
    private int new_checks = 0;

    GameObject btnText;
    bool isStartStream = false;

    public void ScoreEffect()
    {
        uiManager.ScoreAdd(5);
    }

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
            StreamStateChange(1);
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
            StreamStateChange(0);
        }
    }

    public void StreamStateChange(int flag)
    {
        if(flag == 1) //방송시작
        {
            nowStream = true;
            ScoreEffect();
            selectGameManager.SelectBtnChange(1);
            GameObject selectedGame = selectGameManager.GetSelectedGameInfo();
            Debug.Log(selectedGame.GetComponent<SelectGameObject>().gameName);
        }else
        {
            nowStream = false;
            selectGameManager.SelectBtnChange(0);
        }

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
