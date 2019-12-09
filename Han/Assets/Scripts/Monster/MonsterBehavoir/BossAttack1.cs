using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : MonoBehaviour
{
    public Monster monsterScript;
    public Animator animator;

    private void Start()
    {
        monsterScript = transform.parent.GetComponent<Monster>();
        animator = transform.parent.GetChild(0).GetComponent<Animator>();
    }
    private void Update()
    {
        if (animator.GetInteger("State") != 2)
        {
            animator.SetInteger("State", 2);
            monsterScript.Attack();
        }
    }
}
