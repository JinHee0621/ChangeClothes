using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearObject : MonoBehaviour
{
    float distance = 4;
    public bool weared = false;
    private bool char_change = false;
    public bool isdecoObj;
    public bool wearedChange;
    public string clothType = "";
    public int clothType_Part = 0;
    public int clothRankPoint = 0;
    public CharStateManager stat;
    public Sprite[] whenWear;
    private Vector3 first_pos;
    private GameObject hanger;

    //[HideInInspector]
    public bool move = false;
    private void Start()
    {
        InitObject();
    }

    public void InitObject()
    {
        hanger = transform.parent.gameObject;

        first_pos = gameObject.transform.localPosition;
        stat = GameObject.Find("Kimdoe").GetComponent<CharStateManager>();
        clothType_Part = SetObjectPart(gameObject.tag);

        if (wearedChange)
        {
            whenWear = Resources.LoadAll<Sprite>(gameObject.tag + "/" + gameObject.name.Substring(0, gameObject.name.Length - 2));
        }
    }



    private void OnMouseDrag()
    {
        if (!gameObject.GetComponent<MouseDrag>().nowCheck)
        {
            if (!move)
            {
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                transform.position = objPosition;
                WearManager.ChangeCatch(true);
            }
        }
    }

    private void OnMouseUp()
    {
        if(!gameObject.GetComponent<MouseDrag>().nowCheck)
        {
            if (!move)
            {
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                WearManager.ChangeCatch(false);

                if (weared)
                {
                    //옷 장착
                    SoundManager.PlaySFX(1);
                    if (wearedChange)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = whenWear[1];
                    }

                    //동일 파트의 경우는 변경
                    if (!stat.GetBody(clothType_Part).Equals("") && !stat.GetBody(clothType_Part).Equals(clothType))
                    {
                        Transform weardObj = stat.transform.Find("Char_stand").transform.Find(gameObject.tag).GetChild(0);
                        weardObj.transform.SetParent(weardObj.GetComponent<WearObject>().GetThisHanger().transform);
                        weardObj.transform.localPosition = weardObj.GetComponent<WearObject>().GetFirstPos();
                        if ( weardObj.GetComponent<WearObject>().wearedChange)
                        {
                            weardObj.GetComponent<SpriteRenderer>().sprite = weardObj.GetComponent<WearObject>().whenWear[0];
                        }

                        if (weardObj.GetComponent<WearObject>().isdecoObj) weardObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                        else weardObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                    }

                    stat.SetBody(clothType, clothType_Part);
                    gameObject.transform.localPosition = new Vector3(0, 0);
                    gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else if (char_change)
                {
                    WearManager.ChangeCharSprite(1);
                    ResetChanged();
                    gameObject.transform.localPosition = first_pos;
                }
                else
                {
                    SoundManager.PlaySFX(2);
                    gameObject.transform.localPosition = first_pos;
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

    public void Changed()
    {
        char_change = true;
    }

    public void ResetChanged()
    {
        char_change = false;
    }

    public void Equipped()
    {
        weared = true;
    }

    public void UnEquipped()
    {
        weared = false;
    }

    public bool CheckChange()
    {
        return char_change;
    }

    public bool CheckEquip()
    {
        return weared;
    }

    private int SetObjectPart(string tag)
    {
        int typeNum = 0;
        switch(tag)
        {
            case "Shirt":
                typeNum = 1;
                break;
            case "Pants":
                typeNum = 2;
                break;
            case "Outer":
                typeNum = 3;
                break;
            case "Left":
                typeNum = 4;
                break;
            case "Right":
                typeNum = 5;
                break;
            case "Hair":
                typeNum = 6;
                break;
            case "Glass":
                typeNum = 7;
                break;
            case "Face":
                typeNum = 8;
                break;
        }
        return typeNum;
    }

    public Vector3 GetFirstPos()
    {
        return this.first_pos;
    }

    public GameObject GetThisHanger()
    {
        return this.hanger;
    }

    public void SetMove()
    {
        move = true;
        StartCoroutine("WaitSceond");
    }

    public bool GetMove()
    {
        return move;
    }

    IEnumerator WaitSceond()
    {
        yield return new WaitForSeconds(1.5f);
        move = false;
    }

    public void RollBack()
    {
        if ( wearedChange )
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = whenWear[0];
        }
        gameObject.transform.SetParent(GetThisHanger().transform);
        gameObject.transform.localPosition = first_pos;
        if (isdecoObj) gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        else gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        stat.OutBody(clothType_Part, clothRankPoint);
    }

}
