using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public new Transform transform; // 이 물체의 트랜스폼
    public GameObject mainCamera; // 메인 카메라
    public PlayerAnimator playerAnimator; // 플레이어 애니메이션
    public PlayerMovement playerMovement; // 플레이어 무브먼트
    public GameObject[] monster = new GameObject[16];

    public float moveSpeed; // 이동 속도
    public float maxUpMove; // 위로 올라갈 수 있는 최대치
    public float dashSpeed; // 대시 속도
    public int hp, damage;
    public int combo;

    private void Start()
    {
        transform = GetComponent<Transform>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerAnimator = gameObject.transform.GetChild(0).GetComponent<PlayerAnimator>();
        playerMovement = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerMovement>();
        moveSpeed = 5.0f;
        maxUpMove = -1.2f;
        dashSpeed = 25.0f;
        hp = 100;
        damage = 1;
        combo = 0;
    }
    private void Update()
    {
        if (hp == 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(gameManager.GameOver());
            }
        }
    }

    // setter
    public void SetCombo(int combo) { this.combo = combo; }
    // getter
    public int GetPlayerHP() { return hp; }

    public void Idle()
    {

    }
    public void Move(float horizontal, float vertical)
    {
        transform.Translate(moveSpeed * horizontal * Time.deltaTime, moveSpeed * vertical * Time.deltaTime, 0.0f);
        if (transform.position.x < mainCamera.transform.position.x - 8.5f) { transform.position = new Vector2(mainCamera.transform.position.x - 8.5f, transform.position.y); }
        if (transform.position.x > mainCamera.transform.position.x + 8.5f) { transform.position = new Vector2(mainCamera.transform.position.x + 8.5f, transform.position.y); }
        if (transform.position.y < mainCamera.transform.position.y - 4.0f) { transform.position = new Vector2(transform.position.x, mainCamera.transform.position.y - 4.0f); }
        if (transform.position.y > mainCamera.transform.position.y + maxUpMove) { transform.position = new Vector2(transform.position.x, mainCamera.transform.position.y + maxUpMove); }
    }
    public void Move(KeyCode key)
    {
        if (key == KeyCode.A) { transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f); }
        else if (key == KeyCode.D) { transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f); }
        if (key == KeyCode.S) { transform.Translate(0.0f, -moveSpeed * Time.deltaTime, 0.0f); }
        else if (key == KeyCode.W) { transform.Translate(0.0f, moveSpeed * Time.deltaTime, 0.0f); }
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
                if (transform.position.x > mainCamera.transform.position.x - 8.5f) { transform.Translate(-dashSpeed * Time.deltaTime, 0.0f, 0.0f); }
                else { transform.position = new Vector3(mainCamera.transform.position.x - 8.5f, transform.position.y, transform.position.z); }
            }
            else if (key == KeyCode.D)
            {
                if (transform.position.x < mainCamera.transform.position.x + 8.5f) { transform.Translate(+dashSpeed * Time.deltaTime, 0.0f, 0.0f); }
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
                if ((m != null) && (m.GetMonsterState() <= 4) && (m.GetMonsterState() >= 0))
                {
                    m.SetHit(combo);
                    m.SetHPDecrease(damage);
                }

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
                if (m != null)
                {
                    m.SetHit(4);
                    m.SetHPDecrease(damage);
                }

                Scarecrow s = go.transform.parent.GetComponent<Scarecrow>();
                if (s != null) { s.UnderHit(); }
            }
        }
    }
    public void Hit(int damage)
    {
        // 애니메이션 추가
        hp -= damage;
        if (hp <= 0)
        {
            playerAnimator.Die();
            playerMovement.enabled = false;
        }
    }
    public void Skill(int skillNum)
    {
        switch (skillNum)
        {
            case 1:
                break;

            case 2:
                break;

            case 3:
                break;
        }
    }

    public void SetMonsterHitbox(GameObject monster)
    {
        for (int i = 0; i < 16; i++)
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
        for (int i = 0; i < 16; i++)
        {
            if (this.monster[i] == monster)
            {
                this.monster[i] = null;
                return;
            }
        }
    }
}
