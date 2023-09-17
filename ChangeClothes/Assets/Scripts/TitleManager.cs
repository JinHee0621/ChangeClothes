using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    public Image fadeIn;
    public GameObject fadeOut;
    public GameObject closeWindow;

    private void Awake()
    {
        StartCoroutine(removeFadeOut());
    }

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
            OptionManager.instance.optionWindow.SetActive(true);
            OptionManager.instance.GoMain();
            SceneManager.LoadScene("Main");
            yield return null;
        }
    }

    IEnumerator removeFadeOut()
    {
        yield return new WaitForSeconds(5f);
        fadeOut.SetActive(false);
    }

    public void OpenGameExitWindow()
    {
        SoundManager.PlaySFX(7);
        closeWindow.SetActive(true);
    }

    public void GameExit()
    {
        SoundManager.PlaySFX(7);
        fadeIn.enabled = true;
        StartCoroutine(QuitFadeIn());
    }
    IEnumerator QuitFadeIn()
    {
        Color nextColor = fadeIn.color;
        nextColor.a += 0.025f;
        fadeIn.color = nextColor;
        yield return new WaitForSeconds(0.05f);
        if (fadeIn.color.a < 1f)
        {
            StartCoroutine(QuitFadeIn());
        }
        else
        {
            Application.Quit();
            yield return null;
        }
    }

    public void CloseGameExitWindow()
    {
        SoundManager.PlaySFX(7);
        closeWindow.SetActive(false);
    }

}
