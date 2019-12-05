using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject monsterImage;
    public MonsterAI monsterAI;
    public Animator animator;
    public GameObject[] player = new GameObject[1];

    public int hp, damage;
    public float speed;

    private void Start()
    {
        monsterImage = transform.GetChild(0).gameObject;
        monsterAI = transform.GetChild(3).GetComponent<MonsterAI>();
        animator = transform.GetChild(0).GetComponent<Animator>();

        hp = 10;
        damage = 10;
        speed = 0.05f;
    }

    public void Attack()
    {
        foreach (GameObject go in player)
        {
            if (go != null)
            {
                Player p = go.transform.parent.GetComponent<Player>();
                if (p != null)
                {
                    p.Hit(damage);
                }
            }
        }
    }

    public void SetMonsterHitbox(GameObject player)
    {
        for (int i = 0; i < 1; i++)
        {
            if (this.player[i] == null)
            {
                this.player[i] = player;
                return;
            }
        }
    }
    public void DeleteMonsterHitbox(GameObject player)
    {
        for (int i = 0; i < 1; i++)
        {
            if (this.player[i] == player)
            {
                this.player[i] = null;
                return;
            }
        }
    }

    public IEnumerator Shake()
    {
        monsterImage.transform.Translate(0.1f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        monsterImage.transform.Translate(-0.2f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        monsterImage.transform.Translate(0.1f, 0.0f, 0.0f);
        yield return null;
    }

    // setter
    public void SetHPDecrease(int decrease)
    {
        hp -= decrease;
    }
    /// <summary>
    /// 1~3 : 콤보, 4 : 아래공격
    /// </summary>
    public void SetHit(int combo)
    {
        monsterAI.SetAttackCombo(combo);
    }
    // getter
    public int GetHP() { return hp; }
    public float GetSpeed() { return speed; }
    public int GetMonsterState() { return monsterAI.GetState(); }
}
