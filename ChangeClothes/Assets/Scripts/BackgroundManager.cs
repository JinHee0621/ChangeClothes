using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundManager : MonoBehaviour
{
    public ParticleSystem trasureEffect;
    public Transform trasureUIPos;
    public GameObject trasureObject;
    public GameObject backScreen;
    public Transform targetPos;


    private void OnMouseDown()
    {
        if (!DataManager.dataTrasure)
        {
            DataManager.dataTrasure = true;
            StartCoroutine(TrasureObjectMov());
        }
    }

    IEnumerator TrasureObjectMov()
    {
        SoundManager.PlaySFX(25);
        trasureObject.SetActive(true);
        backScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        trasureObject.transform.DOMoveX(targetPos.localPosition.x, 1.5f).SetEase(Ease.OutQuad);
        trasureObject.transform.DOMoveY(targetPos.localPosition.y, 1.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(2f);
        trasureObject.SetActive(false);

        SoundManager.PlaySFX(24);

        ParticleSystem effect = Instantiate(trasureEffect, trasureUIPos) as ParticleSystem;
        effect.transform.position = trasureUIPos.position;
        Destroy(effect.gameObject, 1f);
        yield return new WaitForSeconds(1f);
        backScreen.SetActive(false);
    }
}
