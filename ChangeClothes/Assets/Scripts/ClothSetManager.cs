using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothSetManager : MonoBehaviour
{
    public GameObject shirtSet;
    public GameObject pantsSet;
    public GameObject outerSet;

    GameObject[] hangerSet;
    GameObject[] pantsHangerSet;
    GameObject[] outerHangerSet;


    private bool isShirtSetOpen = false;
    private bool isPantsSetOpen = false;
    private bool isOuterSetOpen = false;

    private bool moving = false;


    private void Start()
    {
        hangerSet = GameObject.FindGameObjectsWithTag("Hanger");
        pantsHangerSet = GameObject.FindGameObjectsWithTag("Hanger_Pants");
        outerHangerSet = GameObject.FindGameObjectsWithTag("Hanger_Outer");
    }

    public void ShirtSetOpen()
    {
        if(!moving)
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

    IEnumerator WaitSceond()
    {
        yield return new WaitForSeconds(0.8f);
        moving = false;
    }

}
