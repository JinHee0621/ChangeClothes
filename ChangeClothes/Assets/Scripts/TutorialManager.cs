using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject tutorialSet;
    public GameObject tutorialContents;

    public int page = 0;

    void Start()
    {
        soundManager.BGMOn();
        if (!DataManager.viewTutorial)
        {
            StartCoroutine(TutorialFirstOpen());
        }
    }

    IEnumerator TutorialFirstOpen()
    {
        yield return new WaitForSeconds(1f);
        HowToPlayBtnClick();
        DataManager.viewTutorial = true;
    }

    public void HowToPlayBtnClick()
    {
        SoundManager.PlaySFX(7);
        tutorialSet.SetActive(true);
        page = 0;
        TutorialPageMov();
    }

    public void CloseTutorial()
    {
        SoundManager.PlaySFX(14);
        page = 0;
        TutorialPageMov();
        tutorialSet.SetActive(false);
    }

    public void TutorialPageMov()
    {

        switch (page)
        {
            case 0:
                tutorialContents.transform.DOMoveX(15f, 1.5f).SetEase(Ease.OutQuad);
                break;
            case 1:
                tutorialContents.transform.DOMoveX(4.5f, 1.5f).SetEase(Ease.OutQuad);
                break;
            case 2:
                tutorialContents.transform.DOMoveX(-5.3f, 1.5f).SetEase(Ease.OutQuad);
                break;
            case 3:
                tutorialContents.transform.DOMoveX(-15.3f, 1.5f).SetEase(Ease.OutQuad);
                break;
        }
    }

    public void NextTutorialPage()
    {
        if(page < 3)
        {
            page += 1;
            TutorialPageMov();
        }
    }

    public void PreviousTutorialPage()
    {
        if (page > 0)
        {
            page -= 1;
            TutorialPageMov();
        }
    }

}
