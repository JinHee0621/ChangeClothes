using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public BackgroundButtonManager backgroundBtnUI;
    public ClothSetManager clothSetBtnUI;
    public GameObject optionWindow;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource mouseSFXSource;

    public Slider bgmSlider;
    public Slider sfxSlider;

    private bool optionOpen = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.PlaySFX(7);
            if (!optionOpen)
            {
                backgroundBtnUI.CloseMenuPub();
                clothSetBtnUI.CloseAll();
                optionWindow.SetActive(true);
                optionOpen = true;
            } else
            {
                optionWindow.SetActive(false);
                optionOpen = false;
            }
        }
    }
    public void OpenOptionWindow()
    {
        backgroundBtnUI.CloseMenuPub();
        clothSetBtnUI.CloseAll();
        SoundManager.PlaySFX(7);
        optionWindow.SetActive(true);
        optionOpen = true;
    }


    public void CloseOptionWindow()
    {
        SoundManager.PlaySFX(7);
        optionWindow.SetActive(false);
        optionOpen = false;
    }

    public void BGMValueChange()
    {
        bgmSource.volume = bgmSlider.value * 0.5f;
    }

    public void SFXValueChange()
    {
        sfxSource.volume = sfxSlider.value;
        mouseSFXSource.volume = sfxSlider.value;
    }

}
