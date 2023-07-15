using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    // Start is called before the first frame update
    public bool nowCheck = false;
    float distance = 4;

    private void OnMouseDrag()
    {
        if(nowCheck == false)
        {
            if (gameObject.GetComponent<WearObject>() != null && !gameObject.GetComponent<WearObject>().GetMove())
            {
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                transform.position = objPosition;
            }
        }
    }
}
