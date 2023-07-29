using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearManager : MonoBehaviour
{
    public List<GameObject> clothes;

    private static bool catched = false;
    private bool confinedMouse = false;
    
    public static void ChangeCatch(bool check)
    {
        catched = check;
    }

    private void Update()
    {
        if (!catched)
        {
            if (!confinedMouse)
            {
                Cursor.lockState = CursorLockMode.None;
                confinedMouse = true;
            }
        } 
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            confinedMouse = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < clothes.Count; i++)
        {
            if (clothes[i].gameObject.name.Equals(collision.gameObject.name))
            {
                if (!collision.GetComponent<WearObject>().CheckEquip())
                {
                    collision.gameObject.transform.SetParent(transform.Find(collision.gameObject.tag));
                    collision.transform.localScale = new Vector3(1, 1);
                    collision.GetComponent<WearObject>().Equipped();
                    break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<WearObject>() != null && collision.GetComponent<WearObject>().CheckEquip())
        {
            collision.GetComponent<WearObject>().UnEquipped();
            collision.gameObject.transform.SetParent(collision.GetComponent<WearObject>().GetThisHanger().transform);
            collision.transform.localScale = new Vector3(1, 1);
        }
    }
}
