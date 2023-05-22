using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameManager : MonoBehaviour
{

    public GameObject uiMoveManager;
    public GameObject leftBtn;
    public GameObject rightBtn;
    public List<GameObject> selectableGame;

    public Text gameNameTxt;
    public Text gameConditionTxt;
    public Text gameMentalTxt;

    int gameNum = 0;
    bool moveScroll = false;

    public void SelectBtnChange(int flag)
    {
        if(flag == 1)
        {
            leftBtn.SetActive(false);
            rightBtn.SetActive(false);
        } else
        {
            leftBtn.SetActive(true);
            rightBtn.SetActive(true);
        }
    }

    public void LeftMoveScroll()
    {
        if(!moveScroll)
        {
            SoundManager.PlaySFX(4);
            moveScroll = true;
            StartCoroutine("WaitSceond");
            if (gameNum <= 0)
            {
                Debug.Log("First Game");
            }
            else
            {
                gameNum -= 1;
                uiMoveManager.GetComponent<UIMovingManager>().LeftMoveScroll();
            }
            ChangeGaameInfo();
        }
    }

    public void RightMoveScroll()
    {
        if(!moveScroll)
        {
            SoundManager.PlaySFX(4);
            moveScroll = true;
            StartCoroutine("WaitSceond");
            gameNum += 1;
            uiMoveManager.GetComponent<UIMovingManager>().RightMoveScroll();
            ChangeGaameInfo();
        }
    }

    public GameObject GetSelectedGameInfo()
    {
        return selectableGame[gameNum];
    }

    IEnumerator WaitSceond()
    {
        yield return new WaitForSeconds(0.8f);
        moveScroll = false;
    }

    public GameObject CheckSelectGame()
    {
        return selectableGame[gameNum];
    }

    public void ChangeGaameInfo()
    {
        SelectGameObject target = selectableGame[gameNum].gameObject.GetComponent<SelectGameObject>();

        gameNameTxt.text = target.gameName;
        gameConditionTxt.text = target.needCondition.ToString();
        gameMentalTxt.text = target.needMental.ToString();
    }

}
