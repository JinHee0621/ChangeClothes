using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
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
    }

    public static void ResetWaitTime()
    {
        checkTime = 0f;
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

}
