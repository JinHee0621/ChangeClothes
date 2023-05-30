using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartStreamManager : MonoBehaviour
{
    public bool nowStream = false;

    public CheckResultManager checkRessultManageer;
    public CharStateManager charStateManager;
    public SelectGameManager selectGameManager;
    public UIMovingManager uiManager;
    public StatusUIManager statUi;

    public GameObject scoreObj;

    public GameObject streamStartBtn;
    public GameObject streamTimeGuage;
    public GameObject streamViewerNum;


    GameObject selectedGame;


    public int test_time;
    private int new_checks = 0;

    GameObject btnText;
    bool isStartStream = false;

    public Text streamTimeSecond;
    public Text streamTimeMinute;
    public Text streamTimeHour;

    private int stream_second = 0;
    private int stream_minute = 0;
    private int stream_hour = 0;

    public void ReadyStream()
    {
        isStartStream = false;
        StreamStateChange(0);
        streamStartBtn.transform.GetChild(0).GetComponent<Button>().interactable = true;
    }


    public void PushStreamStartBtn()
    {
        if(!isStartStream)
        {
            SoundManager.PlaySFX(7);
            uiManager.MoniterOnOff(1);
            btnText = streamStartBtn.transform.GetChild(0).transform.GetChild(0).gameObject;
            StartCoroutine("NowStreamText");
            
            
            GameObject selectGame = selectGameManager.CheckSelectGame();
            streamStartBtn.transform.GetChild(0).GetComponent<Button>().interactable = false;
            //선택 게임에 따른 상황 변화
            CheckStreamTime(selectGame.GetComponent<SelectGameObject>().playTime);

            isStartStream = true;
            StreamStateChange(1);
        } else
        {
            //방송 종료 후 
            SoundManager.PlaySFX(7);
            uiManager.MoniterOnOff(0);

            uiManager.ShowCharStatVal(0, selectedGame.GetComponent<SelectGameObject>().needCondition);
            uiManager.ShowCharStatVal(1, selectedGame.GetComponent<SelectGameObject>().needMental);
            statUi.ChangeGuage(0, selectedGame.GetComponent<SelectGameObject>().needCondition);
            statUi.ChangeGuage(1, selectedGame.GetComponent<SelectGameObject>().needMental);

            btnText = streamStartBtn.transform.GetChild(0).transform.GetChild(0).gameObject;
            btnText.GetComponent<Text>().text = "방송준비";
            streamStartBtn.transform.GetChild(0).GetComponent<Button>().interactable = true;
            StopCoroutine("NowStreamText");
            isStartStream = false;
            StreamStateChange(0);

            if(charStateManager.condition <= 0)
            {
                streamStartBtn.transform.GetChild(0).GetComponent<Button>().interactable = false;
                StreamStateChange(1);
                checkRessultManageer.NextDay();
                charStateManager.NextDay();
            } 
        }
    }

    public void StreamStateChange(int flag)
    {
        if(flag == 1) //방송시작
        {
            nowStream = true;
            selectGameManager.SelectBtnChange(1);
            selectedGame = selectGameManager.GetSelectedGameInfo();
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
        // time guage 초기화
        streamTimeGuage.GetComponent<Image>().fillAmount = 0f;
        StartCoroutine(TimeStatChange(ticks, new_checks));
    }

    IEnumerator TimeStatChange(int ticks, int check)
    {
        if(check >= ticks)
        {
            if(!uiManager.CheckAllScoreActivate())
            {
                // Score Icon All Disable
                PushStreamStartBtn();
                streamTimeGuage.GetComponent<Image>().fillAmount = 100;
                yield return null;
            } else
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(TimeStatChange(ticks, new_checks));
            }
        } else
        {
            new_checks = check + 1;
            stream_second += Random.Range(30,59);
            stream_minute += 1;
            streamTimeGuage.GetComponent<Image>().fillAmount = (float)(check) / ticks;
            ChangeTimeText();
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
    public void ChangeTimeText()
    {
        string hour = "00";
        string minute = "00";
        string second = "00";

        if (stream_second < 10)
        {
            second = "0" + stream_second.ToString();
        }
        else if (stream_second >= 60)
        {
            stream_second = 0;
            second = "00";
        }
        else
        {
            second = stream_second.ToString();
        }

        if (stream_minute < 10)
        {
            minute = "0" + stream_minute.ToString();
        } else if (stream_minute == 60)
        {
            stream_minute = 0;
            minute = "00";
            stream_hour += 1;
        }
        else
        {
            minute = stream_minute.ToString();
        }


        if (stream_hour < 10)
        {
            hour = "0" + stream_hour.ToString();
        } else
        {
            hour = stream_hour.ToString();
        }

        streamTimeSecond.text = second;
        streamTimeMinute.text = minute;
        streamTimeHour.text = hour;
    } 
}
