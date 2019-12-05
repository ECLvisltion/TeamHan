using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = transform.parent.GetChild(0).GetComponent<Animator>();
    }
    private void Update()
    {
        if (animator.GetInteger("State") != 0) { animator.SetInteger("State", 0); }
    }
}
