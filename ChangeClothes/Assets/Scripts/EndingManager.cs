using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndingManager : MonoBehaviour
{
    public Image fadeIn;
    public Animator endimgAnim;
    public SoundManager soundManager;

    void Start()
    {
        endimgAnim.SetTrigger("FadeOutEnd");
        StartCoroutine(EndingSFX());
    }
    IEnumerator EndingSFX()
    {
        yield return new WaitForSeconds(0.5f);
        SoundManager.PlaySFX(26);
        yield return new WaitForSeconds(1.5f);
        SoundManager.PlaySFX(26);
        yield return new WaitForSeconds(1.2f);
        SoundManager.PlaySFX(26);
        yield return new WaitForSeconds(1.0f);
        SoundManager.PlaySFX(15);
        yield return new WaitForSeconds(1.0f);
        SoundManager.PlayBGM();
        yield return new WaitForSeconds(54.0f);
        soundManager.BGMOff();
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(SceneChangeFadeIn());
    }

    IEnumerator SceneChangeFadeIn()
    {
        Color nextColor = fadeIn.color;
        nextColor.a += 0.025f;
        fadeIn.color = nextColor;
        yield return new WaitForSeconds(0.05f);
        if (fadeIn.color.a < 1f)
        {
            StartCoroutine(SceneChangeFadeIn());
        }
        else
        {
            if (!DataManager.viewEnding)
            {
                DataManager.viewEnding = true;
                DataManager.SaveFile();
            }
            SceneManager.LoadScene("Title");
            OptionManager.instance.GoTitle();
            yield return null;
        }
    }
}
