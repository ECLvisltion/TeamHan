using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public new Transform transform; // 이 물체의 트랜스폼
    public GameObject mainCamera; // 메인 카메라
    public PlayerAnimator playerAnimator; // 플레이어 애니메이션
    public PlayerMovement playerMovement; // 플레이어 무브먼트
    public GameObject[] monster = new GameObject[8];

    public float moveSpeed; // 이동 속도
    public float maxUpMove; // 위로 올라갈 수 있는 최대치
    public float dashSpeed; // 대시 속도
    public int hp;
    public int combo;

    private void Start()
    {
        transform = GetComponent<Transform>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerAnimator = gameObject.transform.GetChild(0).GetComponent<PlayerAnimator>();
        playerMovement = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerMovement>();
        moveSpeed = 0.1f;
        maxUpMove = -1.2f;
        dashSpeed = 0.5f;
        hp = 100;
        combo = 0;
    }

    // setter
    public void SetCombo(int combo) { this.combo = combo; }
    // getter
    public int GetPlayerHP() { return hp; }

    public void Idle()
    {

    }
    public void Move(KeyCode key)
    {
        if (key == KeyCode.A) { transform.Translate(-moveSpeed, 0.0f, 0.0f); }
        else if (key == KeyCode.D) { transform.Translate(moveSpeed, 0.0f, 0.0f); }
        if (key == KeyCode.S) { transform.Translate(0.0f, -moveSpeed, 0.0f); }
        else if (key == KeyCode.W) { transform.Translate(0.0f, moveSpeed, 0.0f); }
    }
    public IEnumerator Dash(KeyCode key)
    {
        playerMovement.SetIsDash(true);

        if (key == KeyCode.A)
        {
            GameObject go;
            go = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/Dash_Dust01"), new Vector3(transform.position.x, transform.position.y - 1.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            go.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        if (key == KeyCode.D)
        {
            GameObject go;
            go = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/Dash_Dust01"), new Vector3(transform.position.x, transform.position.y - 1.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        for (int i = 0; i < 10; i++)
        {
            if (key == KeyCode.A)
            {
                if (transform.position.x > mainCamera.transform.position.x - 8.5f) { transform.Translate(-dashSpeed, 0.0f, 0.0f); }
                else { transform.position = new Vector3(mainCamera.transform.position.x - 8.5f, transform.position.y, transform.position.z); }
            }
            else if (key == KeyCode.D)
            {
                if (transform.position.x < mainCamera.transform.position.x + 8.5f) { transform.Translate(+dashSpeed, 0.0f, 0.0f); }
                else { transform.position = new Vector3(mainCamera.transform.position.x + 8.5f, transform.position.y, transform.position.z); }
            }
            
            yield return new WaitForSeconds(0.02f);
        }

        playerAnimator.Idle();
        playerMovement.SetIsDash(false);
        yield return null;
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
                if (s != null)
                {
                    if (transform.localScale.x < 0)
                    {
                        s.Hit(combo, KeyCode.A);
                    }
                    else
                    {
                        s.Hit(combo, KeyCode.D);
                    }
                }
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

                Scarecrow s = go.transform.parent.GetComponent<Scarecrow>();
                if (s != null) { s.UnderHit(); }
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
