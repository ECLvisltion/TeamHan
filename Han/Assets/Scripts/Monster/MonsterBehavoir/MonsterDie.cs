using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDie : MonoBehaviour
{
    public GameObject monster;
    public Animator animator;
    public SpriteRenderer monsterColor;
    public int i;

    private void Start()
    {
        monster = transform.parent.gameObject;
        animator = monster.transform.GetChild(0).GetComponent<Animator>();
        monsterColor = monster.transform.GetChild(0).GetComponent<SpriteRenderer>();
        i = 100;
    }
    private void Update()
    {
        if (animator.GetInteger("State") != -1) { animator.SetInteger("State", -1); }

        monsterColor.color = new Color(1.0f, 1.0f, 1.0f, 0.01f * i);
        i--;

        if (i <= 0) { Destroy(monster); }
    }
}
