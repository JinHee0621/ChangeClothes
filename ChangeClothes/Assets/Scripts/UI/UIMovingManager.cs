using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMovingManager : MonoBehaviour
{
    public CharStateManager characterState;

    public GameObject screen;
    public GameObject dayUI;
    public GameObject dayNumUI;
    public GameObject bottomUI;

    public GameObject gameSetUI;
    public SelectGameManager gameSetManager;
    public GameObject gameUIScroll;

    public GameObject CamSet;
    public GameObject camScreenUI;
    public Text camScreenTxt;

    public GameObject statusSetUI;
    public GameObject clothSetBtnUI;
    public GameObject backgorundSetBtnUI;
    public GameObject moniterScreen;
    public GameObject viewerUI;
    public GameObject viewerUIText;

    public GameObject clothRankUI;
    public GameObject clothRankCenter;
    public GameObject clothRankContentSet;
    public GameObject clothRankContent;
    private int rankCount = 0;
 

    public GameObject conditonPosition;
    public GameObject mentalPosition;
    public GameObject conditionValObj;
    public GameObject mentalValObj;

    public GameObject[] scoreObjects;

    Sprite[] daySprite;

    private Vector3 dayUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 bottomUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 viewerUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 rankUI_default_pos = new Vector3(0, 0, 0);

    System.Random randomIndex = new System.Random();

    void Start()
    {
        ResetUI();
        MoveRankUI();
    }

    public void FadeOutCover()
    {
        screen.GetComponent<SpriteRenderer>().DOColor(new Color(0f,0f,0f,1f), 3f);
    }

    public void FadeInCover()
    {
        screen.GetComponent<SpriteRenderer>().DOColor(new Color(0f,0f,0f,0f), 3f);
    }

    public void PopupCamScreen(string text)
    {
        StartCoroutine(PopupCamScreenMove(text));
    }

    IEnumerator PopupCamScreenMove(string text)
    {
        CamSet.transform.DOLocalMoveX(CamSet.transform.localPosition.x - 450, 1.5f);
        camScreenTxt.DOText(text, 2f);
        yield return new WaitForSeconds(2.5f);
        CamSet.transform.DOLocalMoveX(CamSet.transform.localPosition.x + 450, 1.5f);
        yield return new WaitForSeconds(2f);
        camScreenTxt.text = "";
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
                ChangeViewerTxt();
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


    public void dayCheck(int num)
    {
        dayNumUI.GetComponent<Image>().sprite = daySprite[num];
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

    public void ResetUI()
    {
        daySprite = Resources.LoadAll<Sprite>("UI/UI_Number");
        dayCheck(gameObject.GetComponent<StatusUIManager>().statusSet.streaming_date);
        dayUI_default_pos = dayUI.transform.localPosition;
        bottomUI_default_pos = bottomUI.transform.localPosition;
        viewerUI_default_pos = viewerUI.transform.localPosition;
        MoveUI();

        rankUI_default_pos = clothRankUI.transform.localPosition;
        rankUI_default_pos.x += 155;

    }

    public void MoveUI()
    {
        StartCoroutine(DelayOpen(3f));
        StartCoroutine(DelayEffect(10, 1.0f));
        StartCoroutine(DelayEffect(10, 1.5f));
        bottomUI.transform.DOLocalMoveX(bottomUI.transform.localPosition.x - 653, 2.5f);
        bottomUI.transform.DOLocalMoveY(bottomUI.transform.localPosition.y + 421, 2.5f);
        dayUI.transform.DOLocalMoveY(dayUI.transform.localPosition.y - 139, 2f);
    }

    public void MoveRankUI()
    {
        clothRankUI.transform.DOLocalMoveX(clothRankUI.transform.localPosition.x + 155, 2f);
    }

    public void ReMoveRankUI()
    {
        clothRankUI.transform.DOLocalMoveX(clothRankUI.transform.localPosition.x - 155, 2f);
    }


    public void PopUpRankUI()
    {
        clothRankUI.transform.DOLocalMove(clothRankCenter.transform.localPosition, 1f);
        clothRankUI.transform.DOScaleX(1.75f, 1f);
        clothRankUI.transform.DOScaleY(1.75f, 1f);
    }

    public void BackRankUI()
    {
        clothRankUI.transform.DOLocalMove(rankUI_default_pos, 1f);
        clothRankUI.transform.DOScaleX(1f, 1f);
        clothRankUI.transform.DOScaleY(1f, 1f);
    }


    public void MoveRankContentUI(string contentText)
    {
        StartCoroutine(MoveRankContentCorutin(contentText));
    }

    IEnumerator MoveRankContentCorutin(string contentText)
    {
        if (rankCount > 0)
        {
            Transform[] rankChild = clothRankContentSet.GetComponentsInChildren<Transform>();
            if (rankChild != null)
            {
                for (int i = 1; i < rankChild.Length; i++)
                {
                    if (rankChild[i] != clothRankContentSet.transform)
                    {
                        rankChild[i].transform.DOLocalMoveY(rankChild[i].transform.localPosition.y - 55, 0.5f);
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        GameObject content = Instantiate(clothRankContent, clothRankContentSet.transform);
        yield return new WaitForSeconds(0.5f);
        content.GetComponent<Text>().DOText(contentText, 0.5f);
        yield return new WaitForSeconds(0.5f);
        rankCount += 1;
    }

    public void ResetRankContentUI()
    {
        rankCount = 0;
        Transform[] rankChild = clothRankContentSet.GetComponentsInChildren<Transform>();

        if(rankChild != null)
        {
            for(int i = 1; i < rankChild.Length; i++)
            {
                if(rankChild[i] != clothRankContentSet.transform)
                {
                    Destroy(rankChild[i].gameObject);
                }
            } 
        }
    }



    public void RemoveUI()
    {
        dayUI.transform.DOLocalMoveY(dayUI_default_pos.y, 2f);
        bottomUI.transform.DOLocalMoveX(bottomUI_default_pos.x, 2.5f);
        bottomUI.transform.DOLocalMoveY(bottomUI_default_pos.y, 2.5f);
        clothSetBtnUI.GetComponent<ClothSetManager>().CloseAll();
        backgorundSetBtnUI.GetComponent<BackgroundButtonManager>().CloseMenuPub();
    }

    public void OpenGameSetUI()
    {
        gameSetUI.transform.DOLocalMoveY(gameSetUI.transform.localPosition.y + 940, 1.5f);
        statusSetUI.transform.DOLocalMoveY(statusSetUI.transform.localPosition.y + 940, 1.5f);
        viewerUI.transform.DOLocalMoveY(viewerUI.transform.localPosition.y - 139, 1.5f);
        gameSetManager.ChangeGaameInfo();
    }

    public void CloseGameSetUI()
    {
        gameSetUI.transform.DOLocalMoveY(gameSetUI.transform.localPosition.y - 940, 1.5f);
        statusSetUI.transform.DOLocalMoveY(statusSetUI.transform.localPosition.y - 940, 1.5f);
        viewerUI.transform.DOLocalMoveY(viewerUI_default_pos.y, 1.5f);
    }


    public void LeftMoveScroll()
    {
        gameUIScroll.transform.DOLocalMoveX(gameUIScroll.transform.localPosition.x + 200, 0.5f);
    }

    public void RightMoveScroll()
    {
        gameUIScroll.transform.DOLocalMoveX(gameUIScroll.transform.localPosition.x - 200, 0.5f);
    }

    IEnumerator DelayOpen(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator DelayEffect(int sfxCode, float time)
    {
        yield return new WaitForSeconds(time);
        SoundManager.PlaySFX(sfxCode);
        StopCoroutine(DelayEffect(sfxCode, time));
    }

    public void ChangeViewerTxt()
    {
        viewerUIText.GetComponent<Text>().text = gameObject.GetComponent<StatusUIManager>().statusSet.viewer_Like.ToString();
    }
}
