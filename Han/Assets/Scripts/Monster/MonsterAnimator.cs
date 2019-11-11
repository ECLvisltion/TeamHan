using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    public MonsterAI ai;
    public Animator animator;

    private void Start()
    {
        ai = transform.parent.GetChild(3).GetComponent<MonsterAI>();
        animator = gameObject.GetComponent<Animator>();
    }

    // setter
    public void SetState(int state)
    {
        animator.SetInteger("State", state);
    }
    public void SetIsHit(bool isHit)
    {
        animator.SetBool("IsHit", isHit);
    }
    public void SetHitFalse() { animator.SetBool("IsHit", false); }

    public void Hit()
    {
        
    }
}
