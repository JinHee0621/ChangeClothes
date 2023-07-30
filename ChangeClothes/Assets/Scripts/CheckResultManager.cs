using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckResultManager : MonoBehaviour
{
    public CharStateManager charStateManager;
    public UIMovingManager uiManager;
    public AddClothManager addClothManager;
    public GameObject objectBox;
    public GameObject cover1;
    public GameObject cover2;

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
        OptionManager.instance.nowCheckResult = true;
        SoundManager.PlaySFX(5);
        uiManager.RemoveUI();
        StartCoroutine(ScreenClose());
    }

    IEnumerator ScreenClose()
    {
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
        Dictionary<string, int> equipped = new Dictionary<string, int>();

        string shirt_type = charStateManager.shirt_type;
        string pants_type = charStateManager.pants_type;
        string outer_type = charStateManager.outer_type;
        string left_type = charStateManager.left_type;
        string right_type = charStateManager.right_type;
        string hair_type = charStateManager.hair_type;
        string glass_type = charStateManager.glass_type;
        string face_type = charStateManager.face_type;

        if (shirt_type.Equals(""))
        {
            uiManager.CheckResultText("게이밍 슈트");
            score += 80;
        }
        if (shirt_type.Equals("") && pants_type.Equals("") && outer_type.Equals("")) {
            uiManager.CheckResultText("속옷만 걸치고 \n 나왔");
            score += 10;
        }
        uiManager.ScoreVal(score);
    }
    IEnumerator ScreenOpen()
    {
        yield return new WaitForSeconds(0.5f);
        uiManager.FadeOutCover();
        yield return new WaitForSeconds(3.5f);
        foreach (GameObject i in patterns)
        {
            i.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        cover1.GetComponent<Animator>().SetTrigger("StartCheck");
        cover2.GetComponent<Animator>().SetTrigger("StartCheck");
        yield return new WaitForSeconds(2.5f);
        uiManager.FadeInCover();
        uiManager.ResetUI();
        yield return new WaitForSeconds(3f);
        nowRestarting = false;
        OptionManager.instance.nowCheckResult = false;
        addClothManager.UnLockSet("Squid");
    }



}
