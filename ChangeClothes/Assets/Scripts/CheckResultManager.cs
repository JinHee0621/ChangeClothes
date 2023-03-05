using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckResultManager : MonoBehaviour
{
    public GameObject cover1;
    public GameObject cover2;
    public void startCheckResult()
    {
        SoundManager.OffBGM();
        gameObject.SetActive(false);
        cover1.GetComponent<Animator>().SetTrigger("StartCheck");
        cover2.GetComponent<Animator>().SetTrigger("StartCheck");
        SoundManager.PlaySFX(3);
    }

}
