using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public GameObject gameManager;
    public PlayerMovement playerMovement;
    public GameObject player;
    public Player playerScript;
    public Animator animator;
    public GameObject mainCamera;
    public bool isCombo;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerMovement = gameManager.GetComponent<PlayerMovement>();
        player = gameObject.transform.parent.gameObject;
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        isCombo = false;
    }

    public void Idle()
    {
        isCombo = false;
        animator.SetInteger("Status", 0);
        playerMovement.SetCanMove(true);
    }
    public void Move(KeyCode key)
    {
        isCombo = false;
        if (key == KeyCode.A) { player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }
        else if (key == KeyCode.D) { player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }
        animator.SetInteger("Status", 1);
    }
    public void Dash(KeyCode key)
    {
        isCombo = false;
        if (key == KeyCode.A) { player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }
        else if (key == KeyCode.D) { player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }
        animator.SetInteger("Status", 2);
    }
    public void Attack()
    {
        isCombo = true;
        animator.SetBool("Attack", true);
    }
    public void UnderKick()
    {
        isCombo = false;
        animator.SetBool("UnderKick", true);
    }
    public void Attacking()
    {
        playerScript.Attacking();
    }
    public void UnderKicking()
    {
        playerScript.UnderKicking();
    }
    public void AttackOver()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("UnderKick", false);
        animator.SetInteger("Status", -2);
    }
    public void Die()
    {
        animator.SetInteger("Status", -1);
    }
    public void Skill(int skillNum)
    {
        playerMovement.SetCanMove(false);

        switch (skillNum)
        {
            case 1:
                StartCoroutine(Skill1());
                break;

            case 2:
                animator.SetInteger("SkillAnim", 10);
                animator.SetInteger("Status", 3);
                break;

            case 3:
                animator.SetInteger("SkillAnim", 20);
                animator.SetInteger("Status", 3);
                break;
        }
    }

    public void CanMove()
    {
        SetCanMove(true);
    }
    public void CantMove()
    {
        SetCanMove(false);
    }

    private IEnumerator Skill1()
    {
        GameObject charge = null;
        Transform shadow = player.transform.GetChild(3);
        SpriteRenderer shadowSR = shadow.GetComponent<SpriteRenderer>();

        animator.SetInteger("SkillAnim", 0);
        animator.SetInteger("Status", 3);

        yield return new WaitForSeconds(0.2f);

        animator.SetInteger("SkillAnim", 1);
        animator.SetInteger("Status", 3);
        GameObject jump = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SkillHan_Jump"),
            new Vector3(player.transform.position.x + player.transform.localScale.x * -0.5f, player.transform.position.y - 1.3f, 0.0f),
            Quaternion.Euler(0.0f, 0.0f, 0.0f));
        jump.transform.localScale = new Vector3(player.transform.localScale.x * jump.transform.localScale.x, jump.transform.localScale.y, 1.0f);

        for (int i = 10; i > 0; i--)
        {
            if (i == 5)
            {
                charge = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SkillHan_Charge"), player.transform);
                charge.transform.localPosition = new Vector2(-0.8f, 1.2f);
            }

            player.transform.Translate(player.transform.localScale.x * 0.1f, 0.08f * i, 0.0f);
            if (player.transform.position.x < mainCamera.transform.position.x - 8.5f) { player.transform.position = new Vector2(-8.5f, player.transform.position.y); }
            else if (player.transform.position.x > mainCamera.transform.position.x + 8.5f) { player.transform.position = new Vector2(8.5f, player.transform.position.y); }

            shadow.Translate(0.0f, -0.08f * i, 0.0f);
            shadowSR.color = new Color(1.0f, 1.0f, 1.0f, 0.45f + 0.01f * i);
            yield return new WaitForSeconds(0.03f);
        }

        animator.SetInteger("SkillAnim", 2);
        animator.SetInteger("Status", 3);
        //charge.transform.localPosition = new Vector2(-0.5f, -0.5f);

        for (int i = 0; i < 5; i++)
        {
            player.transform.Translate(player.transform.localScale.x * 0.06f, -0.88f, 0.0f);
            if (player.transform.position.x < mainCamera.transform.position.x - 8.5f) { player.transform.position = new Vector2(-8.5f, player.transform.position.y); }
            else if (player.transform.position.x > mainCamera.transform.position.x + 8.5f) { player.transform.position = new Vector2(8.5f, player.transform.position.y); }
            shadow.Translate(0.0f, 0.88f, 0.0f);
            shadowSR.color = new Color(1.0f, 1.0f, 1.0f, 0.56f + 0.11f * i);
            yield return new WaitForSeconds(0.01f);
        }

        animator.SetInteger("SkillAnim", 3);
        animator.SetInteger("Status", 3);
        GameObject attack = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SkillHan_Attack"),
            new Vector3(player.transform.position.x + player.transform.localScale.x * 0.5f, player.transform.position.y - 1.5f, 0.0f),
            Quaternion.Euler(0.0f, 0.0f, 0.0f));
        attack.transform.localScale = new Vector3(player.transform.localScale.x * attack.transform.localScale.x, attack.transform.localScale.y, 1.0f);

        yield return new WaitForSeconds(0.5f);

        animator.SetInteger("Status", 0);
        playerMovement.SetCanMove(true);

        yield return null;
    }
    private void Skill2(int num)
    {
        switch (num)
        {
            case 0:
                //Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SkillFury_Fire"), player.transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                StartCoroutine(PauseScene(0.5f));
                break;
            case 1:
                player.transform.Translate(player.transform.localScale.x * 6.0f, 0.0f, 0.0f);
                if (player.transform.position.x < mainCamera.transform.position.x - 8.5f) { player.transform.position = new Vector2(-8.5f, player.transform.position.y); }
                else if (player.transform.position.x > mainCamera.transform.position.x + 8.5f) { player.transform.position = new Vector2(8.5f, player.transform.position.y); }
                GameObject kick = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SkillFury_Kick"),
                    new Vector3(player.transform.position.x + player.transform.localScale.x * 1.0f, player.transform.position.y, 0.0f),
                    Quaternion.Euler(0.0f, 0.0f, 0.0f));
                kick.transform.localScale = new Vector3(player.transform.localScale.x * kick.transform.localScale.x, kick.transform.localScale.y, 1.0f);
                GameObject attack = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SkillFury_Attack"),
                    new Vector3(player.transform.position.x + player.transform.localScale.x * 1.0f, player.transform.position.y, 0.0f),
                    Quaternion.Euler(0.0f, 0.0f, 0.0f));
                attack.transform.localScale = new Vector3(player.transform.localScale.x * attack.transform.localScale.x, attack.transform.localScale.y, 1.0f);
                StartCoroutine(Skill2SlowMotion(1.0f));
                break;
        }
    }
    private void Skill3On()
    {
        StartCoroutine(Skill3());
    }
    private IEnumerator Skill3()
    {
        GameObject g1 = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SkillGun_Gun"),
            new Vector3(player.transform.position.x + player.transform.localScale.x * 3.0f, player.transform.position.y - 1.5f, 0.0f),
            Quaternion.Euler(0.0f, 0.0f, 0.0f));
        g1.transform.localScale = new Vector3(player.transform.localScale.x * g1.transform.localScale.x, g1.transform.localScale.y, 1.0f);
        yield return new WaitForSeconds(0.15f);

        GameObject g2 = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SkillGun_Gon"),
            new Vector3(player.transform.position.x + player.transform.localScale.x * 6.0f, player.transform.position.y - 1.5f, 0.0f),
            Quaternion.Euler(0.0f, 0.0f, 0.0f));
        g2.transform.localScale = new Vector3(player.transform.localScale.x * g2.transform.localScale.x, g2.transform.localScale.y, 1.0f);
        yield return new WaitForSeconds(0.15f);

        GameObject g3 = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SkillGun_Gam"),
            new Vector3(player.transform.position.x + player.transform.localScale.x * 9.0f, player.transform.position.y - 1.5f, 0.0f),
            Quaternion.Euler(0.0f, 0.0f, 0.0f));
        g3.transform.localScale = new Vector3(player.transform.localScale.x * g3.transform.localScale.x, g3.transform.localScale.y, 1.0f);
        yield return new WaitForSeconds(0.15f);

        GameObject g4 = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/SkillGun_Li"),
            new Vector3(player.transform.position.x + player.transform.localScale.x * 12.0f, player.transform.position.y - 1.5f, 0.0f),
            Quaternion.Euler(0.0f, 0.0f, 0.0f));
        g4.transform.localScale = new Vector3(player.transform.localScale.x * g4.transform.localScale.x, g4.transform.localScale.y, 1.0f);
        yield return new WaitForSeconds(0.15f);
        
        animator.SetInteger("Status", 0);
        playerMovement.SetCanMove(true);

        yield return null;
    }
    private IEnumerator PauseScene(float second)
    {
        Time.timeScale = 0.0f;

        float elapsedTime = 0.0f;
        while (elapsedTime < second)
        {
            yield return null;
            elapsedTime += Time.unscaledDeltaTime;
        }

        Time.timeScale = 1.0f;
        yield return null;
    }
    private IEnumerator Skill2SlowMotion(float second)
    {
        yield return new WaitForSeconds(0.02f);
        Time.timeScale = 0.0f;
        
        float elapsedTime = 0.0f;

        while (elapsedTime < second)
        {
            yield return null;
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = elapsedTime / second;
        }
        
        Time.timeScale = 1.0f;
        animator.SetInteger("Status", 0);
        playerMovement.SetCanMove(true);

        yield return null;
    }

    // setter
    private void SetCanMove(bool isCan) { playerMovement.SetCanMove(isCan); }
    private void ResetCombo() { playerScript.SetCombo(0); }
    // getter
    public bool GetIsCombo() { return isCombo; }
}
