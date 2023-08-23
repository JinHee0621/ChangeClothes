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
        if (challengeCheck[challengeNum] == false)
        {
            challengeElements[challengeNum].transform.Find("UnlockCover").gameObject.SetActive(false);
            clearCnt += 1;
            challengeCheck[challengeNum] = true;
            uiMovingManager.AlertChallengeClear();

            switch (challengeNum) {
                case 0:
                    addClothManager.UnLockSet("Squid");
                    addCloth = true;
                    break;
                case 1:
                    addClothManager.UnLockSet("HunsuBell");
                    addCloth = true;
                    break;
            }
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
