using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Text;
using System.IO;

public class TitleManager : MonoBehaviour
{
    public Image fadeIn;
    public SoundManager soundManager;
    public GameObject fadeOut;
    public GameObject closeWindow;
    string filePath = "SaveData/data";


    private void Awake()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(removeFadeOut());
    }

    private void Start()
    {
        soundManager.BGMOn();
    }

    public void GoToMain()
    {
        if (!File.Exists(filePath))
        {
            SoundManager.PlaySFX(5);
            fadeIn.enabled = true;
            StartCoroutine(SceneChangeFadeIn("Opening"));
        }
        else
        {
            SoundManager.PlaySFX(5);
            fadeIn.enabled = true;
            StartCoroutine(SceneChangeFadeIn("Main"));
        }
    }
    IEnumerator SceneChangeFadeIn(string sceneName)
    {
        Color nextColor = fadeIn.color;
        nextColor.a += 0.025f;
        fadeIn.color = nextColor;
        yield return new WaitForSeconds(0.05f);
        if (fadeIn.color.a < 1f)
        {
            StartCoroutine(SceneChangeFadeIn(sceneName));
        }
        else
        {
            if(sceneName.Equals("Main"))
            {
                OptionManager.instance.GoMain();
                OptionManager.instance.optionWindow.SetActive(true);
            }
            SceneManager.LoadScene(sceneName);
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
