using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUIManager : MonoBehaviour
{
    public GameObject conditionGuage;
    public GameObject conditionText;
    public GameObject conditionNum;
    public GameObject tensionGuage;
    public GameObject tensionText;
    public GameObject tensionNum;
    public GameObject statusSet;

    string[] conditionTextSet = {"매우좋음", "좋음", "보통", "나쁨", "매우나쁨"};
    string[] testionTextSet = { "하이텐션", "보통텐션", "로우텐션" };

    int charCondition = 0;
    int charTension = 0;
    int guageConditon = 0;
    int guageTension = 0;


    public void ReSetGuage()
    {
        conditionGuage.GetComponent<Image>().fillAmount = 0;
        tensionGuage.GetComponent<Image>().fillAmount = 0;
        charCondition = statusSet.GetComponent<CharStateManager>().condition;
        charTension = statusSet.GetComponent<CharStateManager>().tension;
        StartCoroutine("SetConditionGuageRunning");
        StartCoroutine("SetTensionGuageRunning");
    }

    IEnumerator SetConditionGuageRunning()
    {
        if(guageConditon < charCondition)
        {
            guageConditon += 1;
            conditionNum.GetComponent<Text>().text = guageConditon.ToString();
            SetGuage(0);
            SetStatusText(0);
            yield return new WaitForSeconds(0.015f);
            StartCoroutine("SetConditionGuageRunning");
        } else
        {
            yield return null;
        }
    }

    IEnumerator SetTensionGuageRunning()
    {
        if (guageTension < charTension)
        {
            guageTension += 1;
            tensionNum.GetComponent<Text>().text = guageTension.ToString();
            SetGuage(1);
            SetStatusText(1);
            yield return new WaitForSeconds(0.02f);
            StartCoroutine("SetTensionGuageRunning");
        }
        else
        {
            yield return null;
        }
    }

    public void SetGuage(int target)
    {
        if(target == 0)
        {
            conditionGuage.GetComponent<Image>().fillAmount = (float)guageConditon / 100f;
        } else
        {
            tensionGuage.GetComponent<Image>().fillAmount = (float)guageTension / 100f;
        }
    }


    public void SetStatusText(int target)
    {
        if(target == 0)
        {
            if(guageConditon > 80)
            {
                conditionText.GetComponent<Text>().text = conditionTextSet[0];
            } 
            else if(guageConditon > 60 && guageConditon <= 80)
            {
                conditionText.GetComponent<Text>().text = conditionTextSet[1];
            } 
            else if (guageConditon > 60 && guageConditon <= 80)
            {
                conditionText.GetComponent<Text>().text = conditionTextSet[1];
            } 
            else if (guageConditon > 40 && guageConditon <= 60)
            {
                conditionText.GetComponent<Text>().text = conditionTextSet[2];
            }
            else if (guageConditon > 20 && guageConditon <= 40)
            {
                conditionText.GetComponent<Text>().text = conditionTextSet[3];
            }
            else if (guageConditon >= 0 && guageConditon <= 20)
            {
                conditionText.GetComponent<Text>().text = conditionTextSet[4];
            }
        } else
        {
            if (guageTension > 75)
            {
                tensionText.GetComponent<Text>().text = testionTextSet[0];
            }
            else if (guageTension > 35 && guageTension <= 75)
            {
                tensionText.GetComponent<Text>().text = testionTextSet[1];
            }
            else if (guageTension > 0 && guageTension <= 35)
            {
                tensionText.GetComponent<Text>().text = testionTextSet[2];
            }
        }
    } 
}
