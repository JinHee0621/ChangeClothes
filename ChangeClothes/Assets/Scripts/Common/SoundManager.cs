using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> SFX_List;
    public List<AudioClip> BGM_List;
    static AudioSource Mouse_Sound;
    static AudioSource SFX_Sound;
    static AudioSource SFX_Sound2;
    public AudioSource BGM_Sound_pub;
    static AudioSource BGM_Sound;
    static public List<AudioClip> SFX;

    void Start()
    {
        SFX = SFX_List;
        Mouse_Sound = transform.Find("MouseSFX").gameObject.GetComponent<AudioSource>();
        SFX_Sound = transform.Find("EffectMusic").gameObject.GetComponent<AudioSource>();
        SFX_Sound2 = transform.Find("EffectMusic2").gameObject.GetComponent<AudioSource>();
        BGM_Sound = transform.Find("BackgroundMusic").gameObject.GetComponent<AudioSource>();

    }
    static public void OffBGM()
    {
        BGM_Sound.Stop();
    }

    public static void PlayBGM()
    {
        BGM_Sound.volume = OptionManager.ReturnBGMVolumeSize();
        BGM_Sound.Play();
    }

    static public void PlaySFX(int code)
    {
        SFX_Sound.clip = SFX[code];
        SFX_Sound.Play();
    }
    static public void PlaySFX2(int code)
    {
        SFX_Sound2.clip = SFX[code];
        SFX_Sound2.Play();
    }


    static public void MouseSFX(int code)
    {
        Mouse_Sound.clip = SFX[code];
        Mouse_Sound.Play();
    }

    public void BGMOn()
    {
        BGM_Sound_pub.Play();
        BGM_Sound_pub.volume = 0f;
        StartCoroutine(FadeInBGM(BGM_Sound_pub));
    }

    IEnumerator FadeInBGM(AudioSource source)
    {
        source.volume += 0.05f;
        if(source.volume < OptionManager.ReturnBGMVolumeSize())
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FadeInBGM(source));
        }
    }

    public void BGMOff()
    {
        StartCoroutine(FadeOutBGM(BGM_Sound_pub));
    }

    IEnumerator FadeOutBGM(AudioSource source)
    {
        source.volume -= 0.05f;
        if (source.volume > 0f)
        {
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(FadeOutBGM(source));
        }
    }
}
