using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CheckResultManager : MonoBehaviour
{
    public SoundManager soundManager;
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
        ChallengeManager.clearChellengeIdStack.Clear();
        //결과확인중일때는 옵션이 열리지 않도록 [개발중일 때는 주석처리할것]
        OptionManager.instance.nowCheckResult = true;

        if (clearCount == 0) ChallengeManager.AddChellengeClearId(1);
        else if (clearCount == 2) ChallengeManager.AddChellengeClearId(2);

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
        soundManager.BGMOff();
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

        if (hair_type.Equals("")) uiManager.CheckResultText("모자", "없음");
        else uiManager.CheckResultText("모자", hair_type);

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
        DataManager.dataClearCount = clearCount;
        DataManager.SaveFile();
    }

    public void CheckChellengeCleard()
    {
        Dictionary<string, string> charStatus = uiManager.ReturnResultDic();
        string resultText = "";
        if(charStatus["상의"].Equals("없음") && charStatus["하의"].Equals("없음") && charStatus["외투"].Equals("없음"))
        {
            resultText = "옷좀입어요; ";
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 0;
            if(!ChallengeManager.challengeCheck[2]) ChallengeManager.AddChellengeClearId(3);
        }else if(charStatus["외투"].Equals("마왕복장") && charStatus["모자"].Equals("마왕복장"))
        {
            resultText = "마왕김도 ";
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 20;
        }
        else if(charStateManager.isbald && charStatus["외투"].Equals("뒤집은 회색후드") && charStatus["하의"].Equals("츄리닝 바지"))
        {
            resultText = "ASEX LEGEND ";
            uiManager.CheckResultText("외투", "레이스 코스프레");
            uiManager.CheckResultText("하의", "레이스 코스프레");
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 15;
            if (!ChallengeManager.challengeCheck[9]) ChallengeManager.AddChellengeClearId(10);
        }
        else if (charStatus["얼굴"].Equals("없음") && charStatus["모자"].Equals("요네 코스프레") && charStatus["상의"].Equals("없음") && charStatus["하의"].Equals("요네 코스프레") && charStatus["장식"].Equals("겐지 칼") && charStatus["외투"].Equals("없음"))
        {
            resultText = "코스프레 ";
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 15;
            if (!ChallengeManager.challengeCheck[8])  ChallengeManager.AddChellengeClearId(9);
        }
        else if (charStatus["모자"].Equals("갓") && charStatus["얼굴"].Contains("선글라스") && charStatus["상의"].Equals("없음") && !charStatus["하의"].Equals("없음") && charStatus["얼굴"].Contains("헤드셋") && charStatus["장식"].Contains("리듬게임컨트롤러"))
        {
            resultText = "파워리듬게이머 ";
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 15;
        }
        else if (charStatus["모자"].Equals("갓") && charStatus["얼굴"].Contains("선글라스") && charStatus["상의"].Equals("없음") && charStatus["하의"].Equals("여름반바지") && charStatus["얼굴"].Contains("헤드셋"))
        {
            resultText = "리듬게이머 "; 
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 10;
            if (!ChallengeManager.challengeCheck[7]) ChallengeManager.AddChellengeClearId(8);
        }
        else if (charStatus["상의"].Equals("없음") && !charStatus["하의"].Equals("없음") && charStatus["외투"].Equals("없음"))
        {
            resultText = "게이밍슈트 "; 
            uiManager.CheckResultText("상의", "게이밍슈트");
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 10;
            if (!ChallengeManager.challengeCheck[3]) ChallengeManager.AddChellengeClearId(4);
        }
        else if(charStatus["모자"].Equals("헌팅 캡") && !charStatus["상의"].Equals("없음") && !charStatus["하의"].Equals("없음"))
        {
            resultText = "힐링방송 ";
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 10;
            if (!ChallengeManager.challengeCheck[5]) ChallengeManager.AddChellengeClearId(6);
        } else if ( charStatus["얼굴"].Equals("없음") && charStatus["모자"].Equals("렘도") && charStatus["상의"].Equals("렘도") && charStatus["하의"].Equals("렘도") && charStatus["외투"].Equals("없음"))
        {
            resultText = "렘 코스프레 ";
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 15;
        }
        else if (charStatus["얼굴"].Equals("없음") && charStatus["모자"].Equals("라뮬라나 복장") && charStatus["상의"].Equals("없음") && charStatus["하의"].Equals("라뮬라나 복장") && charStatus["외투"].Equals("라뮬라나 복장") && charStatus["장식"].Equals("채찍"))
        {
            resultText = "유적탐험가 ";
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 15;
        }
        else if (charStatus["상의"].Equals("크립토 복장") && charStatus["하의"].Equals("크립토 복장") && charStatus["외투"].Equals("크립토 복장"))
        {
            resultText = "크립토 ";
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 15;
        }
        else if (charStatus["외투"].Equals("싼다할아버지") && charStatus["모자"].Equals("싼다할아버지") && charStatus["얼굴"].Contains("싼다할아버지"))
        {
            resultText = "싼다 할아버지 ";
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal = 15;
        }

        if (!charStatus.ContainsKey("종합"))
        {
            resultText = "일반 복장 ";
            uiManager.multiVal = 5;
            uiManager.CheckResultText("종합", resultText);
        }

        if (charStatus["모자"].Equals("사천왕인형탈"))
        {
            resultText = "사천왕! : " + resultText;
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal += 2;
        }

        if (charStatus["얼굴"].Equals("광대 코, 외계인선글라스") && charStatus["장식"].Equals("훈수벨"))
        {
            resultText = "훈수구걸 : " + resultText;
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal -= 1;
        }
        else if (charStatus["얼굴"].Equals("광대 코, 외계인선글라스"))
        {
            resultText = "훈수허용 : " + resultText;
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal += 1;
            if (!ChallengeManager.challengeCheck[4]) ChallengeManager.AddChellengeClearId(5);
        }

        if (!charStatus["모자"].Equals("없음") && !charStatus["얼굴"].Equals("없음") && !charStatus["상의"].Equals("없음") && !charStatus["하의"].Equals("없음") && !charStatus["외투"].Equals("없음") && !charStatus["장식"].Equals("없음"))
        {
            resultText = "전신 장비 : " + resultText;
            uiManager.CheckResultText("종합", resultText);
            uiManager.multiVal += 3;
            if (!ChallengeManager.challengeCheck[6]) ChallengeManager.AddChellengeClearId(7);
        }

        if (DataManager.dataTrasure)
        {
            ChallengeManager.AddChellengeClearId(11);
        }

        ChallengeManager.checkAllChallenge();
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
        if (ChallengeManager.checkAllClear() && !DataManager.viewEnding)
        {
            SceneManager.LoadScene("Ending");
        } else
        {
            soundManager.BGMOn();
            uiManager.FadeInCover();
            uiManager.ResetUI();
            ChallengeManager.ResetWaitTime();
            yield return new WaitForSeconds(3f);
            nowRestarting = false;
            addClothManager.clothSetManager.openAlert = false;
            //결과확인중일때는 옵션이 열리지 않도록 [개발중일 때는 주석처리할것]
            OptionManager.instance.nowCheckResult = false;

            if (challengeManager.addCloth)
            {
                uiManager.PopUpOpen(addClothManager.addMessage);
                yield return new WaitForSeconds(1f);
                challengeManager.ResetAddCloth();
                addClothManager.ResetAddState();
            }
        }
    }

}
