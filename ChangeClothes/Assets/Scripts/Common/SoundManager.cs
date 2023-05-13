using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> SFX_List;
    public List<AudioClip> BGM_List;
    static AudioSource Mouse_Sound;
    static AudioSource SFX_Sound;
    static AudioSource BGM_Sound;
    static AudioSource Condition_SFX;
    static AudioSource Mental_SFX;
    static public List<AudioClip> SFX;

    void Start()
    {
        SFX = SFX_List;
        Mouse_Sound = transform.Find("MouseSFX").gameObject.GetComponent<AudioSource>();
        SFX_Sound = transform.Find("EffectMusic").gameObject.GetComponent<AudioSource>();
        BGM_Sound = transform.Find("BackgrounddMusic").gameObject.GetComponent<AudioSource>();
        Condition_SFX = transform.Find("ConditionGuageSFX").gameObject.GetComponent<AudioSource>();
        Mental_SFX = transform.Find("MentalGuageSFX").gameObject.GetComponent<AudioSource>();
        BGM_Sound.Play();
        StartCoroutine(FadeInBGM(BGM_Sound));
    }
    static public void OffBGM()
    {
        BGM_Sound.Stop();
    }

    static public void PlaySFX(int code)
    {
        SFX_Sound.clip = SFX[code];
        SFX_Sound.Play();
    }

    static public void MouseSFX(int code)
    {
        Mouse_Sound.clip = SFX[code];
        Mouse_Sound.Play();
    }


    static public void PlayConditionSound()
    {
        Condition_SFX.Play();
    }
    static public void PlayMentalSound()
    {
        Mental_SFX.Play();
    }


    IEnumerator FadeInBGM(AudioSource source)
    {
        source.volume += 0.005f;
        if(source.volume < 0.18f)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FadeInBGM(source));
        }
    }
}
