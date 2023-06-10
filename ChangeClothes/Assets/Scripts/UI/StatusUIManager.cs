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

    public GameObject clothRankImg;
    public Text clothRankTxt;
    int currentRank = 0;

    public CharStateManager statusSet;

    string[] conditionTextSet = {"매우좋음", "좋음", "보통", "나쁨", "매우나쁨"};
    string[] mentalTextSet = { "매우좋음", "좋음", "보통", "나쁨", "매우나쁨" };

    int charCondition = 0;
    int charMental = 0;
    int guageConditon = 0;
    int guageMental = 0;
    int changeValue = 0;

    public void ChangeClothRankGuage()
    {
        int changeRank = 0;
        
        if(statusSet.clothRank != 0)
        {
            StartCoroutine(UpdateRank(changeRank, statusSet.clothRank - currentRank));
            currentRank = statusSet.clothRank;
        } else if(statusSet.clothRank == 0 && currentRank != 0)
        {
            StartCoroutine(UpdateRank(changeRank, statusSet.clothRank - currentRank));
            currentRank = statusSet.clothRank;
        }
    }

    IEnumerator UpdateRank(int temp, int next)
    {
        if(temp != next)
        {
            if (0 < next)
            {
                clothRankImg.GetComponent<Image>().fillAmount += 0.01f;
                temp += 1;
            } else
            {
                clothRankImg.GetComponent<Image>().fillAmount -= 0.01f;
                temp -= 1;
            }
            clothRankTxt.text = ((int)currentRank / 10).ToString();
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(UpdateRank(temp, next));
        } else
        {
            yield return null;
        }
    }

    public void ReSetGuage()
    {
        conditionGuage.GetComponent<Image>().fillAmount = 0;
        mentalGuage.GetComponent<Image>().fillAmount = 0;
        statusSet.RandomSetState();
        charCondition = statusSet.condition;
        charMental = statusSet.mental;
        StartCoroutine(SetConditionGuageRunning());
        StartCoroutine(SetMentalGuageRunning());
    }

    public void ChangeGuage(int target, int val)
    {
        val *= -1;
        statusSet.changeState(target, val);
        if (target == 0)
        {
            charCondition = statusSet.condition;
            changeValue = charCondition + val;
        } else if(target == 1)
        {
            charMental = statusSet.mental;
            changeValue = charMental + val;
        }
        StartCoroutine(StatusGuageChanging(target, val));
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



    IEnumerator StatusGuageChanging(int target, int val)
    {
        if(target == 0)
        {
            if(val < 0)
            {
                SoundManager.PlayConditionSound();
                if(guageConditon > 0)
                {
                    guageConditon -= 1;
                } else
                {
                    guageConditon = 0;
                }
                conditionNum.GetComponent<Text>().text = guageConditon.ToString();
                SetGuage(0);
                SetStatusText(0);
                yield return new WaitForSeconds(0.015f);
                val += 1;
                StartCoroutine(StatusGuageChanging(0,val));
            } else if(val > 0)
            {
                SoundManager.PlayConditionSound();
                if(guageConditon < 100)
                {
                    guageConditon += 1;
                } else
                {
                    guageConditon = 100;
                }
                conditionNum.GetComponent<Text>().text = guageConditon.ToString();

                SetGuage(0);
                SetStatusText(0);
                yield return new WaitForSeconds(0.015f);
                val -= 1;
                StartCoroutine(StatusGuageChanging(0, val));
            } else if(val == 0)
            {
                yield return null;
            }

            // Condition Guage 변화
        } else
        {
            if (val < 0)
            {
                SoundManager.PlayMentalSound();
                if (guageMental > 0)
                {
                    guageMental -= 1;
                }
                else
                {
                    guageMental = 0;
                }
                mentalNum.GetComponent<Text>().text = guageMental.ToString();
                SetGuage(1);
                SetStatusText(1);
                yield return new WaitForSeconds(0.02f);
                val += 1;
                StartCoroutine(StatusGuageChanging(1, val));
            }
            else if (val > 0)
            {
                SoundManager.PlayMentalSound();
                if (guageMental < 100)
                {
                    guageMental += 1;
                }
                else
                {
                    guageMental = 100;
                }
                mentalNum.GetComponent<Text>().text = guageMental.ToString();
                SetGuage(1);
                SetStatusText(1);
                yield return new WaitForSeconds(0.02f);
                val -= 1;
                StartCoroutine(StatusGuageChanging(1, val));
            }
            else if (val == 0)
            {
                yield return null;
            }
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
