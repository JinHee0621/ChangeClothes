using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckResultManager : MonoBehaviour
{
    public CharStateManager charStateManager;
    public UIMovingManager uiManager;
    public AddClothManager addClothManager;
    public ChallengeManager challengeManager;
    public int clearCount;
    public CatObject cat;
    public GameObject objectBox;
    public GameObject cover1;
    public GameObject cover2;

    public Animator[] stars;

    private bool nowRestarting = false;

    GameObject[] patterns;
    private void Start()
    {
        patterns = GameObject.FindGameObjectsWithTag("Pattern");
        foreach (GameObject i in patterns)
        {
            i.SetActive(false);
        }
        uiManager.FadeInCover();
    }


    public void RestartDay()
    {
        SoundManager.PlaySFX(5);
        if (nowRestarting == false)
        {
            StartCoroutine(ScreenOpen());

            nowRestarting = true;
        }
    }

    public void startCheckResult()
    {
        //결과확인중일때는 옵션이 열리지 않도록 [개발중일 때는 주석처리할것]
        //OptionManager.instance.nowCheckResult = true;

        if (clearCount == 0) ChallengeManager.AddChellengeClearId(1);
        else if (clearCount == 3) ChallengeManager.AddChellengeClearId(2);

        SoundManager.PlaySFX(5);
        uiManager.RemoveUI();
        StartCoroutine(ScreenClose());
    }

    IEnumerator ScreenClose()
    {
        addClothManager.clothSetManager.openAlert = true;
        CharEquippedSetCheck();
        charStateManager.StopFixCloth();
        yield return new WaitForSeconds(1.5f);
        SoundManager.OffBGM();
        cover1.GetComponent<Animator>().SetTrigger("StartCheck");
        cover2.GetComponent<Animator>().SetTrigger("StartCheck");
        SoundManager.PlaySFX(3);
       
        uiManager.MoveCharacter();
        yield return new WaitForSeconds(0.2f);
        foreach(GameObject i in patterns)
        {
            i.SetActive(true);
            i.GetComponent<BackGroundPattern>().FadeInPattern();
        }
        yield return new WaitForSeconds(0.5f);

    }

    public void CharEquippedSetCheck()
    {
        int score = 0;
        string hair_type = charStateManager.hair_type.Trim();
        string face_type = charStateManager.face_type.Trim();
        string glass_type = charStateManager.glass_type.Trim();
        string shirt_type = charStateManager.shirt_type.Trim();
        string pants_type = charStateManager.pants_type.Trim();
        string outer_type = charStateManager.outer_type.Trim();
        string left_type = charStateManager.left_type.Trim();
        string right_type = charStateManager.right_type.Trim();
        string cat_check = charStateManager.cat_type.Trim();
        
        if (hair_type.StartsWith("일반")) hair_type = "일반";
        if (face_type.StartsWith("일반")) face_type = "일반";
        if (glass_type.StartsWith("일반")) glass_type = "일반안경";
        if (shirt_type.StartsWith("일반")) shirt_type = "일반";
        if (pants_type.StartsWith("일반")) pants_type = "일반";
        if (outer_type.StartsWith("일반")) outer_type = "일반";
        if (left_type.StartsWith("일반")) left_type = "일반장식1";
        if (right_type.StartsWith("일반")) right_type = "일반장식2";

        if(!cat_check.Equals(""))
        {
            uiManager.CheckResultText("고양이", "커비");
        }

        if (hair_type.Equals("")) uiManager.CheckResultText("머리", "없음");
        else uiManager.CheckResultText("머리", hair_type);

        if (face_type.Equals("")) face_type = "없음";
        if (!glass_type.Equals(""))
        {
            if (face_type.Equals("없음"))
                face_type = glass_type;
            else
                face_type = face_type + ", " + glass_type;
        }
        uiManager.CheckResultText("얼굴", face_type);

        if (shirt_type.Equals("")) uiManager.CheckResultText("상의", "없음");
        else uiManager.CheckResultText("상의", shirt_type);

        if (pants_type.Equals("")) uiManager.CheckResultText("하의", "없음");
        else uiManager.CheckResultText("하의", pants_type);

        if (outer_type.Equals("")) uiManager.CheckResultText("외투", "없음");
        else uiManager.CheckResultText("외투", outer_type);

        if (left_type.Equals("")) left_type = "없음";
        if (!right_type.Equals(""))
        {
            if (left_type.Equals("없음"))
                left_type = right_type;
            else
                left_type = left_type + ", " + right_type;
        }
        uiManager.CheckResultText("장식", left_type);

        CheckChellengeCleard();
        uiManager.ScoreVal(score);
        clearCount += 1;
    }

    public void CheckChellengeCleard()
    {
        Dictionary<string, string> charStatus = uiManager.ReturnResultDic();

        if(charStatus["상의"].Equals("없음") && !charStatus["하의"].Equals("없음") && charStatus["외투"].Equals("없음"))
        {
            uiManager.CheckResultText("상의", "게이밍슈트 MK2");
            uiManager.CheckResultText("종합", "게이밍슈트");
            uiManager.multiVal = 10;
            ChallengeManager.AddChellengeClearId(4);
        } 
        
        if(charStatus["상의"].Equals("없음") && charStatus["하의"].Equals("없음") && charStatus["외투"].Equals("없음"))
        {
            uiManager.CheckResultText("종합", "옷좀입어요;");
            uiManager.multiVal = 0;
            ChallengeManager.AddChellengeClearId(3);
        } 
        
        if(charStatus["얼굴"].Equals("광대 코, 외계인안경") && charStatus["하의"].Equals("없음") && charStatus["외투"].Equals("없음"))
        {
            uiManager.CheckResultText("종합", "훈수가필요해");
            uiManager.multiVal = 10;
            ChallengeManager.AddChellengeClearId(5);
        } 
        
        if(!charStatus.ContainsKey("종합"))
        {
            uiManager.multiVal = 5;
            uiManager.CheckResultText("종합", "일반 복장");
        }
    }

    IEnumerator ScreenOpen()
    {
        float waitTime = ChallengeManager.checkTime;
        if (waitTime == 0f) waitTime = 2.5f;

        yield return new WaitForSeconds(0.5f);
        uiManager.FadeOutClear();
        yield return new WaitForSeconds(2.0f);
        challengeManager.CheckChallenge();
        foreach (GameObject i in patterns)
        {
            i.SetActive(false);
        }

        if(cat.weared == false)
        {
            cat.SetPostion();
        }

        yield return new WaitForSeconds(1f);
        cover1.GetComponent<Animator>().SetTrigger("StartCheck");
        cover2.GetComponent<Animator>().SetTrigger("StartCheck");
        yield return new WaitForSeconds(waitTime);
        uiManager.FadeInCover();
        uiManager.ResetUI();
        ChallengeManager.ResetWaitTime();
        yield return new WaitForSeconds(3f);
        nowRestarting = false;
        addClothManager.clothSetManager.openAlert = false;

        //결과확인중일때는 옵션이 열리지 않도록 [개발중일 때는 주석처리할것]
        //OptionManager.instance.nowCheckResult = false;

        if (challengeManager.addCloth)
        {
            uiManager.PopUpOpen(addClothManager.addMessage);
            yield return new WaitForSeconds(1f);
            challengeManager.ResetAddCloth();
            addClothManager.ResetAddState();
        }
    }



}
