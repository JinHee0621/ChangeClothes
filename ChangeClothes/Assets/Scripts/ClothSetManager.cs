using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothSetManager : MonoBehaviour
{
    public GameObject shirtSet;
    public GameObject pantsSet;
    GameObject[] hangerSet;
    GameObject[] pantsHangerSet;


    private bool isShirtSetOpen = false;
    private bool isPantsSetOpen = false;

    private void Start()
    {
        hangerSet = GameObject.FindGameObjectsWithTag("Hanger");
        pantsHangerSet = GameObject.FindGameObjectsWithTag("Hanger_Pants");
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
}
