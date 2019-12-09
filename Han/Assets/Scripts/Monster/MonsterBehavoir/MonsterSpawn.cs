using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject monster, mainCamera;
    public Monster monsterScript;
    public Animator animator;

    private void Start()
    {
        monster = transform.parent.gameObject;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        monsterScript = monster.GetComponent<Monster>();
        animator = monster.transform.GetChild(0).GetComponent<Animator>();
    }
    private void Update()
    {
        if (animator.GetInteger("State") != -2) { animator.SetInteger("State", -2); }

        if (monster.transform.position.x > mainCamera.transform.position.x)
        {
            monster.transform.Translate(-monsterScript.GetSpeed() * Time.deltaTime, 0.0f, 0.0f);
            monster.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            monster.transform.Translate(monsterScript.GetSpeed() * Time.deltaTime, 0.0f, 0.0f);
            monster.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
