using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunsuBellObject : WearObject
{
    private void OnMouseDown()
    {
        if (!gameObject.GetComponent<MouseDrag>().nowCheck)
        {
            if (!move)
            {
                SoundManager.PlaySFX(28);
                if (wearedChange)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = whenWear[0];
                }
            }
        }
    }
}
