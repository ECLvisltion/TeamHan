using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    private GameObject sa;
    private ScarecrowAnimator saScript;

    private void Start()
    {
        sa = transform.GetChild(0).gameObject;
        saScript = gameObject.GetComponentInChildren<ScarecrowAnimator>();
    }

    public void Hit(int combo, KeyCode key)
    {
        saScript.Hit(combo);
        if (combo == 3)
        {
            GameObject go;
            go = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/HitF_Strong0100"), new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            if (key == KeyCode.A) { go.transform.localScale = new Vector3(go.transform.localScale.x * -1.0f, go.transform.localScale.y, 1.0f); }
            if (key == KeyCode.D) { go.transform.localScale = new Vector3(go.transform.localScale.x * +1.0f, go.transform.localScale.y, 1.0f); }
        }
        else
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Effect/HitF_Week0100"), new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        }
        StartCoroutine(Shake());
    }
    public void UnderHit()
    {
        saScript.UnderHit();
        Instantiate(Resources.Load("Prefabs/Effect/HitF_Week0100"), new Vector3(transform.position.x, transform.position.y - 1.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        sa.transform.Translate(0.1f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        sa.transform.Translate(-0.2f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        sa.transform.Translate(0.2f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        sa.transform.Translate(-0.2f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        sa.transform.Translate(0.15f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        sa.transform.Translate(-0.10f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        sa.transform.Translate(0.05f, 0.0f, 0.0f);
        yield return null;
    }
}
