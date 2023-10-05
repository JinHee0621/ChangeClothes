using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatObject : WearObject 
{
    public Transform[] sponePosition;
    private Animator catAnim;
    public Sprite pickedImg;

    public bool cat_Pick = false;
    private bool firstStand = true;
    private bool cat_char_change = false;
    private Vector3 cat_first_pos;
    public int posCode = 0;
    private System.Random random = new System.Random();
    public bool cat_Moving;
    Coroutine runningCatMove = null;

    private int beforePosCode = 0;

    private void Start()
    {
        InitObject();
        SetPostion();
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        cat_first_pos = gameObject.transform.localPosition;
    }

    public void SetPostion()
    {
        if (runningCatMove != null) StopCoroutine(runningCatMove);

        if(DataManager.dataClearCount == 0)
        {
            posCode = 2;
        } else
        {
            if (beforePosCode == 2)
            {
                posCode = random.Next(0, 2);
            }
            else
            {
                posCode = random.Next(0, 3);
            }
        }
        beforePosCode = posCode;
        gameObject.transform.SetParent(sponePosition[posCode]);
        cat_first_pos = new Vector3(0f, 0f, 0f);
        gameObject.transform.localPosition = cat_first_pos;
        CatMotionSelect(posCode);
    }

    public Transform ReturnFirstPos()
    {
        return sponePosition[posCode];
    }

    public void CatMotionSelect(int code)
    {
        if(code != 2) runningCatMove = StartCoroutine(RunningMotion(code));
    }
    IEnumerator RunningMotion(int code)
    {
        int movingPercent = random.Next(0,100);
        catAnim.SetBool("Moving", false);
        if (!cat_Pick && !weared)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            if (code == 0)
            {
                if ((movingPercent < 80 && movingPercent >= 20) && !firstStand)
                {
                    movingPercent = random.Next(0, 100);
                    float movingRange = random.Next(0, 35) / 10f;
                    float movingTime = random.Next(15, 55) / 10f;
                    catAnim.SetBool("Moving", true);
                    if (movingPercent >= 50)
                    {
                        if (gameObject.transform.position.x <= 10)
                        {
                            gameObject.transform.DOLocalMoveX(gameObject.transform.localPosition.x + movingRange, movingTime);
                            cat_Moving = true;
                        }
                        yield return new WaitForSeconds(movingTime);
                        
                        if(!weared) gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        else gameObject.transform.localScale = new Vector3(0.6666667f, 0.6666667f, 1f);

                    } else if(movingPercent < 50)
                    {
                        if (gameObject.transform.position.x > -10)
                        {
                            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                            gameObject.transform.DOLocalMoveX(gameObject.transform.localPosition.x - movingRange, movingTime);
                            cat_Moving = true;
                        }
                        yield return new WaitForSeconds(movingTime);
                        if (!weared) gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        else gameObject.transform.localScale = new Vector3(0.6666667f, 0.6666667f, 1f);
                    }
                    if(!weared) cat_first_pos = gameObject.transform.localPosition;
                    else cat_first_pos = new Vector3(0f, 0f, 0f);

                    catAnim.SetBool("Moving", false);
                    cat_Moving = false;
                    yield return new WaitForSeconds(10f);
                    runningCatMove = StartCoroutine(RunningMotion(code));
                }
                else if (movingPercent >= 80 && movingPercent < 90)
                {
                    firstStand = false;
                    catAnim.SetTrigger("Sit");
                    yield return new WaitForSeconds(35f);

                    runningCatMove = StartCoroutine(RunningMotion(code));
                }
                else
                {
                    firstStand = false;
                    yield return new WaitForSeconds(15f);
                    runningCatMove = StartCoroutine(RunningMotion(code));
                }
            }
            else
            {
                if (movingPercent >= 50)
                {
                    catAnim.SetTrigger("Sit");
                    yield return new WaitForSeconds(60f);
                    runningCatMove = StartCoroutine(RunningMotion(code));
                }
                else
                {
                    yield return new WaitForSeconds(30f);
                    runningCatMove = StartCoroutine(RunningMotion(code));
                }
            }
        } else
        {
            yield return new WaitForSeconds(10f);
            runningCatMove = StartCoroutine(RunningMotion(code));
        }
    }


    public new void InitObject()
    {
        catAnim = gameObject.GetComponent<Animator>();
        cat_first_pos = gameObject.transform.localPosition;
        stat = GameObject.Find("Kimdoe").GetComponent<CharStateManager>();
        clothType_Part = 9;
    }

    private void OnMouseDrag()
    {
        if (!gameObject.GetComponent<MouseDrag>().nowCheck)
        {
            if (!move)
            {
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 4);
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                transform.position = objPosition;
                WearManager.ChangeCatch(true);
            }
        }
    }

    private void OnMouseUp()
    {
        cat_Pick = false;
        if (!gameObject.GetComponent<MouseDrag>().nowCheck)
        {
            if (!move)
            {
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                WearManager.ChangeCatch(false);

                if (weared)
                {
                    //¿Ê ÀåÂø
                    SoundManager.PlaySFX(1);
                    catAnim.SetBool("Equip", true);
                    stat.SetBody(clothType, clothType_Part);
                    gameObject.transform.localPosition = new Vector3(0, 0, 0);
                    gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                    gameObject.transform.localScale = new Vector3(0.6666667f, 0.6666667f, 1f);
                }
                else if (cat_char_change)
                {
                    WearManager.ChangeCharSprite(1);
                    ResetChanged();
                    gameObject.transform.localPosition = cat_first_pos;
                    catAnim.SetBool("Pick", false);
                    gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    catAnim.SetBool("Equip", false);
                    SoundManager.PlaySFX(2);
                    gameObject.transform.localPosition = cat_first_pos;
                    gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    stat.OutBody(clothType_Part, clothRankPoint);
                    catAnim.SetBool("Pick", false);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (!gameObject.GetComponent<MouseDrag>().nowCheck)
        {
            catAnim.SetBool("Equip", false);
            cat_Pick = true;
            if (!move)
            {
                DOTween.Kill(gameObject.transform);
                int pickSoundPercent = random.Next(0, 100);
                if(pickSoundPercent > 50)
                {
                    SoundManager.PlaySFX(27);
                } else
                {
                    SoundManager.PlaySFX(26);
                }
                catAnim.SetBool("Pick", true);
            }
        }
    }
    public void RollBack(bool nextDay)
    {
        catAnim.SetBool("Equip", false);
        if (nextDay)
        {
            SetPostion();
            gameObject.transform.localPosition = new Vector3(0f,0f,0f);
        } else
        {
            gameObject.transform.SetParent(sponePosition[posCode]);
            gameObject.transform.localPosition = cat_first_pos;
        }
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        stat.OutBody(clothType_Part, clothRankPoint);
        catAnim.SetBool("Pick", false);
    }
}
