using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothSetManager : MonoBehaviour
{
    public GameObject shirtSet;
    public GameObject pantsSet;
    public GameObject outerSet;
    public GameObject hairSet;

    GameObject[] hangerSet;
    GameObject[] pantsHangerSet;
    GameObject[] outerHangerSet;
    GameObject[] hairHangerSet;

    private bool isShirtSetOpen = false;
    private bool isPantsSetOpen = false;
    private bool isOuterSetOpen = false;
    private bool isHairSetOpen = false;

    private bool moving = false;


    private void Start()
    {
        ReNewHangers();
    }

    public void ReNewHangers()
    {
        hangerSet = GameObject.FindGameObjectsWithTag("Hanger");
        pantsHangerSet = GameObject.FindGameObjectsWithTag("Hanger_Pants");
        outerHangerSet = GameObject.FindGameObjectsWithTag("Hanger_Outer");
        hairHangerSet = GameObject.FindGameObjectsWithTag("Hanger_Hair");
    }


    public void ShirtSetOpen()
    {
        if (!moving)
        {
            moving = true;
            StartCoroutine("WaitSceond");
            SoundManager.PlaySFX(4);
            if (!isShirtSetOpen)
            {
                shirtSet.GetComponent<Animator>().SetBool("Open", true);
                foreach (GameObject ele in hangerSet)
                {
                    ele.GetComponent<Animator>().SetTrigger("GetClothSet");
                    if (ele.GetComponentInChildren<WearObject>() != null)
                    {
                        ele.GetComponentInChildren<WearObject>().SetMove();
                    }
                }
                isShirtSetOpen = true;
                closePantsSet();
                closeOtherSet();
                closeHairSet();
            }
            else
            {
                shirtSet.GetComponent<Animator>().SetBool("Open", false);
                foreach (GameObject ele in hangerSet)
                {
                    ele.GetComponent<Animator>().SetTrigger("GetClothSet");
                    if (ele.GetComponentInChildren<WearObject>() != null)
                    {
                        ele.GetComponentInChildren<WearObject>().SetMove();
                    }
                }
                isShirtSetOpen = false;
            }
        }
    }

    public void PantsSetOpen()
    {
        if (!moving)
        {
            moving = true;
            StartCoroutine("WaitSceond");
            SoundManager.PlaySFX(4);
            if (!isPantsSetOpen)
            {
                pantsSet.GetComponent<Animator>().SetBool("Open", true);
                foreach (GameObject ele in pantsHangerSet)
                {
                    ele.GetComponent<Animator>().SetTrigger("GetClothSet");
                    if (ele.GetComponentInChildren<WearObject>() != null)
                    {
                        ele.GetComponentInChildren<WearObject>().SetMove();
                    }
                }
                isPantsSetOpen = true;
                closeShirtSet();
                closeOtherSet();
                closeHairSet();
            }
            else
            {
                pantsSet.GetComponent<Animator>().SetBool("Open", false);
                foreach (GameObject ele in pantsHangerSet)
                {
                    ele.GetComponent<Animator>().SetTrigger("GetClothSet");
                    if (ele.GetComponentInChildren<WearObject>() != null)
                    {
                        ele.GetComponentInChildren<WearObject>().SetMove();
                    }
                }
                isPantsSetOpen = false;
            }
        }
    }

    public void OuterSetOpen()
    {
        SoundManager.PlaySFX(6);
        if (!moving)
        {
            moving = true;
            StartCoroutine("WaitSceond");
            SoundManager.PlaySFX(4);
            if (!isOuterSetOpen)
            {
                outerSet.GetComponent<Animator>().SetBool("Open", true);
                foreach (GameObject ele in outerHangerSet)
                {
                    ele.GetComponent<Animator>().SetTrigger("GetOuterClothSet");
                    if (ele.GetComponentInChildren<WearObject>() != null)
                    {
                        ele.GetComponentInChildren<WearObject>().SetMove();
                    }
                }
                isOuterSetOpen = true;
                closeShirtSet();
                closePantsSet();
                closeHairSet();
            }
            else
            {
                outerSet.GetComponent<Animator>().SetBool("Open", false);
                foreach (GameObject ele in outerHangerSet)
                {
                    ele.GetComponent<Animator>().SetTrigger("GetOuterClothSet");
                    if (ele.GetComponentInChildren<WearObject>() != null)
                    {
                        ele.GetComponentInChildren<WearObject>().SetMove();
                    }
                }
                isOuterSetOpen = false;
            }
        }
    }
    public void HairSetOpen()
    {
        if (!moving)
        {
            moving = true;
            StartCoroutine("WaitSceond");
            SoundManager.PlaySFX(4);
            if (!isHairSetOpen)
            {
                hairSet.GetComponent<Animator>().SetBool("Open", true);
                foreach (GameObject ele in hairHangerSet)
                {
                    ele.GetComponent<Animator>().SetTrigger("GetClothSet");
                    if (ele.GetComponentInChildren<WearObject>() != null)
                    {
                        ele.GetComponentInChildren<WearObject>().SetMove();
                    }
                }
                isHairSetOpen = true;

                closeShirtSet();
                closeOtherSet();
                closePantsSet();
            }
            else
            {
                hairSet.GetComponent<Animator>().SetBool("Open", false);
                foreach (GameObject ele in hairHangerSet)
                {
                    ele.GetComponent<Animator>().SetTrigger("GetClothSet");
                    if (ele.GetComponentInChildren<WearObject>() != null)
                    {
                        ele.GetComponentInChildren<WearObject>().SetMove();
                    }
                }
                isHairSetOpen = false;
            }
        }
    }


    IEnumerator WaitSceond()
    {
        yield return new WaitForSeconds(0.8f);
        moving = false;
    }

    public void CloseAll()
    {
        closeOtherSet();
        closePantsSet();
        closeShirtSet();
        closeHairSet();
    }

    void closeShirtSet()
    {
        if (isShirtSetOpen)
        {
            shirtSet.GetComponent<Animator>().SetBool("Open", false);
            foreach (GameObject ele in hangerSet)
            {
                ele.GetComponent<Animator>().SetTrigger("GetClothSet");
                if (ele.GetComponentInChildren<WearObject>() != null)
                {
                    ele.GetComponentInChildren<WearObject>().SetMove();
                }
            }
            isShirtSetOpen = false;
        }
    }

    void closePantsSet()
    {
        if (isPantsSetOpen)
        {
            pantsSet.GetComponent<Animator>().SetBool("Open", false);
            foreach (GameObject ele in pantsHangerSet)
            {
                ele.GetComponent<Animator>().SetTrigger("GetClothSet");
                if (ele.GetComponentInChildren<WearObject>() != null)
                {
                    ele.GetComponentInChildren<WearObject>().SetMove();
                }
            }
            isPantsSetOpen = false;
        }
    }

    void closeOtherSet()
    {
        if (isOuterSetOpen)
        {
            outerSet.GetComponent<Animator>().SetBool("Open", false);
            foreach (GameObject ele in outerHangerSet)
            {
                ele.GetComponent<Animator>().SetTrigger("GetOuterClothSet");
                if (ele.GetComponentInChildren<WearObject>() != null)
                {
                    ele.GetComponentInChildren<WearObject>().SetMove();
                }
            }
            isOuterSetOpen = false;
        }
    }

    void closeHairSet()
    {
        if (isHairSetOpen)
        {
            hairSet.GetComponent<Animator>().SetBool("Open", false);
            foreach (GameObject ele in hairHangerSet)
            {
                ele.GetComponent<Animator>().SetTrigger("GetClothSet");
                if (ele.GetComponentInChildren<WearObject>() != null)
                {
                    ele.GetComponentInChildren<WearObject>().SetMove();
                }
            }
            isHairSetOpen = false;
        }
    }
}
