using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStateManager : MonoBehaviour
{
    public StatusUIManager statusUI;

    public bool playable = true;

    public bool isbald;

    public int minCondition = 60;
    public int maxCondition = 100;
    public int condition = 100;

    public int minMental = 15;
    public int maxMental = 100;
    public int mental = 100;

    public int streaming_date = 1;
    public int viewer_Like = 0;

    public string shirt_type = "";
    public string pants_type = "";
    public string outer_type = "";
    public string left_type = "";
    public string right_type = "";
    public string hair_type = "";
    public string glass_type = "";
    public string face_type = "";

    private GameObject shirt_part;
    private GameObject pants_part;
    private GameObject outer_part;
    private GameObject left_part;
    private GameObject right_part;
    private GameObject hair_part;
    private GameObject glass_part;
    private GameObject face_part;
    private GameObject cat_part;

    public int clothRank = 0;
    private void Start()
    {
        shirt_part = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        pants_part = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        outer_part = gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
        left_part = gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;
        right_part = gameObject.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject;
        hair_part = gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject;
        glass_part = gameObject.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject;
        face_part = gameObject.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject;
        cat_part = gameObject.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject;
    }

    public void StopFixCloth()
    {
        ColliderOff(shirt_part);
        ColliderOff(pants_part);
        ColliderOff(outer_part);
        ColliderOff(left_part);
        ColliderOff(right_part);
        ColliderOff(hair_part);
        ColliderOff(glass_part);
        ColliderOff(face_part);
        ColliderOff(cat_part);
    }

    public void ColliderOff(GameObject part)
    {
        MouseDrag[] partObj = part.GetComponentsInChildren<MouseDrag>();
        foreach(MouseDrag ele in partObj)
        {
            ele.nowCheck = true;
        }
    }

    public void StartFixCloth()
    {
        ColliderOn(shirt_part);
        ColliderOn(pants_part);
        ColliderOn(outer_part);
        ColliderOn(left_part);
        ColliderOn(right_part);
        ColliderOn(hair_part);
        ColliderOn(glass_part);
        ColliderOn(face_part);
        ColliderOn(cat_part);
    }

    public void ColliderOn(GameObject part)
    {
        MouseDrag[] partObj = part.GetComponentsInChildren<MouseDrag>();
        foreach (MouseDrag ele in partObj)
        {
            ele.nowCheck = false;
        }
    }


    public void ClickRollBackBtn()
    {
        SoundManager.PlaySFX(14);
        AllWearRollBack(false);
    }


    public void AllWearRollBack(bool nextDay)
    {
        WearRollback(shirt_part, nextDay);
        WearRollback(pants_part, nextDay);
        WearRollback(outer_part, nextDay);
        WearRollback(left_part, nextDay);
        WearRollback(right_part, nextDay);
        WearRollback(hair_part, nextDay);
        WearRollback(glass_part, nextDay);
        WearRollback(face_part, nextDay);
        WearRollback(cat_part, nextDay);
        WearManager.ChangeCharSprite(0);
        isbald = false;
    }

    public void WearRollback(GameObject part, bool nextDay)
    {
        if (part.name == "Cat")
        {
            CatObject partObj = part.GetComponentInChildren<CatObject>();
            if (partObj != null)
            {
                partObj.RollBack(nextDay);
            }
        } else
        {
            WearObject[] partObj = part.GetComponentsInChildren<WearObject>();
            foreach (WearObject ele in partObj)
            {
                ele.RollBack();
            }
        }

    }

    public void ClothRankAdd(int point)
    {
        clothRank = clothRank + point;
        statusUI.ChangeClothRankGuage();
    }

    public void ClothRankRemove(int point)
    {
        clothRank = clothRank - point;
        statusUI.ChangeClothRankGuage();
    }

    public void changeState(int target, int val)
    {
        if (target == 0)
        {
            condition = condition + val;
            if (condition <= 0) condition = 0;
            else if (condition >= 100) condition = 100;
        }
        else 
        {
            mental = mental + val;
            if (mental <= 0) mental = 0;
            else if (mental >= 100) mental = 100;
        }
    }

    public void NextDay()
    {
        streaming_date = streaming_date + 1;
    }

    public void RandomSetState()
    {
        condition = (int) Random.Range(minCondition, maxCondition);
        mental = (int)Random.Range(minMental, maxMental);
    }

    public void SetBody(string object_type, int part_type)
    {
        switch(part_type)
        {
            case 1:
                shirt_type = object_type;
                break;
            case 2:
                pants_type = object_type;
                break;
            case 3:
                outer_type = object_type;
                break;
            case 4:
                left_type = object_type;
                break;
            case 5:
                right_type = object_type;
                break;
            case 6:
                hair_type = object_type;
                break;
            case 7:
                glass_type = object_type;
                break;
            case 8:
                face_type = object_type;
                break;
        }
    }

    public void OutBody(int part_type, int part_score)
    {

        switch (part_type)
        {
            case 1:
                if (shirt_part.transform.childCount == 0)
                {
                    shirt_type = "";
                }
                break;
            case 2:
                if (pants_part.transform.childCount == 0)
                {
                    pants_type = "";
                }
                break;
            case 3:
                if (outer_part.transform.childCount == 0)
                {
                    outer_type = "";
                }
                break;
            case 4:
                if (left_part.transform.childCount == 0)
                {
                    left_type = "";
                }
                break;
            case 5:
                if (right_part.transform.childCount == 0)
                {
                    right_type = "";
                }
                break;
            case 6:
                if (hair_part.transform.childCount == 0)
                {
                    hair_type = "";
                }
                break;
            case 7:
                if (glass_part.transform.childCount == 0)
                {
                    glass_type = "";
                }
                break;
            case 8:
                if (face_part.transform.childCount == 0)
                {
                    face_type = "";
                }
                break;
        }
    }

    public string GetBody(int part_type)
    {
        switch (part_type)
        {
            case 1:
                return shirt_type;
            case 2:
                return pants_type;
            case 3:
                return outer_type;
            case 4:
                return left_type;
            case 5:
                return right_type;
            case 6:
                return hair_type;
            case 7:
                return glass_type;
            case 8:
                return face_type;
            default:
                return shirt_type;
        }
    }

    public string GetBody(string part_tag)
    {
        switch (part_tag)
        {
            case "Shirt":
                return shirt_type;
            case "Pants":
                return pants_type;
            case "Outer":
                return outer_type;
            case "Left":
                return left_type;
            case "Right":
                return right_type;
            case "Hair":
                return hair_type;
            case "Glass":
                return glass_type;
            case "Face":
                return face_type;
            default:
                return shirt_type;
        }
    }

}
