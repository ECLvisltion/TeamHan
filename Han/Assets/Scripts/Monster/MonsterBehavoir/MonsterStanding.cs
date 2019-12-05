using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStanding : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = transform.parent.GetChild(0).GetComponent<Animator>();
    }
    private void Update()
    {
        if (animator.GetInteger("State") != 7) { animator.SetInteger("State", 7); }
    }
}
