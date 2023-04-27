using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUIManager : MonoBehaviour
{
    public GameObject conditionGuage;
    public GameObject tensionGuage;
    public GameObject statusSet;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(conditionGuage.GetComponent<Image>().fillAmount);
        Debug.Log(tensionGuage.GetComponent<Image>().fillAmount);
    }

    public void ReSetGuage()
    {

    }

    IEnumerator SetStatGuageRunning()
    {

        yield return null;
    }
}
