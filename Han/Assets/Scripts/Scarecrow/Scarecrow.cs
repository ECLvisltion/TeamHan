using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    private ScarecrowAnimator sa;

    private void Start()
    {
        sa = gameObject.GetComponentInChildren<ScarecrowAnimator>();
    }

    public void Hit()
    {
        sa.Hit();
    }
}
