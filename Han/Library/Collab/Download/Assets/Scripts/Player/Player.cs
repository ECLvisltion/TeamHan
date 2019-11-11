using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public new Transform transform; // 이 물체의 트랜스폼
    public GameObject mainCamera; // 메인 카메라
    public PlayerAnimator playerAnimator; // 플레이어 애니메이션
    public GameObject[] monster = new GameObject[8];

    public float movespeed; // 이동 속도
    public float maxUpMove; // 위로 올라갈 수 있는 최대치
    public int hp;
    public int combo;

    private void Start()
    {
        transform = GetComponent<Transform>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerAnimator = gameObject.transform.GetChild(0).GetComponent<PlayerAnimator>();

        movespeed = 0.1f;
        maxUpMove = -1.2f;
        hp = 100;
        combo = 0;
    }

    // setter
    public void SetCombo(int combo) { this.combo = combo; }

    public void Idle()
    {

    }
    public void Move(KeyCode key)
    {
        if (key == KeyCode.A) { transform.Translate(-movespeed, 0.0f, 0.0f); }
        else if (key == KeyCode.D) { transform.Translate(movespeed, 0.0f, 0.0f); }
        if (key == KeyCode.S) { transform.Translate(0.0f, -movespeed, 0.0f); }
        else if (key == KeyCode.W) { transform.Translate(0.0f, movespeed, 0.0f); }
    }
    public void Attack()
    {
        combo++;
    }
    public void Attacking()
    {
        // 피격판정 및 데미지
        foreach (GameObject go in monster)
        {
            if (go != null)
            {
                Monster m = go.transform.parent.GetComponent<Monster>();
                //if (m != null) { m.Hit(combo); }

                Scarecrow s = go.transform.parent.GetComponent<Scarecrow>();
                if (s != null) { s.Hit(combo); }
            }
        }
    }
    public void UnderKicking()
    {
        // 피격판정 및 데미지
        foreach (GameObject go in monster)
        {
            if (go != null)
            {
                Monster m = go.transform.parent.GetComponent<Monster>();
                //if (m != null) { m.Hit(combo); }
            }
        }
    }

    public void SetMonsterHitbox(GameObject monster)
    {
        for (int i = 0; i < 8; i++)
        {
            if (this.monster[i] == null)
            {
                this.monster[i] = monster;
                return;
            }
        }
    }
    public void DeleteMonsterHitbox(GameObject monster)
    {
        for (int i = 0; i < 8; i++)
        {
            if (this.monster[i] == monster)
            {
                this.monster[i] = null;
            }
        }
    }
}
