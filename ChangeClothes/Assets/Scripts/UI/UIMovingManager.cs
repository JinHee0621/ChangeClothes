using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMovingManager : MonoBehaviour
{
    public CharStateManager characterState;

    public Image fadeOut;

    public GameObject screen;
    public GameObject bottomUI;

    public GameObject checkResutCover1;
    public GameObject checkResutCover2;
    public GameObject speechBalloon;
    public Text speech1;
    public Text speech2;
    public Text speech3;
    public Image scoreBackground;
    public Image scoreStar;
    private float score;
    public Text scoreNumText;
    public GameObject restartButton;
    public GameObject RankStarObj;
    public Animator[] stars;
    public ParticleSystem[] starPopEffect;

    public GameObject challengeUI;
    public GameObject challengeAlert;
    public Text challengeNm;
    public Image[] challengeIcon;
    public GameObject challengeEffectPos;
    public ParticleSystem challengeEffect;

    public GameObject popupWindow;
    public Text popupWindowTxt;
    private bool isPopupOpen = false;

    public GameObject statusSetUI;
    public GameObject clothSetBtnUI;
    public GameObject backgorundSetBtnUI;
    public GameObject moniterScreen;

    public GameObject conditonPosition;
    public GameObject mentalPosition;
    public GameObject conditionValObj;
    public GameObject mentalValObj;

    public GameObject[] scoreObjects;

    Sprite[] daySprite;

    private string checkResultData = "";
    private Vector3 dayUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 bottomUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 challengeUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 viewerUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 rankUI_default_pos = new Vector3(0, 0, 0);

    System.Random randomIndex = new System.Random();

    private int checkedRank = 0;
    private bool challengeOpen = false;

    void Start()
    {
        ResetUI();
    }

    public void PopUpOpen(string text)
    {
        if (isPopupOpen == false)
        {
            SoundManager.PlaySFX(16);
            isPopupOpen = true;
            popupWindowTxt.text = text;
            popupWindow.transform.DOScaleX(1f, 0.25f).SetEase(Ease.OutQuad);
            popupWindow.transform.DOScaleY(1f, 0.25f).SetEase(Ease.OutQuad);
            StartCoroutine(PopUpWait());
        }

    }

    IEnumerator PopUpWait()
    {
        yield return new WaitForSeconds(4f);
        PopUpClose();
        popupWindowTxt.text = "";
        yield return new WaitForSeconds(0.5f);
        isPopupOpen = false;
    }

    public void PopUpClose()
    {
        popupWindow.transform.DOScaleX(0f, 0.25f).SetEase(Ease.OutQuad);
        popupWindow.transform.DOScaleY(0f, 0.25f).SetEase(Ease.OutQuad);
    }

    public void FadeOutCover()
    {
        fadeOut.gameObject.SetActive(true);
        StartCoroutine(ResetFadeIn());
    }

    public void FadeOutClear()
    {
        fadeOut.gameObject.SetActive(true);
        StartCoroutine(ClearFadeIn());
    }

    public void FadeInCover()
    {
        screen.GetComponent<SpriteRenderer>().DOColor(new Color(0f,0f,0f,0f), 3f);
    }

    public void MoveCharacter()
    {
        StartCoroutine(MoveCharacterAnim());
    }

    public void CheckResultText(string text)
    {
        checkResultData = text;
    }

    IEnumerator ResetCharacterAnim()
    {
        characterState.gameObject.transform.localPosition = new Vector3(characterState.gameObject.transform.localPosition.x, characterState.gameObject.transform.localPosition.y, 8);
        characterState.gameObject.transform.DOLocalMoveX(characterState.gameObject.transform.localPosition.x - 2.0f, 0.5f);
        characterState.gameObject.transform.DOLocalMoveY(characterState.gameObject.transform.localPosition.y + 1.15f, 0.5f);
        characterState.gameObject.transform.DOScaleX(1f, 0.5f);
        characterState.gameObject.transform.DOScaleY(1f, 0.5f);
        characterState.StartFixCloth();
        yield return new WaitForSeconds(1.5f);
        characterState.AllWearRollBack(); // 게임 재시작시 캐릭터가 장착한 모든 옷 해제
        CheckResultCoverReset();
        speechBalloon.transform.DOLocalMoveY(speechBalloon.transform.localPosition.y - 1000f, 0.5f).SetEase(Ease.OutQuad);
        speechBalloon.transform.DORotate(new Vector3(-90, 0, 0), 0.25f).SetEase(Ease.Linear);
        ReMoveRestartBtn();
        Color nextColor = scoreBackground.color;
        nextColor.a = 0f;
        scoreBackground.color = nextColor;
        scoreNumText.color = nextColor;
        speech1.text = "";
        speech2.text = "";
        speech3.text = "";
        scoreNumText.text =  "0/100";
        scoreStar.fillAmount = 0;
        ResetScoreObj();
    }

    IEnumerator MoveCharacterAnim()
    {
        characterState.gameObject.transform.localPosition = new Vector3(characterState.gameObject.transform.localPosition.x, characterState.gameObject.transform.localPosition.y, -8);
        characterState.gameObject.transform.DOLocalMoveX(characterState.gameObject.transform.localPosition.x + 2.0f, 2.5f);
        characterState.gameObject.transform.DOLocalMoveY(characterState.gameObject.transform.localPosition.y - 1.15f, 2.5f);
        characterState.gameObject.transform.DOScaleX(1.15f, 1.5f);
        characterState.gameObject.transform.DOScaleY(1.15f, 1.5f);
        yield return new WaitForSeconds(2.5f);
        CheckResultMove();
        speechBalloon.transform.DOLocalMoveY(speechBalloon.transform.localPosition.y + 1000f, 2.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(2.5f);
        speechBalloon.transform.DORotate(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(0.75f);
        speech1.DOText("오늘 입은 옷은..", 0.75f).SetEase(Ease.Linear);
        StartCoroutine(TextSoundPlay((0.75f/10), 0, 10));
        yield return new WaitForSeconds(0.85f);
        speech2.DOText(checkResultData + " 구나!", 0.75f).SetEase(Ease.Linear);
        StartCoroutine(TextSoundPlay((0.75f / (checkResultData.Length + 4)), 0, checkResultData.Length + 4));
        yield return new WaitForSeconds(0.75f);
        speech3.DOText("만족도 : ", 0.5f).SetEase(Ease.Linear);
        StartCoroutine(TextSoundPlay((0.5f / 6), 0, 6));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeInScoreUI());
        MoveScoreObj();
        yield return new WaitForSeconds(1f);
        PopUpRunning(5);
    }


    IEnumerator TextSoundPlay(float repeatTime, int textIndex, int destIndex)
    {
        textIndex += 1;
        SoundManager.PlaySFX(12);
        yield return new WaitForSeconds(repeatTime);
        if(textIndex < destIndex)
        {
            StartCoroutine(TextSoundPlay(repeatTime, textIndex, destIndex));
        } else
        {
            yield return null;
        }
    }


    IEnumerator FadeInScoreUI()
    {
        Color nextColor = scoreBackground.color;

        nextColor.a += 0.025f;
        scoreBackground.color = nextColor;
        scoreNumText.color = nextColor;

        yield return new WaitForSeconds(0.05f);
        if (scoreBackground.color.a < 1)
        {
            StartCoroutine(FadeInScoreUI());
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            //StartCoroutine(ScoreUIMove(0, score));
            yield return null;
        }
    }

    public void ScoreVal(int value)
    {
        score = value / 100f;
        if (score > 1) score = 1;
    }

    IEnumerator ScoreUIMove(float nextAmount, float destAmount)
    {
        SoundManager.PlaySFX(7);
        yield return new WaitForSeconds(0.01f);
        if (nextAmount < destAmount)
        {
            nextAmount += 0.01f;

            string text = "" + (int)(nextAmount * 100);

            scoreNumText.text = text + "/100";
            scoreStar.fillAmount = nextAmount;
            StartCoroutine(ScoreUIMove(nextAmount, destAmount));
        } else
        {
            scoreStar.fillAmount = destAmount;
            yield return new WaitForSeconds(2.5f);
            MoveRestartBtn();
            yield return null;
        }
    }

    public void CheckResultMove()
    {
        checkResutCover1.transform.DOLocalMoveY(checkResutCover1.transform.localPosition.y - 100f, 2.5f);
        checkResutCover2.transform.DOLocalMoveY(checkResutCover2.transform.localPosition.y + 100f, 2.5f);
    }

    public void CheckResultCoverReset()
    {
        checkResutCover1.transform.DOLocalMoveY(checkResutCover1.transform.localPosition.y + 100f, 2.5f);
        checkResutCover2.transform.DOLocalMoveY(checkResutCover2.transform.localPosition.y - 100f, 2.5f);
    }

    public void MoveScoreObj()
    {
        RankStarObj.transform.DOLocalMoveY(RankStarObj.transform.localPosition.y + 5f, 1f).SetEase(Ease.OutQuad);
    }

    public void ResetScoreObj()
    {
        ResetStars();
        checkedRank = 0;
        RankStarObj.transform.DOLocalMoveY(RankStarObj.transform.localPosition.y - 5f, 0f);
    }


    public void PopUpRunning(int rank)
    {
        StartCoroutine(PopUpStars(rank));
    }

    IEnumerator PopUpStars(int rank)
    {
        checkedRank = rank;
        for (int i = 0; i < rank; i++)
        {
            stars[i].SetTrigger("StarPop");
            yield return new WaitForSeconds(0.5f);
            SoundManager.PlaySFX(18);
            ParticleSystem effect = Instantiate(starPopEffect[i], stars[i].transform) as ParticleSystem;
            effect.transform.position = stars[i].transform.position;
            Destroy(effect.gameObject, 1f);
        }
        MoveRestartBtn();
    }

    public void ResetStars()
    {
        for (int i = 0; i < checkedRank; i++)
        {
            stars[i].SetTrigger("StarPop");
        }
    }


    public void MoveRestartBtn()
    {
        restartButton.transform.DOLocalMoveY(restartButton.transform.localPosition.y + 350f, 1f).SetEase(Ease.OutQuad);
    }

    public void ReMoveRestartBtn()
    {
        restartButton.transform.DOLocalMoveY(restartButton.transform.localPosition.y - 350f, 1f).SetEase(Ease.OutQuad);
    }


    public void ShowCharStatVal(int target, int value)
    {
        GameObject popVal;
        value *= -1; // UI에서 보여지는 값은 감소값에 해당됨

        string showValue = value.ToString();
        if (value >= 0)
        {
            showValue = "+" + value.ToString();
        } 

        // target 0: Condition , 1 : Mental
        if (target == 0)
        {
            popVal = Instantiate(conditionValObj, conditonPosition.transform);
            popVal.GetComponent<Text>().text = showValue;
            popVal.transform.DOLocalMoveY(popVal.transform.localPosition.y + 15, 3f);
            StartCoroutine(RemoveValObj(popVal));
        }  else if (target == 1)
        {
            popVal = Instantiate(mentalValObj, mentalPosition.transform);
            popVal.GetComponent<Text>().text = showValue;
            popVal.transform.DOLocalMoveY(popVal.transform.localPosition.y + 15, 3f);
            StartCoroutine(RemoveValObj(popVal));
        }
    }
    
    IEnumerator RemoveValObj(GameObject target)
    {
        target.GetComponent<Text>().DOColor(new Color(target.GetComponent<Text>().color.r, target.GetComponent<Text>().color.g, target.GetComponent<Text>().color.b, 0f), 3f);
        yield return new WaitForSeconds(3.5f);
        Destroy(target);
        yield return null;
    }


    public bool CheckAllScoreActivate()
    {
        bool scoreActive = false;
        for (int i  = 0; i < scoreObjects.Length; i++)
        {
            if(scoreObjects[i].activeSelf)
            {
                scoreActive = true;
                break;
            }
        }
        return scoreActive;
    }

    public void ScoreAdd(int cnt)
    {
        int failcnt = 0;
        for (int i = 0; i < cnt;)
        {
            int index = randomIndex.Next(0, scoreObjects.Length - 1);

            if(failcnt >= 10)
            {
                gameObject.GetComponent<StatusUIManager>().statusSet.viewer_Like += 150;
                break;
            }

            if (scoreObjects[index].activeSelf)
            {
                failcnt += 1;
                continue;
            }
            else
            {
                scoreObjects[index].SetActive(true);
                scoreObjects[index].GetComponent<ScoreObject>().StartMove();
                i += 1;
            }
        }
    }

    public void MoniterOnOff(int plag)
    {
        if (plag == 0)
        {
            moniterScreen.transform.DOScale(new Vector3(0f, 0f, 1f), 0.5f).SetEase(Ease.InOutExpo);
        } else
        {
            moniterScreen.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.InOutExpo);

        }
    }

    public void MoveStatusGuage()
    {
        gameObject.GetComponent<StatusUIManager>().ReSetGuage();
    }

    public void ChallengeBtnClicked()
    {
        if (!challengeOpen)
        {
            challengeUI.transform.DOLocalMoveX(-330, 0.5f).SetEase(Ease.OutQuad);
            challengeOpen = true;
        } else
        {
            challengeUI.transform.DOLocalMoveX(-730, 0.5f).SetEase(Ease.OutQuad);
            challengeOpen = false;
        }
    }

    public void AlertChallengeClear()
    {
        SoundManager.PlaySFX(5);
        ParticleSystem effect = Instantiate(challengeEffect, challengeEffectPos.transform) as ParticleSystem;
        effect.transform.position = challengeEffectPos.transform.position;
        Destroy(effect.gameObject, 1f);
        StartCoroutine(AlertChallenge());
    }

    IEnumerator AlertChallenge() 
    {
        challengeAlert.transform.DOLocalMoveX(-570, 0.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.5f);
        challengeAlert.transform.DOLocalMoveX(-590, 0.05f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.05f);
        challengeAlert.transform.DOLocalMoveX(-570, 0.05f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.05f);
        challengeAlert.transform.DOLocalMoveX(-585, 0.05f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.05f);
        challengeAlert.transform.DOLocalMoveX(-570, 0.05f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.05f);
        challengeAlert.transform.DOLocalMoveX(-580, 0.05f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.05f);
        challengeAlert.transform.DOLocalMoveX(-570, 0.05f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.05f);
        challengeAlert.transform.DOLocalMoveX(-580, 0.05f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(0.05f);
        challengeAlert.transform.DOLocalMoveX(-585, 0.05f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(1.5f);
        challengeAlert.transform.DOLocalMoveX(-970, 0.5f).SetEase(Ease.OutQuad);
    }

    public void ResetUI()
    {
        //daySprite = Resources.LoadAll<Sprite>("UI/UI_Number");
        bottomUI_default_pos = bottomUI.transform.localPosition;
        challengeUI_default_pos = challengeUI.transform.localPosition;

        MoveUI();
    }

    public void MoveUI()
    {
        StartCoroutine(DelayOpen(1.5f));
        StartCoroutine(DelayEffect(10, 1.5f));
        bottomUI.transform.DOLocalMoveX(bottomUI.transform.localPosition.x - 668, 2.5f).SetEase(Ease.OutQuad); 
        bottomUI.transform.DOLocalMoveY(bottomUI.transform.localPosition.y + 418, 2.5f).SetEase(Ease.OutQuad); 
        challengeUI.transform.DOLocalMoveX(-730, 2.5f).SetEase(Ease.OutQuad); 
    }

    public void RemoveUI()
    {
        bottomUI.transform.DOLocalMoveX(bottomUI_default_pos.x, 2.5f);
        bottomUI.transform.DOLocalMoveY(bottomUI_default_pos.y, 2.5f);
        clothSetBtnUI.GetComponent<ClothSetManager>().CloseAll();
        backgorundSetBtnUI.GetComponent<BackgroundButtonManager>().CloseMenuPub();
        challengeUI.transform.DOLocalMoveX(challengeUI_default_pos.x, 2.5f);
    }

    IEnumerator DelayOpen(float time)
    {
        StartCoroutine(FirstFadeOut());
        yield return new WaitForSeconds(time);
    }

    IEnumerator FirstFadeOut()
    {
        Color nextColor = fadeOut.color;
        nextColor.a -= 0.025f;
        fadeOut.color = nextColor;
        yield return new WaitForSeconds(0.05f);
        if(fadeOut.color.a > 0)
        {
            StartCoroutine(FirstFadeOut());
        } else
        {
            fadeOut.gameObject.SetActive(false);
            yield return null;
        }
    }

    IEnumerator ResetFadeIn()
    {
        Color nextColor = fadeOut.color;
        nextColor.a += 0.025f;
        fadeOut.color = nextColor;
        yield return new WaitForSeconds(0.05f);
        if (fadeOut.color.a < 1)
        {
            StartCoroutine(ResetFadeIn());
        }
        else
        {
            StartCoroutine(ResetCharacterAnim());
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(FirstFadeOut());
            yield return null;
        }
    }

    IEnumerator ClearFadeIn()
    {
        Color nextColor = fadeOut.color;
        nextColor.a += 0.025f;
        fadeOut.color = nextColor;
        yield return new WaitForSeconds(0.05f);
        if (fadeOut.color.a < 1)
        {
            StartCoroutine(ClearFadeIn());
        }
        else
        {
            StartCoroutine(ResetCharacterAnim());
            yield return new WaitForSeconds(2.5f);
            yield return null;
        }
    }


    IEnumerator ResetFadeIn(float waitTime)
    {
        Color nextColor = fadeOut.color;
        nextColor.a += 0.025f;
        fadeOut.color = nextColor;
        yield return new WaitForSeconds(0.05f);
        if (fadeOut.color.a < 1)
        {
            StartCoroutine(ResetFadeIn(waitTime));
        }
        else
        {
            StartCoroutine(ResetCharacterAnim());
            Debug.Log("도전과제대기타임: "  + waitTime);
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(FirstFadeOut());
            yield return null;
        }
    }



    IEnumerator DelayEffect(int sfxCode, float time)
    {
        yield return new WaitForSeconds(time);
        SoundManager.PlaySFX(sfxCode);
        StopCoroutine(DelayEffect(sfxCode, time));
    }

}
