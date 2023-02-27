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

    private void Start()
    {
        stat = GameObject.Find("Kimdoe").GetComponent<CharStateManager>();
        clothType_Part = setObjectPart(gameObject.tag);
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
            gameObject.transform.localPosition = new Vector3(0, 0);
        }
    }


    public void Equipped()
    {
        weared = true;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        stat.setBody(clothType, clothType_Part);
    }

    public void UnEquipped()
    {
        weared = false;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        stat.outBody(clothType_Part);
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
}
