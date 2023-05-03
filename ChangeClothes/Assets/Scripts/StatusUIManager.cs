using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUIManager : MonoBehaviour
{
    public GameObject conditionGuage;
    public GameObject conditionText;
    public GameObject conditionNum;
    public GameObject mentalGuage;
    public GameObject mentalText;
    public GameObject mentalNum;
    public GameObject statusSet;

    string[] conditionTextSet = {"매우좋음", "좋음", "보통", "나쁨", "매우나쁨"};
    string[] mentalTextSet = { "매우좋음", "좋음", "보통", "나쁨", "매우나쁨" };

    int charCondition = 0;
    int charMental = 0;
    int guageConditon = 0;
    int guageMental = 0;


    public void ReSetGuage()
    {
        conditionGuage.GetComponent<Image>().fillAmount = 0;
        mentalGuage.GetComponent<Image>().fillAmount = 0;
        charCondition = statusSet.GetComponent<CharStateManager>().condition;
        charMental = statusSet.GetComponent<CharStateManager>().mental;
        StartCoroutine("SetConditionGuageRunning");
        StartCoroutine("SetMentalGuageRunning");
    }

    IEnumerator SetConditionGuageRunning()
    {
        if(guageConditon < charCondition)
        {
            SoundManager.PlayConditionSound();
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

    IEnumerator SetMentalGuageRunning()
    {
        if (guageMental < charMental)
        {
            SoundManager.PlayMentalSound();
            guageMental += 1;
            mentalNum.GetComponent<Text>().text = guageMental.ToString();
            SetGuage(1);
            SetStatusText(1);
            yield return new WaitForSeconds(0.02f);
            StartCoroutine("SetMentalGuageRunning");
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
            mentalGuage.GetComponent<Image>().fillAmount = (float)guageMental / 100f;
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
            if (guageMental > 80)
            {
                mentalText.GetComponent<Text>().text = mentalTextSet[0];
            }
            else if (guageMental > 60 && guageMental <= 80)
            {
                mentalText.GetComponent<Text>().text = mentalTextSet[1];
            }
            else if (guageMental > 60 && guageMental <= 80)
            {
                mentalText.GetComponent<Text>().text = mentalTextSet[1];
            }
            else if (guageMental > 40 && guageMental <= 60)
            {
                mentalText.GetComponent<Text>().text = mentalTextSet[2];
            }
            else if (guageMental > 20 && guageMental <= 40)
            {
                mentalText.GetComponent<Text>().text = mentalTextSet[3];
            }
            else if (guageMental >= 0 && guageMental <= 20)
            {
                mentalText.GetComponent<Text>().text = mentalTextSet[4];
            }
        }
    } 
}
