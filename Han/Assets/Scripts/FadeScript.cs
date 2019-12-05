using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Image image;
    public float fadeTickTime, fadeAlpha;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        fadeTickTime = 0.01f;
        fadeAlpha = 0.00f;

        image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp)) { StartCoroutine(FadeOut()); }
        if (Input.GetKeyDown(KeyCode.PageDown)) { StartCoroutine(FadeIn()); }
        if (Input.GetKeyDown(KeyCode.Home)) { StartCoroutine(FadeOut(5, 0.0f, 0.4f)); }
        if (Input.GetKeyDown(KeyCode.End)) { StartCoroutine(FadeIn(5, 0.4f, 0.0f)); }
    }

    // getter
    public float GetFadeTickTime() { return fadeTickTime; }

    public IEnumerator FadeOut()
    {
        fadeAlpha = 0.0f;
        image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
        for (int i = 0; i < 50; i++)
        {
            fadeAlpha = i * 0.02f;
            image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            yield return new WaitForSeconds(fadeTickTime);
        }
        fadeAlpha = 1.0f;
        image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
        yield return null;
    }
    public IEnumerator FadeOut(int frame, float startAlpha, float finalAlpha)
    {
        fadeAlpha = startAlpha;
        image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
        for (int i = 0; i < frame; i++)
        {
            fadeAlpha = i * ((finalAlpha - startAlpha) / frame);
            image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            yield return new WaitForSeconds(fadeTickTime);
        }
        fadeAlpha = finalAlpha;
        image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
        yield return null;
    }
    public IEnumerator FadeIn()
    {
        fadeAlpha = 1.0f;
        image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
        for (int i = 0; i < 50; i++)
        {
            fadeAlpha = 1.0f - (i * 0.02f);
            image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            yield return new WaitForSeconds(fadeTickTime);
        }
        fadeAlpha = 0.0f;
        image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
        yield return null;
    }
    public IEnumerator FadeIn(int frame, float startAlpha, float finalAlpha)
    {
        fadeAlpha = startAlpha;
        image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
        for (int i = 0; i < frame; i++)
        {
            fadeAlpha = startAlpha - (i * ((startAlpha - finalAlpha) / frame));
            image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
            yield return new WaitForSeconds(fadeTickTime);
        }
        fadeAlpha = finalAlpha;
        image.color = new Color(0.0f, 0.0f, 0.0f, fadeAlpha);
        yield return null;
    }
}
