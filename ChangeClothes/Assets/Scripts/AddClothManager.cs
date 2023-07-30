using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClothManager : MonoBehaviour
{
    public UIMovingManager uiManager;
    public ClothSetManager clothSetManager;

    public GameObject shirtHanger;
    public GameObject pantsHanger;
    public GameObject outerHanger;

    public GameObject[] clothSet;
    public Transform shirtSetPos;
    public Transform outerSetPos;
    public Transform pantSetPos;

    public Transform latestShirtPos;
    public Transform latestOuterPos;
    public Transform latestPantsPos;
    // Start is called before the first frame update

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
                Vector3 nextPos;

                switch (target_ele.tag)
                {
                    case "Shirt":
                        nextPos = latestShirtPos.localPosition;
                        nextPos.x += 1f;
                        nextPos.z -= 0.1f;

                        new_hanger = Instantiate(shirtHanger, shirtSetPos);
                        new_hanger.transform.localPosition = nextPos;
                        target_ele.SetParent(new_hanger.transform);
                        target_ele.localPosition = first_ele_pos;

                        continue;

                    case "Outer":
                        nextPos = latestOuterPos.localPosition;
                        nextPos.x -= 1f;
                        nextPos.z -= 0.1f;

                        new_hanger = Instantiate(outerHanger, outerSetPos);
                        new_hanger.transform.localPosition = nextPos;
                        target_ele.SetParent(new_hanger.transform);
                        target_ele.localPosition = first_ele_pos;

                        continue;
                    case "Pants":
                        nextPos = latestPantsPos.localPosition;
                        nextPos.x += 1f;
                        nextPos.z -= 0.1f;

                        new_hanger = Instantiate(pantsHanger, pantSetPos);
                        new_hanger.transform.localPosition = nextPos;
                        target_ele.SetParent(new_hanger.transform);
                        target_ele.localPosition = first_ele_pos;
                        continue;
                }

            }

        }
        clothSetManager.ReNewHangers();
        uiManager.PopUpOpen();
    }
}
