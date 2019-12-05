using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackCol : MonoBehaviour
{
    private GameObject monster;
    private Monster monsterScript;

    private void Start()
    {
        monster = transform.parent.gameObject;
        monsterScript = monster.GetComponent<Monster>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHitbox")
        {
            monsterScript.SetMonsterHitbox(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHitbox")
        {
            monsterScript.DeleteMonsterHitbox(collision.gameObject);
        }
    }
}
