using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIMovingManager : MonoBehaviour
{
    public GameObject dayUI;
    public GameObject bottomUI;
    public GameObject gameSetUI;
    public GameObject gameUIScroll;
    public GameObject statusSetUI;
    public GameObject clothSetBtnUI;
    public GameObject backgorundSetBtnUI;
    public GameObject moniterScreen;

    private Vector3 dayUI_default_pos = new Vector3(0, 0, 0);
    private Vector3 bottomUI_default_pos = new Vector3(0, 0, 0);
    void Start()
    {
        dayUI_default_pos = dayUI.transform.localPosition;
        bottomUI_default_pos = bottomUI.transform.localPosition;
        MoveUI();
    }

    public void MoniterOnOff(int plag)
    {
        if(plag == 0)
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

    public void MoveUI()
    {
        // dayUI.transform.DOMoveY(dayUI.transform.position.y - 83, 2f);
        bottomUI.transform.DOLocalMoveX(bottomUI.transform.localPosition.x - 653, 2.5f);
        bottomUI.transform.DOLocalMoveY(bottomUI.transform.localPosition.y + 421, 2.5f);
        dayUI.transform.DOLocalMoveY(dayUI.transform.localPosition.y - 139, 2f);
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
    }

    public void LeftMoveScroll()
    {
        gameUIScroll.transform.DOLocalMoveX(gameUIScroll.transform.localPosition.x + 200, 0.5f);
    }

    public void RightMoveScroll()
    {
        gameUIScroll.transform.DOLocalMoveX(gameUIScroll.transform.localPosition.x - 200, 0.5f);
    }
}
