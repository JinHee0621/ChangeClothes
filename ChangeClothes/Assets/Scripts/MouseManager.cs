using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Touch are screens location. Convert to world
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Clicked");
            // Effect for feedback
            SpecialEffectsScript.MakeExplosion((position));
        }
        /*
        // Look for all fingers
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // -- Tap: quick touch & release
            // ------------------------------------------------
            if (touch.phase == TouchPhase.Ended && touch.tapCount == 1)
            {

            }
        }*/
    }
}
