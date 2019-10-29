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

    public void Hit(int combo)
    {
        saScript.Hit();
        if (combo == 4) { Instantiate(Resources.Load("Prefabs/Effect/HitF_Strong_00"), new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f)); }
        else { Instantiate(Resources.Load("Prefabs/Effect/HitF_Week_00"), new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))); }
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
