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


    private void Start()
    {
        hangerSet = GameObject.FindGameObjectsWithTag("Hanger");
        pantsHangerSet = GameObject.FindGameObjectsWithTag("Hanger_Pants");
        outerHangerSet = GameObject.FindGameObjectsWithTag("Hanger_Outer");
    }

    public void ShirtSetOpen()
    {
        SoundManager.PlaySFX(4);
        if (!isShirtSetOpen)
        {
            shirtSet.GetComponent<Animator>().SetBool("Open", true);
            foreach(GameObject ele in hangerSet)
            {
                ele.GetComponent<Animator>().SetTrigger("GetClothSet");
            }
            isShirtSetOpen = true;
        } else
        {
            shirtSet.GetComponent<Animator>().SetBool("Open", false);
            foreach (GameObject ele in hangerSet)
            {
                ele.GetComponent<Animator>().SetTrigger("GetClothSet");
            }
            isShirtSetOpen = false;
        }
    }

    public void PantsSetOpen()
    {
        SoundManager.PlaySFX(4);
        if (!isPantsSetOpen)
        {
            pantsSet.GetComponent<Animator>().SetBool("Open", true);
            foreach (GameObject ele in pantsHangerSet)
            {
                ele.GetComponent<Animator>().SetTrigger("GetClothSet");
            }
            isPantsSetOpen = true;
        }
        else
        {
            pantsSet.GetComponent<Animator>().SetBool("Open", false);
            foreach (GameObject ele in pantsHangerSet)
            {
                ele.GetComponent<Animator>().SetTrigger("GetClothSet");
            }
            isPantsSetOpen = false;
        }
    }

    public void OuterSetOpen()
    {
        SoundManager.PlaySFX(4);
        if (!isOuterSetOpen)
        {
            outerSet.GetComponent<Animator>().SetBool("Open", true);
            foreach (GameObject ele in outerHangerSet)
            {
                ele.GetComponent<Animator>().SetTrigger("GetOuterClothSet");
            }
            isOuterSetOpen = true;
        }
        else
        {
            outerSet.GetComponent<Animator>().SetBool("Open", false);
            foreach (GameObject ele in outerHangerSet)
            {
                ele.GetComponent<Animator>().SetTrigger("GetOuterClothSet");
            }
            isOuterSetOpen = false;
        }
    }

    IEnumerator waitSceond()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
