using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarAnimation : MonoBehaviour
{
    public GameObject[] go = new GameObject[14];
    public int imageCount, a;
    public bool animationOff;
    public float tickTime = 0.05f;

    private void Start()
    {
        imageCount = 14;
        animationOff = false;

        for (int i = 0; i < imageCount; i++)
        {
            go[i] = transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < imageCount; i++)
        {
            go[i].SetActive(false);
        }

        StartCoroutine(AnimationOn(tickTime));
    }

    public IEnumerator AnimationOn(float tickTime)
    {
        a = 0;
        go[a].SetActive(true);
        yield return new WaitForSeconds(tickTime);

        while (true)
        {
            if (animationOff == true)
            {
                break;
            }

            go[a].SetActive(false);
            
            if (a == imageCount - 1) { a = 0; }
            else { a++; }

            go[a].SetActive(true);

            yield return new WaitForSeconds(tickTime);
        }
        yield return null;
    }
    public void AnimationOff() { animationOff = true; }
}
