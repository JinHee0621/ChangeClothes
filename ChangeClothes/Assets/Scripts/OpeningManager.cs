using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    public Image fadeIn;
    public Animator openingSceneAnim;
    private int page = 0;
    private bool animPlay = false;

    public void NextOpening()
    {
        if (!animPlay && page < 5)
        {
            SoundManager.PlaySFX(7);
            animPlay = true;
            openingSceneAnim.SetTrigger("Next");
            page += 1;
            StartCoroutine(WaitAnim());
        } else if(!animPlay && page >= 5)
        {
            fadeIn.gameObject.SetActive(true);
            SoundManager.PlaySFX(7);
            StartCoroutine(SceneChangeFadeIn());
        }
    }

    IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(2f);
        animPlay = false; 
    }

    IEnumerator SceneChangeFadeIn()
    {
        Color nextColor = fadeIn.color;
        nextColor.a += 0.025f;
        fadeIn.color = nextColor;
        yield return new WaitForSeconds(0.05f);
        if (fadeIn.color.a < 1f)
        {
            StartCoroutine(SceneChangeFadeIn());
        }
        else
        {
            OptionManager.instance.GoMain();
            SceneManager.LoadScene("Main");
            yield return null;
        }
    }

}
