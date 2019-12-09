using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player playerScript;
    public PlayerAnimator playerAnimator;
    public GameObject mainCamera;
    public Transform mainCameraTransform;
    public bool canMove, canDashA, canDashD, isDash;
    public float[] skill = new float[3];
    public float horizontal, vertical;

    /*
    KeyCode[] keyCodes = null;
    List<KeyCode> keys = new List<KeyCode>();
    */

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<PlayerAnimator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraTransform = mainCamera.GetComponent<Transform>();

        canMove = true;
        canDashA = false;
        canDashD = false;
        isDash = false;
        skill[0] = 1.0f;
        skill[1] = 1.0f;
        skill[2] = 1.0f;
        horizontal = 0;
        vertical = 0;

        /*
        // 키코드를 한번 받아옵니다. 이렇게 전체를 받아도되지만 원하는것만 추가해도 되겠지요.
        if (keyCodes == null)
            keyCodes = System.Enum.GetValues(typeof(KeyCode)) as KeyCode[];
        */
    }
    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        /*
        // 매 프레임마다 눌렸는지 검사해서 리스트에 누른 키를 추가합니다.
        foreach (KeyCode keyCode in keyCodes)
            if (Input.GetKeyDown(keyCode))
                keys.Add(keyCode);

        // 출력을 위해 담아버립니다.
        string text = string.Empty;
        foreach (KeyCode keyCode in keys)
            Debug.Log(keyCode.ToString());
        */

        if (canMove == true && isDash == false)
        {
            // 좌우를 같이 누르거나, 상하를 같이 누르거나, 모두 누르고 있거나, 모두 누르지 않을 때
            if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) ||
                (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) ||
                (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) ||
                (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                && !(playerAnimator.GetIsCombo()))
            {
                playerScript.Idle();
                playerAnimator.Idle();
            }
            // 패드 이동
            if (horizontal != 0 || vertical != 0)
            {
                playerScript.Move(horizontal, vertical);
                if (horizontal < 0)
                {
                    playerAnimator.Move(KeyCode.A);
                }
                else
                {
                    playerAnimator.Move(KeyCode.D);
                }
            }
            else // 논스틱 이동
            {
                // 좌우 이동
                if (playerScript.transform.position.x > mainCameraTransform.position.x - 8.5f)
                {
                    if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                    {
                        playerScript.Move(KeyCode.A);
                        playerAnimator.Move(KeyCode.A);
                    }
                }
                else if (playerScript.transform.position.x < mainCameraTransform.position.x + 8.5f)
                {
                    if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                    {
                        playerScript.Move(KeyCode.D);
                        playerAnimator.Move(KeyCode.D);
                    }
                }
                // 상하 이동
                if (playerScript.transform.position.y > -4.0f)
                {
                    if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
                    {
                        playerScript.Move(KeyCode.S);
                        playerAnimator.Move(KeyCode.S);
                    }
                }
                else if (playerScript.transform.position.y < playerScript.maxUpMove)
                {
                    if (!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
                    {
                        playerScript.Move(KeyCode.W);
                        playerAnimator.Move(KeyCode.W);
                    }
                }
            }
            // 스틱/패드 대시
            if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                if (canDashA == true) { canDashA = false; }
                if (canDashD == true) { canDashD = false; }

                if (horizontal > 0)
                {
                    playerAnimator.Dash(KeyCode.D);
                    StartCoroutine(playerScript.Dash(KeyCode.D));
                }
                else if (horizontal < 0)
                {
                    playerAnimator.Dash(KeyCode.A);
                    StartCoroutine(playerScript.Dash(KeyCode.A));
                }
            }
            // 대시
            if (Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D))
            {
                if (canDashA == false) { StartCoroutine(DashTimer(KeyCode.A)); }
                else
                {
                    playerAnimator.Dash(KeyCode.A);
                    StartCoroutine(playerScript.Dash(KeyCode.A));
                }
            }
            if (!Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D))
            {
                if (canDashD == false) { StartCoroutine(DashTimer(KeyCode.D)); }
                else
                {
                    StartCoroutine(playerScript.Dash(KeyCode.D));
                    playerAnimator.Dash(KeyCode.D);
                }
            }
            // 기본 공격
            if (Input.GetKeyDown(KeyCode.K) || // 키보드
               (vertical == 0.95f && Input.GetKeyDown(KeyCode.JoystickButton2))) // 패드
            {
                playerAnimator.UnderKick();
            }
            else if (Input.GetKeyDown(KeyCode.J) || // 키보드
                Input.GetKeyDown(KeyCode.JoystickButton2)) // 패드
            {
                playerScript.Attack();
                playerAnimator.Attack();
            }
            // 스킬 공격
            if (skill[0] == 1.0f &&
                (Input.GetKeyDown(KeyCode.U) || // 키보드
                Input.GetKeyDown(KeyCode.JoystickButton3))) // 패드
            {
                StartCoroutine(SkillCooltime(0, 20.0f));
                playerAnimator.Skill(1);
            }
            if (skill[1] == 1.0f &&
                (Input.GetKeyDown(KeyCode.I) || // 키보드
                Input.GetKeyDown(KeyCode.JoystickButton1))) // 패드
            {
                StartCoroutine(SkillCooltime(1, 40.0f));
                playerAnimator.Skill(2);
            }
            if (skill[2] == 1.0f &&
                (Input.GetKeyDown(KeyCode.O) || // 키보드
                Input.GetKeyDown(KeyCode.JoystickButton0))) // 패드
            {
                StartCoroutine(SkillCooltime(2, 30.0f));
                playerAnimator.Skill(3);
            }
        }
    }

    // setter
    public void SetCanMove(bool isCan) { canMove = isCan; }
    public void SetIsDash(bool isDash) { this.isDash = isDash; }
    // getter
    public bool GetCanMove() { return canMove; }
    public float GetSkillCooltime(int type) { return skill[type]; }

    public IEnumerator DashTimer(KeyCode key)
    {
        if (key == KeyCode.A) { canDashA = true; canDashD = false; }
        if (key == KeyCode.D) { canDashD = true; canDashA = false; }
        yield return new WaitForSeconds(0.3f);
        canDashA = false; canDashD = false;
        yield return null;
    }
    public IEnumerator SkillCooltime(int type, float time)
    {
        skill[type] = 0.0f;

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(time / 100.0f);
            skill[type] += 0.01f;
        }

        skill[type] = 1.0f;
        yield return null;
    }
}
