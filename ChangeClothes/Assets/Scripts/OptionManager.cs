using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{

    public static OptionManager instance = null;

    public BackgroundButtonManager backgroundBtnUI;
    public ClothSetManager clothSetBtnUI;
    public GameObject optionWindow;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource mouseSFXSource;

    public Slider bgmSlider;
    public Slider sfxSlider;
    
    public bool nowTitle;
    public bool nowCheckResult;

    public float bgmSize;
    public float sfxSize;

    public bool optionOpen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            nowTitle = true;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            if (instance != this) Destroy(this.gameObject);
        }
        SoundValueInit();
    }

    void Update()
    {
        if (!nowTitle)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && nowCheckResult == false)
            {
                SoundManager.PlaySFX(7);
                if (!optionOpen)
                {
                    backgroundBtnUI.CloseMenuPub();
                    clothSetBtnUI.CloseAll();
                    optionWindow.SetActive(true);
                    instance.optionOpen = true;
                }
                else
                {
                    optionWindow.SetActive(false);
                    instance.optionOpen = false;
                }
            }
        }
    }


    public void GoMain()
    {
        StartCoroutine(WaitChangeScene());
    }

    IEnumerator WaitChangeScene()
    {
        yield return new WaitForSeconds(0.05f);
        nowTitle = false;
        backgroundBtnUI = GameObject.Find("deco_box_btn").GetComponent<BackgroundButtonManager>();
        clothSetBtnUI = GameObject.Find("SetButton").GetComponent<ClothSetManager>();
        optionWindow = GameObject.Find("OptionSet");
        bgmSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        sfxSource = GameObject.Find("EffectMusic").GetComponent<AudioSource>();
        mouseSFXSource = GameObject.Find("MouseSFX").GetComponent<AudioSource>();
        bgmSlider = GameObject.Find("BGMSize").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFXSize").GetComponent<Slider>();

        bgmSlider.onValueChanged.AddListener(BGMValueChange);
        sfxSlider.onValueChanged.AddListener(SFXValueChange);

        SoundValueInit();
        optionWindow.SetActive(false);
    }

    private void BGMValueChange(float arg0)
    {
        bgmSize = bgmSlider.value;
        bgmSource.volume = bgmSize * 0.5f;
    }

    private void SFXValueChange(float arg0)
    {
        sfxSize = sfxSlider.value;
        sfxSource.volume = sfxSize;
        mouseSFXSource.volume = sfxSize;

    }
    public void BGMValueChange()
    {
        bgmSize = bgmSlider.value;
        bgmSource.volume = bgmSize * 0.5f;
    }

    void SFXValueChange()
    {
        sfxSize = sfxSlider.value;
        sfxSource.volume = sfxSize;
        mouseSFXSource.volume = sfxSize;
    }


    public void ReturnTitle()
    {
        nowTitle = true;
    }


    public void TitleOpenOption()
    {
        SoundManager.PlaySFX(7);
        optionWindow.SetActive(true);
        instance.optionOpen = true;
    }

    public void TitleCloseOption()
    {
        SoundManager.PlaySFX(7);
        optionWindow.SetActive(false);
        instance.optionOpen = false;
    }

    public void OpenOptionWindow()
    {
        backgroundBtnUI.CloseMenuPub();
        clothSetBtnUI.CloseAll();
        SoundManager.PlaySFX(7);
        optionWindow.SetActive(true);
        instance.optionOpen = true;
    }

    public void CloseOptionWindow()
    {
        SoundManager.PlaySFX(7);
        optionWindow.SetActive(false);
        instance.optionOpen = false;
    }


    void SoundValueInit()
    {
        bgmSource.volume = bgmSize * 0.5f;
        sfxSource.volume = sfxSize;
        mouseSFXSource.volume = sfxSize;

        sfxSlider.value = sfxSize;
        bgmSlider.value = bgmSize;
    }

}
