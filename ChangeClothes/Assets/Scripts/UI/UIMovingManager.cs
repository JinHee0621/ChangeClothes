using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UIMovingManager : MonoBehaviour
{
    public CharStateManager characterState;
    public BackgroundButtonManager backBtnManager;

    public Image fadeOut;

    public GameObject screen;
    public GameObject bottomUI;

    public GameObject checkResutCover1;
    public GameObject checkResutCover2;
    public GameObject speechBalloon;
    public Text speech1;
    public Text speech2;
    public Text[] partText;
    private string[] partArr = {"모자","얼굴","상의","하의","외투","장식" };
    public GameObject scoreNumText;
    public GameObject scoreMessageText;
    public GameObject multiScoreText;
    public Text scoreText;
    public int multiVal;
    private int scoreVal = 0;

    public Image scoreBackground;
    public Image scoreStar;
    private float score;

    public GameObject restartButton;
    public GameObject RankStarObj;
    public Animator[] stars;
    public ParticleSystem[] starPopEffect;

    public GameObject tutorialBtn;

    public GameObject challengeUI;
    public GameObject challengeAlert;
    public Text challengeNm;
    public GameObject challengeEffectPos;
    public ParticleSystem challengeEffect;

    public GameObject challengeHintUI;
    public Image challengeHintIcon;
    public Text challengeHintName;
    public Text challengeHintContent;

    public GameObject popupWindow;
    public Text popupWindowTxt;
    private bool isPopupOpen = false;

    public GameObject statusSetUI;
    public GameObject clothSetBtnUI;

    public GameObject[] scoreObjects;
    public GameObject boxText;

    Sprite[] daySprite;

    private Dictionary<string,string> checkResultData = new Dictionary<string, string>();
    private Vector3 dayUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 bottomUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 challengeUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 tutorialBtn_default_pos = new Vector3(0, 0, 0);
    private Vector3 viewerUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 rankUI_default_pos = new Vector3(0, 0, 0);

    System.Random randomIndex = new System.Random();

    private int checkedRank = 0;
    private bool challengeOpen = false;

    void Start()
    {
        ResetUI();
        multiVal = 1;
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

    public void CheckResultText(string part, string clothType)
    {
        if(checkResultData.ContainsKey(part)) checkResultData[part] =  clothType;
        else checkResultData.Add(part, clothType);
    }
    
    public Dictionary<string,string> ReturnResultDic()
    {
        return checkResultData;
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
        characterState.AllWearRollBack(true); // 게임 재시작시 캐릭터가 장착한 모든 옷 해제
        CheckResultCoverReset();
        speechBalloon.transform.DOLocalMoveY(speechBalloon.transform.localPosition.y - 1000f, 0.5f).SetEase(Ease.OutQuad);
        ReMoveRestartBtn();
        Color nextColor = scoreBackground.color;
        nextColor.a = 0f;
        scoreBackground.color = nextColor;
        speech1.text = "";
        speech2.text = "";

        for (int i = 0; i < partText.Length; i++)
        {
            partText[i].text = "";
        }
        scoreVal = 0;
        scoreText.text = "";

        checkResultData.Clear();
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
        speech1.DOText("이번에 입은 옷은..", 0.75f).SetEase(Ease.Linear);
        StartCoroutine(TextSoundPlay((0.75f/11), 0, 11));
        yield return new WaitForSeconds(0.85f);

        for(int i = 0; i < partText.Length-1; i++)
        {
            speech2.DOText(speech2.text + partArr[i] + " : \n", 0.75f).SetEase(Ease.Linear);
            StartCoroutine(TextSoundPlay((0.75f / 2), 0, 2));
            yield return new WaitForSeconds(0.75f);
            partText[i].text = checkResultData[partArr[i]];
            if(!checkResultData[partArr[i]].Equals("없음"))
            {
                scoreNumText.GetComponent<Text>().text = "+1";
                GameObject popScore = Instantiate(scoreNumText, partText[i].transform);
                SoundManager.PlaySFX(19);
                scoreVal += 1;
                StartCoroutine(DestroyScore(popScore));
            }
            yield return new WaitForSeconds(0.75f);
        }
        scoreText.text = "0 점";
        int half_val = scoreVal / 2;
        for (int i = 0; i <= half_val; i++)
        {
            scoreText.text = i.ToString() + " 점";
            SoundManager.PlaySFX(20);
            yield return new WaitForSeconds(0.15f);
        }

        for (int i = half_val + 1; i <= scoreVal; i++)
        {
            scoreText.text = i.ToString() + " 점";
            SoundManager.PlaySFX(20);
            yield return new WaitForSeconds(0.45f);
        }
        yield return new WaitForSeconds(0.75f);

        partText[partText.Length-1].DOText(checkResultData["종합"], 1f).SetEase(Ease.Linear);
        StartCoroutine(FinalTextSoundPlay((0.75f / checkResultData["종합"].Length), 0, checkResultData["종합"].Length));
        yield return new WaitForSeconds(1f);
        multiScoreText.GetComponent<Text>().text = "X" + multiVal;
        GameObject instantMultiScoreText = Instantiate(multiScoreText, scoreText.transform);
        yield return new WaitForSeconds(0.5f);
        SoundManager.PlaySFX(22);
        yield return new WaitForSeconds(0.5f);

        if(scoreVal > scoreVal * multiVal)
        {
            for (int i = scoreVal; i >= scoreVal * multiVal; i--)
            {
                scoreText.text = i.ToString() + " 점";
                SoundManager.PlaySFX(20);
                yield return new WaitForSeconds(0.075f);
            }
        } else
        {
            for (int i = scoreVal; i <= scoreVal * multiVal; i++)
            {
                scoreText.text = i.ToString() + " 점";
                SoundManager.PlaySFX(20);
                yield return new WaitForSeconds(0.075f);
            }
        }


        scoreVal *= multiVal;
        yield return new WaitForSeconds(0.5f);
        Destroy(instantMultiScoreText);

        if(checkResultData.ContainsKey("고양이"))
        {
            scoreNumText.GetComponent<Text>().text = "+10"; 
            GameObject popMessage = Instantiate(scoreMessageText, scoreText.transform);
            SoundManager.PlaySFX(22);
            for (int i = scoreVal; i <= scoreVal + 10; i++)
            {
                scoreText.text = i.ToString() + " 점";
                SoundManager.PlaySFX(20);
                yield return new WaitForSeconds(0.025f);
            }
            scoreVal += 10;
            StartCoroutine(DestroyScore(popMessage));
        }

        // 최종 스코어 랭크 표기 로직
        StartCoroutine(FadeInScoreUI());
        MoveScoreObj();
        yield return new WaitForSeconds(1f);


        PopUpRunning(scoreVal / 10);
    }

    IEnumerator DestroyScore(GameObject target)
    {
        yield return new WaitForSeconds(1f);
        Destroy(target);
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

    IEnumerator FinalTextSoundPlay(float repeatTime, int textIndex, int destIndex)
    {
        textIndex += 1;
        SoundManager.PlaySFX(21);
        yield return new WaitForSeconds(repeatTime);
        if (textIndex < destIndex)
        {
            StartCoroutine(FinalTextSoundPlay(repeatTime, textIndex, destIndex));
        }
        else
        {
            yield return null;
        }
    }


    IEnumerator FadeInScoreUI()
    {
        Color nextColor = scoreBackground.color;

        nextColor.a += 0.025f;
        scoreBackground.color = nextColor;

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
        //최고점수는 60점 고정
        if (rank >= 6) rank = 6;
        StartCoroutine(PopUpStars(rank));
    }

    IEnumerator PopUpStars(int rank)
    {
        checkedRank = rank;
        for (int i = 0; i < rank; i++)
        {
            if (i < 5)
            {
                stars[i].SetTrigger("StarPop");
                yield return new WaitForSeconds(0.5f);
                SoundManager.PlaySFX(18);
            }
            else
            {
                stars[i].SetTrigger("BigStarPop");
                yield return new WaitForSeconds(0.5f);
                SoundManager.PlaySFX(23);
            }

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
            if (i < 5) stars[i].SetTrigger("StarPop");
            else stars[i].SetTrigger("BigStarPop");
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

    public void MoveStatusGuage()
    {
        gameObject.GetComponent<StatusUIManager>().ReSetGuage();
    }
    // 도전과제 관련
    public void ChallengeBtnClicked()
    {
        SoundManager.PlaySFX(4);
        if (!challengeOpen)
        {
            challengeUI.transform.DOLocalMoveX(-275, 0.5f).SetEase(Ease.OutQuad);
            challengeOpen = true;
        } else
        {
            challengeUI.transform.DOLocalMoveX(-730, 0.5f).SetEase(Ease.OutQuad);
            challengeOpen = false;
        }
    }

    public void AlertChallengeClear(Sprite icon, string challengeName)
    {
        SoundManager.PlaySFX(5);
        challengeAlert.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = icon;
        challengeAlert.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = challengeName;
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

    public void OpenHintUI(Sprite hintIcon, string hintName, string hintContent)
    {
        challengeHintIcon.sprite = hintIcon;
        challengeHintName.text = hintName;
        challengeHintContent.text = hintContent;
        challengeHintUI.transform.DOLocalMoveX(117, 0.5f).SetEase(Ease.OutQuad);
    }

    public void CloseHintUI()
    {
        challengeHintUI.transform.DOLocalMoveX(-293, 0.5f).SetEase(Ease.OutQuad);
    }


    public void ResetUI()
    {
        //daySprite = Resources.LoadAll<Sprite>("UI/UI_Number");
        bottomUI_default_pos = bottomUI.transform.localPosition;
        challengeUI_default_pos = challengeUI.transform.localPosition;
        tutorialBtn_default_pos = tutorialBtn.transform.localPosition;

        MoveUI();
    }

    public void MoveUI()
    {
        StartCoroutine(DelayOpen(1.5f));
        StartCoroutine(DelayEffect(10, 1.5f));
        boxText.SetActive(true);
        bottomUI.transform.DOLocalMoveX(bottomUI.transform.localPosition.x - 668, 2.5f).SetEase(Ease.OutQuad); 
        bottomUI.transform.DOLocalMoveY(bottomUI.transform.localPosition.y + 418, 2.5f).SetEase(Ease.OutQuad); 
        challengeUI.transform.DOLocalMoveX(-730, 2.5f).SetEase(Ease.OutQuad); 
        tutorialBtn.transform.DOLocalMoveX(-817, 2.5f).SetEase(Ease.OutQuad);
    }

    public void RemoveUI()
    {
        boxText.SetActive(false);
        bottomUI.transform.DOLocalMoveX(bottomUI_default_pos.x, 2.5f);
        bottomUI.transform.DOLocalMoveY(bottomUI_default_pos.y, 2.5f);
        clothSetBtnUI.GetComponent<ClothSetManager>().CloseAll();
        backBtnManager.CloseMenuPub();
        challengeUI.transform.DOLocalMoveX(challengeUI_default_pos.x, 2.5f);
        tutorialBtn.transform.DOLocalMoveX(tutorialBtn_default_pos.x, 2.5f);
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
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(FirstFadeOut());
            yield return null;
        }
    }

    IEnumerator GoTitleFadeIn()
    {
        Color nextColor = fadeOut.color;
        nextColor.a += 0.025f;
        fadeOut.color = nextColor;
        yield return new WaitForSeconds(0.05f);
        if (fadeOut.color.a < 1)
        {
            StartCoroutine(GoTitleFadeIn());
        }
        else
        {
            SceneManager.LoadScene("Title");
            OptionManager.instance.GoTitle();
            yield return null;
        }
    }

    public void GoTitle()
    {
        fadeOut.gameObject.SetActive(true);
        StartCoroutine(GoTitleFadeIn());
    }



    IEnumerator DelayEffect(int sfxCode, float time)
    {
        yield return new WaitForSeconds(time);
        SoundManager.PlaySFX(sfxCode);
        StopCoroutine(DelayEffect(sfxCode, time));
    }

}
