using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStateManager : MonoBehaviour
{
    public string shirt_type = "";
    public string pants_type = "";
    public string outer_type = "";
    public string left_type = "";
    public string right_type = "";
    public string hair_type = "";
    public string glass_type = "";
    public string face_type = "";

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

    public void OutBody(int part_type)
    {
        switch (part_type)
        {
            case 1:
                shirt_type = "";
                break;
            case 2:
                pants_type = "";
                break;
            case 3:
                outer_type = "";
                break;
            case 4:
                left_type = "";
                break;
            case 5:
                right_type = "";
                break;
            case 6:
                hair_type = "";
                break;
            case 7:
                glass_type = "";
                break;
            case 8:
                face_type = "";
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

}
