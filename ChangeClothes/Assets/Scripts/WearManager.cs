using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearManager : MonoBehaviour
{
    public List<GameObject> clothes;
    public SpriteRenderer char_sprite;
    public Sprite bald_img;
    public Sprite common_img;

    private static WearManager wearManager;
    private static bool catched = false;
    private bool confinedMouse = false;


    private void Awake()
    {
        wearManager = gameObject.GetComponent<WearManager>();
    }


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
                    if (!collision.gameObject.tag.Equals("Hair_change") && !collision.gameObject.tag.Equals("Cat"))
                    {
                        collision.gameObject.transform.SetParent(transform.Find(collision.gameObject.tag));
                        collision.transform.localScale = new Vector3(1, 1);
                        collision.GetComponent<WearObject>().Equipped();
                        break;
                    }

                    if(collision.gameObject.tag.Equals("Cat"))
                    {
                        collision.gameObject.transform.SetParent(transform.Find(collision.gameObject.tag));
                        collision.GetComponent<WearObject>().Equipped();
                        break;
                    }
                }

                if (!collision.GetComponent<WearObject>().CheckChange())
                {
                    if (collision.gameObject.tag.Equals("Hair_change"))
                    {
                        collision.transform.localScale = new Vector3(1, 1);
                        collision.GetComponent<WearObject>().Changed();
                        break;
                    }
                }


            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<WearObject>() != null && collision.GetComponent<WearObject>().CheckEquip())
        {
            if (!collision.gameObject.tag.Equals("Hair_change") && !collision.gameObject.tag.Equals("Cat"))
            {
                collision.GetComponent<WearObject>().UnEquipped();
                collision.gameObject.transform.SetParent(collision.GetComponent<WearObject>().GetThisHanger().transform);
                collision.transform.localScale = new Vector3(1, 1);
            }

            if(collision.gameObject.tag.Equals("Cat"))
            {
                collision.GetComponent<WearObject>().UnEquipped();
                collision.gameObject.transform.SetParent(collision.GetComponent<CatObject>().ReturnFirstPos());
            }
        }

        if(collision.GetComponent<WearObject>() != null && collision.GetComponent<WearObject>().CheckChange())
        {
            if (collision.gameObject.tag.Equals("Hair_change"))
            {
                collision.transform.localScale = new Vector3(1, 1);
                collision.GetComponent<WearObject>().ResetChanged();
            }
        }
    }

    public static void ChangeCharSprite(int flag)
    {
        if(flag == 0)
        {
            wearManager.char_sprite.sprite = wearManager.common_img;
        } else if(flag == 1)
        {
            SoundManager.PlaySFX(17);
            wearManager.char_sprite.sprite = wearManager.bald_img;
            wearManager.transform.GetComponentInParent<CharStateManager>().isbald = true;
        }
    }
}
