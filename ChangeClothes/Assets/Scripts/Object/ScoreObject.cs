using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreObject : MonoBehaviour
{
    public CharStateManager charScore;
    public int likeScore;

    Vector3 firstPos;
    public void StartMove()
    {
        firstPos = transform.localPosition;
        System.Random randomPos = new System.Random();
        gameObject.transform.position = new Vector3(firstPos.x, firstPos.y + ((float)randomPos.NextDouble()/10));
        StartCoroutine(AfterMove(randomPos.NextDouble()));
    }

    IEnumerator AfterMove(double time)
    {
        yield return new WaitForSeconds((float)time);
        gameObject.transform.DOLocalMove(new Vector3(-381f, 395f, 0f), 1.5f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(1.5f);
        charScore.viewer_Like += likeScore;
        EndMove();
    }

    public void EndMove()
    {
        gameObject.transform.position = firstPos;
        gameObject.SetActive(false);
    }
}
