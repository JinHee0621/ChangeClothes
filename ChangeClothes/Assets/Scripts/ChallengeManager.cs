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
    static Stack<int> clearChellengeIdStack = new Stack<int>();
    public GameObject[] challengeObjects;
    static GameObject[] challengeElements = new GameObject[10];
    static bool[] challengeCheck = new bool[10];
    public Text clearCntObj;
    static Text clearCntTxt;
    static int clearCnt = 0;
    public static float checkTime = 0f;

    Coroutine checkHintClickTime = null;
    private float clickTime = 0f;
    private bool openHint = false;
    private string[] hintContent =
    {
        "결과확인 1회 달성",
        "결과확인 3회 달성",
        "상의, 하의, 외투를 장비하지 않고 결과확인 1회 달성",
        "상의를 장비하지 않고 결과확인 1회 달성",
        "광대 코, 외계인 안경을 반드시 장비하고 결과확인 1회 달성",
        "",
        "",
        "",
        "",
        ""
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
            ClearChallenge(clearChellengeIdStack.Pop());
            yield return new WaitForSeconds(3f);
        }
    }

    public static void AddChellengeClearId(int id)
    {
        checkTime += 2.5f;
        clearChellengeIdStack.Push(id);
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
        if(clickTime > 1f && !openHint)
        {
            int contentIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name.Replace("Challenge", "").ToString());
            uiMovingObject.OpenHintUI(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite, EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Text>().text, hintContent[contentIndex-1]);
            openHint = true;
        }else
        {
            EventSystem.current.currentSelectedGameObject.transform.GetChild(4).GetComponent<Image>().fillAmount += 0.02f;
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
