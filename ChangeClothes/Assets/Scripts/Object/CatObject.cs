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
    private int posCode = 0;
    private System.Random random = new System.Random();
    private void Start()
    {
        InitObject();
        SetPostion();
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        cat_first_pos = gameObject.transform.localPosition;
    }

    public void SetPostion()
    {
        posCode = random.Next(0, 2);
        posCode = 0;
        Debug.Log(posCode);
        gameObject.transform.SetParent(sponePosition[posCode]);
        CatMotionSelect(posCode);
    }

    public Transform ReturnFirstPos()
    {
        return sponePosition[posCode];
    }

    public void CatMotionSelect(int code)
    {
        if(code != 2) StartCoroutine(RunningMotion(code));
    }
    IEnumerator RunningMotion(int code)
    {
        int movingPercent = random.Next(0,100);
        if(!cat_Pick && !weared)
        {
            if (code == 0)
            {
                if (movingPercent >= 50 && !firstStand)
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
                        }
                        yield return new WaitForSeconds(movingTime);
                    } else
                    {
                        if (gameObject.transform.position.x > -10)
                        {
                            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                            gameObject.transform.DOLocalMoveX(gameObject.transform.localPosition.x - movingRange, movingTime);
                        }
                        yield return new WaitForSeconds(movingTime);
                        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    }

                    cat_first_pos = gameObject.transform.localPosition;
                    catAnim.SetBool("Moving", false);
                    yield return new WaitForSeconds(10f);
                    StartCoroutine(RunningMotion(code));
                }
                else
                {
                    firstStand = false;
                    yield return new WaitForSeconds(10f);
                    StartCoroutine(RunningMotion(code));
                }
            }
            else
            {
                if (movingPercent >= 50)
                {
                    catAnim.SetTrigger("Sit");
                    yield return new WaitForSeconds(15f);
                    catAnim.ResetTrigger("Sit");
                    StartCoroutine(RunningMotion(code));
                }
                else
                {
                    yield return new WaitForSeconds(10f);
                    StartCoroutine(RunningMotion(code));
                }
            }
        } else
        {
            yield return new WaitForSeconds(10f);
            StartCoroutine(RunningMotion(code));
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
        catAnim.SetBool("Pick", false);
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
                    if (wearedChange)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = whenWear[1];
                    }

                    stat.SetBody(clothType, clothType_Part);
                    gameObject.transform.localPosition = new Vector3(0, 0);
                    gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else if (cat_char_change)
                {
                    WearManager.ChangeCharSprite(1);
                    ResetChanged();
                    gameObject.transform.localPosition = cat_first_pos;
                }
                else
                {
                    SoundManager.PlaySFX(2);
                    gameObject.transform.localPosition = cat_first_pos;
                    if (isdecoObj) gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    else gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    stat.OutBody(clothType_Part, clothRankPoint);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (!gameObject.GetComponent<MouseDrag>().nowCheck)
        {
            catAnim.SetBool("Pick", true);
            cat_Pick = true;
            if (!move)
            {
                SoundManager.PlaySFX(0);
                if (wearedChange)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = whenWear[0];
                }
            }
        }
    }
    public new void RollBack()
    {
        gameObject.transform.localPosition = cat_first_pos;
        if (isdecoObj) gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        else gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        stat.OutBody(clothType_Part, clothRankPoint);
    }
}
