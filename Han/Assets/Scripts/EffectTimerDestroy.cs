using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimerDestroy : MonoBehaviour
{
    public float alpha;
    public SpriteRenderer sr;

    private void Start()
    {
        alpha = 1.0f;
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        alpha -= 0.02f;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        if (alpha <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
