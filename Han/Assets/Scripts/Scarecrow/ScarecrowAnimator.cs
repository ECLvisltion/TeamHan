using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Idle()
    {
        animator.SetInteger("State", 0);
        animator.SetInteger("HitCombo", 0);
    }
    public void Hit(int combo)
    {
        animator.SetInteger("State", 1);
        animator.SetInteger("HitCombo", combo);
    }
    public void UnderHit()
    {
        animator.SetInteger("State", 1);
        animator.SetInteger("HitCombo", -1);
    }
}
