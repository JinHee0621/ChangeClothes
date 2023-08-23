using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClothManager : MonoBehaviour
{
    public UIMovingManager uiManager;
    public ClothSetManager clothSetManager;

    public GameObject hairHanger;
    public GameObject shirtHanger;
    public GameObject pantsHanger;
    public GameObject outerHanger;

    public GameObject[] clothSet;
    public Transform hairSetPos;
    public Transform shirtSetPos;
    public Transform outerSetPos;
    public Transform pantSetPos;
    public Transform itemSetPos;

    public Transform latestHairPos;
    public Transform latestShirtPos;
    public Transform latestOuterPos;
    public Transform latestPantsPos;

    public string addMessage;
    public bool shirtAdd;
    public bool pantsAdd;
    public bool outerAdd;
    public bool hairAdd;
    public bool itemAdd;
    // Start is called before the first frame update

    public void UnLockSet(string clothType)
    {
        clothSetManager.openAlert = true;
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
                        if (nextPos.x >= 3.5f)
                        {
                            nextPos.x = -3.5f;
                            nextPos.y -= 2.5f;
                        }
                        else nextPos.x += 1f;
                        nextPos.z -= 0.1f;

                        new_hanger = Instantiate(shirtHanger, shirtSetPos);
                        new_hanger.transform.localPosition = nextPos;
                        target_ele.SetParent(new_hanger.transform);
                        target_ele.localPosition = first_ele_pos;
                        latestShirtPos = new_hanger.transform;
                        shirtAdd = true;
                        continue;

                    case "Outer":
                        nextPos = latestOuterPos.localPosition;
                        nextPos.x -= 1f;
                        nextPos.z -= 0.1f;

                        new_hanger = Instantiate(outerHanger, outerSetPos);
                        new_hanger.transform.localPosition = nextPos;
                        target_ele.SetParent(new_hanger.transform);
                        target_ele.localPosition = first_ele_pos;
                        latestOuterPos = new_hanger.transform;
                        outerAdd = true;
                        continue;
                    case "Pants":
                        nextPos = latestPantsPos.localPosition;
                        if (nextPos.x >= 3.5f)
                        {
                            nextPos.x = -3.5f;
                            nextPos.y -= 3f;
                        }
                        else nextPos.x += 1f;
                        nextPos.z -= 0.1f;

                        new_hanger = Instantiate(pantsHanger, pantSetPos);
                        new_hanger.transform.localPosition = nextPos;
                        target_ele.SetParent(new_hanger.transform);
                        target_ele.localPosition = first_ele_pos;

                        latestPantsPos = new_hanger.transform;
                        pantsAdd = true;
                        continue;
                    case "Hair":
                        nextPos = latestHairPos.localPosition;
                        if (nextPos.x >= 3.5f)
                        {
                            nextPos.x = -3.5f;
                            nextPos.y -= 1.5f;
                        }
                        else nextPos.x += 1f;
                        nextPos.z -= 0.1f;

                        new_hanger = Instantiate(hairHanger, hairSetPos);
                        new_hanger.transform.localPosition = nextPos;
                        target_ele.SetParent(new_hanger.transform);
                        target_ele.localPosition = first_ele_pos;

                        latestHairPos = new_hanger.transform;
                        hairAdd = true;
                        continue;
                    default:
                        target_ele.SetParent(itemSetPos);
                        target_ele.localPosition = first_ele_pos;
                        itemAdd = true;
                        continue;
                }

            }

        }

        if (shirtAdd && !addMessage.Contains("상의")) addMessage += "신규 상의\n";
        if (pantsAdd && !addMessage.Contains("하의")) addMessage += "신규 하의\n";
        if (outerAdd && !addMessage.Contains("외투")) addMessage += "신규 외투\n";
        if (hairAdd && !addMessage.Contains("모자")) addMessage += "신규 모자\n";
        if (itemAdd && !addMessage.Contains("도구")) addMessage += "신규 도구\n";

        clothSetManager.ReNewHangers();
        StartCoroutine(WaitAlert());
    }

    IEnumerator WaitAlert()
    {
        yield return new WaitForSeconds(2f);
        clothSetManager.openAlert = false;
    }

    public void ResetAddState()
    {
        addMessage = "";
        shirtAdd = false;
        pantsAdd = false;
        outerAdd = false;
        hairAdd = false;
        itemAdd = false;
    }
}
