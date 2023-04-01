using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothSetManager : MonoBehaviour
{
    public GameObject shirtSet;
    public GameObject pantsSet;
    GameObject[] hangerSet;

    private bool isShirtSetOpen = false;

    private void Start()
    {
        hangerSet = GameObject.FindGameObjectsWithTag("Hanger");
    }

    public void ShirtSetOpen()
    {
        if(!isShirtSetOpen)
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

}
