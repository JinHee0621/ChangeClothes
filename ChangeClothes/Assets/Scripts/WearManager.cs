using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearManager : MonoBehaviour
{
    //public GameObject[] clothes;
    public List<GameObject> clothes;
    public GameObject OtherPos;

    private static bool Catched = false;
    private bool ConfinedMouse = false;
    
    /*
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    */
    public static void changeCatch(bool check)
    {
        Catched = check;
    }

    private void Update()
    {
        if (!Catched)
        {
            if (!ConfinedMouse)
            {
                Cursor.lockState = CursorLockMode.None;
                ConfinedMouse = true;
            }
        } 
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            ConfinedMouse = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for(int i = 0; i < clothes.Count; i++)
        {
            if (clothes[i].gameObject.name.Equals(collision.gameObject.name)){
                if (!collision.GetComponent<WearObject>().checkEquip())
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
        if(collision.GetComponent<WearObject>().checkEquip())
        {
            Debug.Log("out This " + collision.gameObject.name);
            collision.GetComponent<WearObject>().UnEquipped();
            collision.gameObject.transform.SetParent(OtherPos.transform);
            collision.transform.localScale = new Vector3(1, 1);
        }
    }
}
