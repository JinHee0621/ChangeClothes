using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StateGuage : MonoBehaviour
{
    public GameObject state_owner;
    public GameObject condition_guage;
    public GameObject tenstion_guage;
    public GameObject mental_guage;
    public GameObject state_window;

    private Image condition_Img;
    private Image tension_Img;
    private Image mental_Img;
    private bool state_window_activated = false;
    void Start()
    {
        condition_Img = condition_guage.GetComponent<Image>();
        tension_Img = tenstion_guage.GetComponent<Image>();
        mental_Img = mental_guage.GetComponent<Image>();
        SetGuageState();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            if(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z)).x < 0)
            {
                if (!state_window_activated)
                {
                    state_window.SetActive(true);
                    state_window_activated = true;
                }
            }
        } 
        else
        {
            if (state_window_activated)
            {
                state_window.SetActive(false);
                state_window_activated = false;
            }
        }
        new WaitForSeconds(0.05f);
    }

    public void SetGuageState()
    {
        condition_Img.fillAmount = state_owner.GetComponent<CharStateManager>().condition / 100f;
        tension_Img.fillAmount = state_owner.GetComponent<CharStateManager>().tension / 100f;
        mental_Img.fillAmount = state_owner.GetComponent<CharStateManager>().mental / 100f;
    }

}
