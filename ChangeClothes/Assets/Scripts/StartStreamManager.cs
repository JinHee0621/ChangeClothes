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

    public GameObject streamStartBtn;
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
            
            
            GameObject selectGame = selectGameManager.CheckSelectGame();
            
            //선택 게임에 따른 상황 변화
            CheckStreamTime(selectGame.GetComponent<SelectGameObject>().playTime);

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
            if(!uiManager.CheckAllScoreActivate())
            {
                // Score Icon All Disable
                PushStreamStartBtn();
                yield return null;
            } else
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(TimeStatChange(ticks, new_checks));
            }
        } else
        {
            new_checks = check + 1;
            streamTimeGuage.GetComponent<Image>().fillAmount = (float)(check) / ticks;

            yield return new WaitForSeconds(0.05f);
            if (new_checks % 30 == 0)
            {
                AddScoreBasic();
            }
            StartCoroutine(TimeStatChange(ticks, new_checks));
        }
    }
    public void AddScoreBasic()
    {
        uiManager.ScoreAdd(1);
    }

    public void EventOccured()
    {

    }
}
