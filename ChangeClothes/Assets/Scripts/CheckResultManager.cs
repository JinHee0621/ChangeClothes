using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CheckResultManager : MonoBehaviour
{
    public CharStateManager charStateManager;
    public UIMovingManager uiManager;
    public GameObject objectBox;
    public GameObject cover1;
    public GameObject cover2;


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

    public void NextDay()
    {
        StartCoroutine(ScreenOpen());
    }

    public void startCheckResult()
    {
        SoundManager.PlaySFX(5);
        uiManager.RemoveUI();
        StartCoroutine(ScreenClose());
    }

    IEnumerator ScreenClose()
    {
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
        //시너지 효과 등 출력
        Dictionary<string, int> equipped = CharEquippedSetCheck();

    }

    public Dictionary<string, int> CharEquippedSetCheck()
    {
        Dictionary<string, int> equipped = new Dictionary<string, int>();

        string shirt_type = charStateManager.shirt_type;
        string pants_type = charStateManager.pants_type;
        string outer_type = charStateManager.outer_type;
        string left_type = charStateManager.left_type;
        string right_type = charStateManager.right_type;
        string hair_type = charStateManager.hair_type;
        string glass_type = charStateManager.glass_type;
        string face_type = charStateManager.face_type;

        equipped.Add(shirt_type, 1);

        if (equipped.ContainsKey(pants_type))  equipped[pants_type] += 1;
        else equipped.Add(pants_type, 1);

        if (equipped.ContainsKey(outer_type)) equipped[outer_type] += 1;
        else equipped.Add(outer_type, 1);

        if (equipped.ContainsKey(left_type))  equipped[left_type] += 1;
        else equipped.Add(left_type, 1);

        if (equipped.ContainsKey(right_type)) equipped[right_type] += 1;
        else equipped.Add(right_type, 1);

        if (equipped.ContainsKey(hair_type))  equipped[hair_type] += 1;
        else equipped.Add(hair_type, 1);

        if (equipped.ContainsKey(glass_type)) equipped[glass_type] += 1;
        else equipped.Add(glass_type, 1);

        if (equipped.ContainsKey(face_type))equipped[face_type] += 1;
        else equipped.Add(face_type, 1);

        return equipped;
    }

    IEnumerator ScreenOpen()
    {
        yield return new WaitForSeconds(2.5f);
        uiManager.GetComponent<UIMovingManager>().CloseGameSetUI();
        uiManager.GetComponent<UIMovingManager>().FadeOutCover();
        yield return new WaitForSeconds(3.5f);
        foreach (GameObject i in patterns)
        {
            i.SetActive(false);
        }
        //SoundManager.OffBGM();
        //SoundManager.PlaySFX(3);
        yield return new WaitForSeconds(1f);
        cover1.GetComponent<Animator>().SetTrigger("StartCheck");
        cover2.GetComponent<Animator>().SetTrigger("StartCheck");
        yield return new WaitForSeconds(3f);
        uiManager.FadeInCover();
        yield return new WaitForSeconds(3f);
        uiManager.ResetUI();
        //streamManager.StreamStateChange(0);
    }



}
