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

    public void setBody(string object_type, int part_type)
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
        }
    }

    public void outBody(int part_type)
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
        }
    }
}
