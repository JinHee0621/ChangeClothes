using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameManager : MonoBehaviour
{
    public GameObject uiMoveManager;
    public List<GameObject> selectableGame;

    int gameNum = 0;
    bool moveScroll = false;

    public void LeftMoveScroll()
    {
        if(!moveScroll)
        {
            SoundManager.PlaySFX(6);
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
            SoundManager.PlaySFX(6);
            moveScroll = true;
            StartCoroutine("WaitSceond");
            gameNum += 1;
            uiMoveManager.GetComponent<UIMovingManager>().RightMoveScroll();
        }
    }


    IEnumerator WaitSceond()
    {
        yield return new WaitForSeconds(0.8f);
        moveScroll = false;
    }

}
