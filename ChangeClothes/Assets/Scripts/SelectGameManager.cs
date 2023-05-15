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

}
