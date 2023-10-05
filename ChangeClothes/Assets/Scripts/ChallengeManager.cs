using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChallengeManager : MonoBehaviour
{
    public AddClothManager addClothManager;
    public bool addCloth;

    public UIMovingManager uiMovingObject;
    static UIMovingManager uiMovingManager;
    public static Queue<int> clearChellengeIdStack = new Queue<int>();
    public GameObject[] challengeObjects;
    static GameObject[] challengeElements = new GameObject[12];
    public static bool[] challengeCheck = new bool[12];
    public Text clearCntObj;
    static Text clearCntTxt;
    public static int clearCnt = 0;
    public static float checkTime = 0f;

    Coroutine checkHintClickTime = null;
    private float clickTime = 0f;
    private bool openHint = false;
    private string[] hintContent =
    {
        "결과확인 1회",
        "결과확인 3회",
        "상의, 하의, 외투를 장비하지 않고 결과확인",
        "상의만 장비하지 않고 결과확인",
        "광대 코, 외계인 선글라스 를 반드시 장비하고 결과확인",
        "헌팅캡 모자, 상의 아무거나, 하의 아무거나 장비하고 결과확인",
        "모든 부위에 아무거나 장비하고 결과확인",
        "상의는 입으면 안되며 갓, 선글라스 아무거나, 여름반바지, 헤드셋 장비하고 결과확인",
        "요네 코스프레만 모두 장비하고 결과확인",
        "레이스 코스프레만 모두 장비하고 이발 후 결과확인",
        "방음부스에 숨어있는 보물을 찾는다",
        "이 도전과제를 제외한 나머지 도전과제를 달성한 다음 결과확인 1회"
    };

    private void Awake()
    {
        int index = 0;
        foreach (GameObject items in challengeObjects)
        {
            challengeElements[index] = items;
            challengeCheck[index] = false;
            index += 1;
        }
        clearCntTxt = clearCntObj;
        uiMovingManager = uiMovingObject;
    }



    public void CheckChallenge()
    {
        StartCoroutine(WaitCheckChellenge());
    }

    IEnumerator WaitCheckChellenge()
    {
        while (clearChellengeIdStack.Count != 0)
        {
            ClearChallenge(clearChellengeIdStack.Dequeue());
            yield return new WaitForSeconds(3f);
        }
    }

    public static void checkAllChallenge()
    {
        bool clearAllChallenge = true;
        for (int i = 0; i < challengeCheck.Length -1; i++)
        {
            if(challengeCheck[i] == false)
            {
                clearAllChallenge = false;
                break;
            }
        }
        if(clearAllChallenge) AddChellengeClearId(12);
    }

    
    public static bool checkAllClear()
    {
        bool clearAll = true;
        foreach(bool ele in DataManager.challengeClearArr)
        {
            if (ele == false)
            {
                clearAll = false;
                break;
            }
        }

        return clearAll;
    }



    public static void AddChellengeClearId(int id)
    {
        checkTime += 2.5f;
        clearChellengeIdStack.Enqueue(id);
        DataManager.ChanegeChallengeState(id);
    }

    public static void ResetWaitTime()
    {
        checkTime = 0f;
    }

    public void LoadClearChallenge(int challengeNum)
    {
        challengeElements[challengeNum].transform.Find("UnlockCover").gameObject.SetActive(false);
        clearCnt += 1;
        challengeCheck[challengeNum] = true;
        addClothManager.UnLockSet("Challenge" + (challengeNum + 1));
        addClothManager.ResetMessage();
        UpdateChallengeCnt();
    }


    public void ClearChallenge(int challengeNum)
    {
        if (challengeCheck[challengeNum-1] == false)
        {
            challengeElements[challengeNum-1].transform.Find("UnlockCover").gameObject.SetActive(false);
            clearCnt += 1;
            challengeCheck[challengeNum-1] = true;
            uiMovingManager.AlertChallengeClear(challengeElements[challengeNum - 1].transform.GetChild(0).GetComponent<Image>().sprite, challengeElements[challengeNum - 1].transform.GetChild(1).GetComponent<Text>().text);

            addClothManager.UnLockSet("Challenge" + challengeNum);
            addCloth = true;
        }
        UpdateChallengeCnt();
    }

    public static void UpdateChallengeCnt()
    {
        clearCntTxt.text = clearCnt.ToString();
    }
    public void ResetAddCloth()
    {
        addCloth = false;
    }

    public void HintOpen()
    {
        if(Input.GetMouseButtonDown(0))
        {
            checkHintClickTime = StartCoroutine(HintClickTime());
        }
    }
    IEnumerator HintClickTime()
    {
        yield return new WaitForSeconds(0.02f);
        checkHintClickTime = StartCoroutine(HintClickTime());
        if(clickTime > 1.3f && !openHint)
        {
            SoundManager.PlaySFX(14);
            int contentIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name.Replace("Challenge", "").ToString());
            uiMovingObject.OpenHintUI(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite, EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Text>().text, hintContent[contentIndex-1]);
            openHint = true;
        }else
        {
            if(clickTime > 0.3f) EventSystem.current.currentSelectedGameObject.transform.GetChild(4).GetComponent<Image>().fillAmount += 0.02f;
            clickTime += 0.02f;
        }
    }

    public void HintClose()
    {
        if(Input.GetMouseButtonUp(0))
        {
            EventSystem.current.currentSelectedGameObject.transform.GetChild(4).GetComponent<Image>().fillAmount = 0f;
            StopCoroutine(checkHintClickTime);
            uiMovingObject.CloseHintUI();
            clickTime = 0f;
            openHint = false;
        }
    }

}
