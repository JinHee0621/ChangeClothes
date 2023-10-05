using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    public Image fadeIn;
    public Animator openingSceneAnim;
    public GameObject nextBtn;
    private int page = 0;
    private bool animPlay = false;

    public void NextOpening()
    {
        if (!animPlay && page < 4)
        {
            SoundManager.PlaySFX(7);
            animPlay = true;
            openingSceneAnim.SetTrigger("Next");
            nextBtn.SetActive(false);
            page += 1;
            if(page < 4)
            {
                StartCoroutine(WaitAnim(2.5f));
            } else
            {
                StartCoroutine(WaitAnim(5f));
            }

        } else if(!animPlay && page >= 4)
        {
            fadeIn.gameObject.SetActive(true);
            SoundManager.PlaySFX(7);
            StartCoroutine(SceneChangeFadeIn());
        }
    }

    IEnumerator WaitAnim(float time)
    {
        yield return new WaitForSeconds(time);
        animPlay = false;
        nextBtn.SetActive(true);
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
