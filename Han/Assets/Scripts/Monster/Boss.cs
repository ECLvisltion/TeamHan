using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject monsterImage;
    public BossAI bossAI;
    public Animator animator;
    public GameObject[] player = new GameObject[1];

    public int hp, damage;
    public float speed;

    private void Start()
    {
        monsterImage = transform.GetChild(0).gameObject;
        bossAI = transform.GetChild(3).GetComponent<BossAI>();
        animator = transform.GetChild(0).GetComponent<Animator>();

        hp = 30;
        damage = 30;
        speed = 0.03f;
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
        bossAI.SetAttackCombo(combo);
    }
    // getter
    public int GetHP() { return hp; }
    public float GetSpeed() { return speed; }
    public int GetMonsterState() { return bossAI.GetState(); }
}
