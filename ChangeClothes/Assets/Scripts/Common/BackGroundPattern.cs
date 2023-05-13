using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundPattern : MonoBehaviour
{
    private float speed = 2;
    private float xPos = 0;
    [SerializeField] bool checkGameSetOn = false;
    void Start()
    {
        xPos = transform.localPosition.x;
    }

    public void FadeInPattern()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Color defaultColor = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
            defaultColor.a = 0f;
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = defaultColor;
        }
        checkGameSetOn = true;
        StartCoroutine("MovingBackGround");
        for (int i = 0; i < transform.childCount; i++)
        {
            StartCoroutine(FadeIn(transform.GetChild(i).gameObject));
        } 
    }

    IEnumerator FadeIn(GameObject target)
    {
        Color targetColor = target.GetComponent<SpriteRenderer>().color;
        if(targetColor.a >= 1f)
        {
            yield return null;
        } else
        {
            targetColor.a += 0.1f;
            target.GetComponent<SpriteRenderer>().color = targetColor;
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(FadeIn(target));
        }
    }

    IEnumerator MovingBackGround()
    {
        Vector3 nextPos = transform.localPosition;
        if (nextPos.y <= -5.3f)
        {
            nextPos.y = 7f;
            nextPos.x = xPos;
            transform.localPosition = nextPos;
        }

        nextPos.x += speed / 2 * Time.deltaTime;
        nextPos.y -= speed * Time.deltaTime;

        transform.localPosition = nextPos;
        yield return new WaitForSeconds(0.01f);
        if(checkGameSetOn)
        {
            StartCoroutine("MovingBackGround");
        } else
        {
            StopCoroutine("MovingBackGround");
        }
    }

}
