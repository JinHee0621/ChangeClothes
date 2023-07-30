using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    public Image fadeIn;
    public void GoToMain()
    {
        SoundManager.PlaySFX(5);
        fadeIn.enabled = true;
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
            OptionManager.instance.GoMain();
            SceneManager.LoadScene("Main");
            yield return null;
        }
    }
}
