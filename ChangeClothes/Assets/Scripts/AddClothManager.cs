using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClothManager : MonoBehaviour
{
    public GameObject hanger;
    public GameObject pantsHanger;
    public GameObject outerHanger;

    public GameObject[] clothSet;
    public Transform outerSetPos;
    public Transform pantSetPos;
    // Start is called before the first frame update
    void Start()
    {
        UnLockSet("Squid");
    }

    public void UnLockSet(string clothType)
    {
        GameObject target = null;
        int setEleCount = 0;
        foreach(GameObject ele in clothSet)
        {
            if(ele.name.Equals(clothType))
            {
                target = ele;
                setEleCount = target.transform.childCount;
                break;
            }
        }

        if (target != null)
        {
            for (int i = 0; i < setEleCount; i++)
            {
                Transform target_ele = null;
                GameObject new_hanger = null;
                target_ele = target.transform.GetChild(0);
                Vector3 first_ele_pos = target_ele.transform.position;
                switch (target_ele.tag)
                {
                    case "Outer":
                        new_hanger = Instantiate(outerHanger, outerSetPos);
                        new_hanger.transform.localPosition = new Vector2(1.5f, 0.19f);
                        target_ele.SetParent(new_hanger.transform);
                        target_ele.localPosition = first_ele_pos;
                        continue;
                    case "Pants":
                        new_hanger = Instantiate(pantsHanger, pantSetPos);
                        new_hanger.transform.localPosition = new Vector2(-3.8f, 0.53f);
                        target_ele.SetParent(new_hanger.transform);
                        target_ele.localPosition = first_ele_pos;
                        continue;
                }

            }

        }
    }
}
