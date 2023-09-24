using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{

    public static OptionManager instance = null;

    public UIMovingManager uiMovingManager;

    public BackgroundButtonManager backgroundBtnUI;
    public ClothSetManager clothSetBtnUI;
    public GameObject optionWindow;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource sfxSource2;
    public AudioSource mouseSFXSource;

    public Slider bgmSlider;
    public Slider sfxSlider;
    public Button goTitleBtn;
    
    public bool nowTitle;
    public bool nowCheckResult;

    public float bgmSize;
    public float sfxSize;

    public bool optionOpen;

    public Dropdown screenSizeOption;
    public Text screenSize;
    Dropdown screenLocalOption;
    string selectedScreenSize = "1600 x 900 ";

    public GameObject titleWindow;
    public Button returnTitleBtn;
    public Button closeTitleBtn;
    public bool titleOpen;

    private Button optionBtn;
    private Button optionCloseBtn;

    private void Awake()
    {
        if (instance == null)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.SetResolution(1600, 900, false);
            instance = this;
            nowTitle = true;
            DontDestroyOnLoad(this.gameObject);
            SoundValueInit();
            screenLocalOption = screenSizeOption;
            optionWindow.SetActive(false);
        } else
        {
            if (instance != this) Destroy(this.gameObject);
        }
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
        //메인 씬으로 이동 후 
        yield return new WaitForSeconds(0.025f);
        nowTitle = false;
        optionOpen = false;
        uiMovingManager = GameObject.Find("UIManager").GetComponent<UIMovingManager>();
        backgroundBtnUI = GameObject.Find("deco_box_btn").GetComponent<BackgroundButtonManager>();
        clothSetBtnUI = GameObject.Find("SetButton").GetComponent<ClothSetManager>();
        optionWindow = GameObject.Find("OptionSet");
        bgmSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        goTitleBtn = GameObject.Find("TitleBtn").GetComponent<Button>();
        goTitleBtn.onClick.AddListener(OpenReturnTitleWindow);

        sfxSource = GameObject.Find("EffectMusic").GetComponent<AudioSource>();
        sfxSource2 = GameObject.Find("EffectMusic2").GetComponent<AudioSource>();
        mouseSFXSource = GameObject.Find("MouseSFX").GetComponent<AudioSource>();
        bgmSlider = GameObject.Find("BGMSize").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFXSize").GetComponent<Slider>();

        screenSizeOption = GameObject.Find("ScreenSize").GetComponent<Dropdown>();
        screenSize = GameObject.Find("ScreenLabel").GetComponent<Text>();
        screenSize.text = selectedScreenSize;
        screenSizeOption.onValueChanged.AddListener(SelectScreenSize);
        screenSizeOption.value = screenLocalOption.value;

        titleWindow = GameObject.Find("TitleReturnSet");
        returnTitleBtn = GameObject.Find("ReturnTitleYes").GetComponent<Button>();
        closeTitleBtn = GameObject.Find("ReturnTitleNo").GetComponent<Button>();
        returnTitleBtn.onClick.AddListener(GoReturnTitle);
        closeTitleBtn.onClick.AddListener(CloseReturnTitle);

        bgmSlider.onValueChanged.AddListener(BGMValueChange);
        sfxSlider.onValueChanged.AddListener(SFXValueChange);

        SoundValueInit();
        optionWindow.SetActive(false);
        titleWindow.SetActive(false);
    }

    public void GoTitle()
    {
        StartCoroutine(WaitReturnScene());
    }

    IEnumerator WaitReturnScene()
    {
        yield return new WaitForSeconds(0.025f);
        //타이틀 씬으로 이동 후 
        optionOpen = false;
        nowTitle = true;
        optionWindow = GameObject.Find("OptionSet");
        bgmSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();

        sfxSource = GameObject.Find("EffectMusic").GetComponent<AudioSource>();
        sfxSource2 = GameObject.Find("EffectMusic2").GetComponent<AudioSource>();
        mouseSFXSource = GameObject.Find("MouseSFX").GetComponent<AudioSource>();
        bgmSlider = GameObject.Find("BGMSize").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFXSize").GetComponent<Slider>();

        screenSizeOption = GameObject.Find("ScreenSize").GetComponent<Dropdown>();
        screenSize = GameObject.Find("ScreenLabel").GetComponent<Text>();
        screenSize.text = selectedScreenSize;
        screenSizeOption.onValueChanged.AddListener(SelectScreenSize);
        screenSizeOption.value = screenLocalOption.value;

        bgmSlider.onValueChanged.AddListener(BGMValueChange);
        sfxSlider.onValueChanged.AddListener(SFXValueChange);

        optionBtn = GameObject.Find("OptionBtn").GetComponent<Button>();
        optionBtn.onClick.AddListener(TitleOpenOption);

        optionCloseBtn = GameObject.Find("CloseBtn").GetComponent<Button>();
        optionCloseBtn.onClick.AddListener(TitleCloseOption);

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
        sfxSource2.volume = sfxSize;
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
        sfxSource2.volume = sfxSize;
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
    public void SelectScreenSize()
    {
        if (screenSizeOption.value == 3)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.SetResolution(1600, 900, true);
        } else
        {

            screenLocalOption.value = screenSizeOption.value;
            selectedScreenSize = screenSize.text;
            int xSize = int.Parse(screenSize.text.Substring(0, 5).Trim());
            int ySize = int.Parse(screenSize.text.Substring(6, 5).Trim());

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.SetResolution(xSize, ySize, false);
        }

    }


    private void SelectScreenSize(int arg0)
    {
        if (screenSizeOption.value == 3)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.SetResolution(1600, 900, true);
        }
        else
        {
            screenLocalOption.value = screenSizeOption.value;
            selectedScreenSize = screenSize.text;
            int xSize = int.Parse(screenSize.text.Substring(0, 5).Trim());
            int ySize = int.Parse(screenSize.text.Substring(6, 5).Trim());

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.SetResolution(xSize, ySize, false);
        }
    }

    public void OpenReturnTitleWindow()
    {
        SoundManager.PlaySFX(7);
        titleWindow.SetActive(true);
        instance.titleOpen = true;
    }


    public void CloseReturnTitleWindow()
    {
        SoundManager.PlaySFX(7);
        titleWindow.SetActive(false);
        instance.titleOpen = false;
    }
    private void GoReturnTitle()
    {
        SoundManager.PlaySFX(7);
        uiMovingManager.GoTitle();
    }
    private void CloseReturnTitle()
    {
        CloseReturnTitleWindow();
    }
}
