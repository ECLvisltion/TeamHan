using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3Script : MonoBehaviour
{
    public GameObject[] monster = new GameObject[16];
    public int skill1dmg;

    private void Start()
    {
        skill1dmg = 3;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MonsterHitbox")
        {
            for (int i = 0; i < 16; i++)
            {
                if (monster[i] == null)
                {
                    monster[i] = collision.gameObject;
                    return;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MonsterHitbox")
        {
            for (int i = 0; i < 16; i++)
            {
                if (monster[i] == collision.gameObject)
                {
                    monster[i] = null;
                    return;
                }
            }
        }
    }

    private void Damage()
    {
        foreach (GameObject go in monster)
        {
            if (go != null)
            {
                Monster m = go.transform.parent.GetComponent<Monster>();
                m.SetHit(3);
                m.SetHPDecrease(skill1dmg);
            }
        }
    }
}
