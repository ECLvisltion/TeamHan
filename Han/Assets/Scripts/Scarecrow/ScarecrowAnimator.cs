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
    }
    public void Hit()
    {
        animator.SetInteger("State", 1);
    }
}
