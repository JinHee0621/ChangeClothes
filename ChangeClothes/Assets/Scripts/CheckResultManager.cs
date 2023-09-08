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
        //���Ȯ�����϶��� �ɼ��� ������ �ʵ��� [�������� ���� �ּ�ó���Ұ�]
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
        
        if (hair_type.StartsWith("�Ϲ�")) hair_type = "�Ϲ�";
        if (face_type.StartsWith("�Ϲ�")) face_type = "�Ϲ�";
        if (glass_type.StartsWith("�Ϲ�")) glass_type = "�ϹݾȰ�";
        if (shirt_type.StartsWith("�Ϲ�")) shirt_type = "�Ϲ�";
        if (pants_type.StartsWith("�Ϲ�")) pants_type = "�Ϲ�";
        if (outer_type.StartsWith("�Ϲ�")) outer_type = "�Ϲ�";
        if (left_type.StartsWith("�Ϲ�")) left_type = "�Ϲ����1";
        if (right_type.StartsWith("�Ϲ�")) right_type = "�Ϲ����2";

        if(!cat_check.Equals(""))
        {
            uiManager.CheckResultText("�����", "Ŀ��");
        }

        if (hair_type.Equals("")) uiManager.CheckResultText("�Ӹ�", "����");
        else uiManager.CheckResultText("�Ӹ�", hair_type);

        if (face_type.Equals("")) face_type = "����";
        if (!glass_type.Equals(""))
        {
            if (face_type.Equals("����"))
                face_type = glass_type;
            else
                face_type = face_type + ", " + glass_type;
        }
        uiManager.CheckResultText("��", face_type);

        if (shirt_type.Equals("")) uiManager.CheckResultText("����", "����");
        else uiManager.CheckResultText("����", shirt_type);

        if (pants_type.Equals("")) uiManager.CheckResultText("����", "����");
        else uiManager.CheckResultText("����", pants_type);

        if (outer_type.Equals("")) uiManager.CheckResultText("����", "����");
        else uiManager.CheckResultText("����", outer_type);

        if (left_type.Equals("")) left_type = "����";
        if (!right_type.Equals(""))
        {
            if (left_type.Equals("����"))
                left_type = right_type;
            else
                left_type = left_type + ", " + right_type;
        }
        uiManager.CheckResultText("���", left_type);

        CheckChellengeCleard();
        uiManager.ScoreVal(score);
        clearCount += 1;
    }

    public void CheckChellengeCleard()
    {
        Dictionary<string, string> charStatus = uiManager.ReturnResultDic();

        if(charStatus["����"].Equals("����") && !charStatus["����"].Equals("����") && charStatus["����"].Equals("����"))
        {
            uiManager.CheckResultText("����", "���ֽ̹�Ʈ MK2");
            uiManager.CheckResultText("����", "���ֽ̹�Ʈ");
            uiManager.multiVal = 10;
            ChallengeManager.AddChellengeClearId(4);
        } 
        
        if(charStatus["����"].Equals("����") && charStatus["����"].Equals("����") && charStatus["����"].Equals("����"))
        {
            uiManager.CheckResultText("����", "�����Ծ��;");
            uiManager.multiVal = 0;
            ChallengeManager.AddChellengeClearId(3);
        } 
        
        if(charStatus["��"].Equals("���� ��, �ܰ��ξȰ�") && charStatus["����"].Equals("����") && charStatus["����"].Equals("����"))
        {
            uiManager.CheckResultText("����", "�Ƽ����ʿ���");
            uiManager.multiVal = 10;
            ChallengeManager.AddChellengeClearId(5);
        } 
        
        if(!charStatus.ContainsKey("����"))
        {
            uiManager.multiVal = 5;
            uiManager.CheckResultText("����", "�Ϲ� ����");
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

        //���Ȯ�����϶��� �ɼ��� ������ �ʵ��� [�������� ���� �ּ�ó���Ұ�]
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
