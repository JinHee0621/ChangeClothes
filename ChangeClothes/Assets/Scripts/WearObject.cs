using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearObject : MonoBehaviour
{
    float distance = 4;
    public bool weared = false;
    public string clothType = "";
    public int clothType_Part = 0;
    public CharStateManager stat;
    public Sprite[] whenWear;
    private Vector2 first_pos;

    private void Start()
    {
        first_pos = gameObject.transform.localPosition;
        stat = GameObject.Find("Kimdoe").GetComponent<CharStateManager>();
        clothType_Part = setObjectPart(gameObject.tag);

        if(gameObject.tag.Equals("Outer"))
        {
            whenWear = Resources.LoadAll<Sprite>("Outer/" + gameObject.name.Substring(0,gameObject.name.Length - 2));
        }
    }

    // X : 9, Y : 5
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.position = objPosition;
        WearManager.changeCatch(true);
    }

    private void OnMouseUp()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
       WearManager.changeCatch(false);

        if (weared)
        {
            SoundManager.PlaySFX(1);
            if (gameObject.tag.Equals("Outer"))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = whenWear[1];
            }

            if (!stat.getBody(clothType_Part).Equals("") && !stat.getBody(clothType_Part).Equals(clothType))
            {
                Transform weardObj = stat.transform.Find("Char_stand").transform.Find(gameObject.tag).GetChild(0);
                weardObj.transform.SetParent(GameObject.Find("Object").gameObject.transform.Find(gameObject.tag));
                weardObj.transform.localPosition = weardObj.GetComponent<WearObject>().getFirstPos();
                weardObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            stat.setBody(clothType, clothType_Part);
            gameObject.transform.localPosition = new Vector3(0, 0);

        } else
        {
            SoundManager.PlaySFX(2);
            if (!weared)
            {
                gameObject.transform.localPosition = first_pos;
            }
        }
    }

    private void OnMouseDown()
    {
        SoundManager.PlaySFX(0);
        if (gameObject.tag.Equals("Outer"))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = whenWear[0];
        }
    }


    public void Equipped()
    {
        weared = true;
    }

    public void UnEquipped()
    {
        weared = false;
    }

    public bool checkEquip()
    {
        return weared;
    }

    private int setObjectPart(string tag)
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
        }
        return typeNum;
    }

    public Vector2 getFirstPos()
    {
        return this.first_pos;
    }
}
