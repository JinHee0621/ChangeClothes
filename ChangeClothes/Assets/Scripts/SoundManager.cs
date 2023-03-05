using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> SFX_List;
    public List<AudioClip> BGM_List;
    static AudioSource SFX_Sound;
    static AudioSource BGM_Sound;
    static public List<AudioClip> SFX;
    // Start is called before the first frame update
    void Start()
    {
        SFX = SFX_List;
        SFX_Sound = transform.Find("EffectMusic").gameObject.GetComponent<AudioSource>();
        BGM_Sound = transform.Find("BackgrounddMusic").gameObject.GetComponent<AudioSource>();

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

    IEnumerator FadeInBGM(AudioSource source)
    {
        source.volume += 0.01f;
        if(source.volume < 0.25f)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FadeInBGM(source));
        } else
        {
            Debug.Log("end");
        }
    }
}
