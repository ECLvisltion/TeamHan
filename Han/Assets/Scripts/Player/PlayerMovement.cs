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
    }
    private void FixedUpdate()
    {
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
            // 좌우 이동
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && playerScript.transform.position.x > mainCameraTransform.position.x - 8.5f)
            {
                playerScript.Move(KeyCode.A);
                playerAnimator.Move(KeyCode.A);
            }
            else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && playerScript.transform.position.x < mainCameraTransform.position.x + 8.5f)
            {
                playerScript.Move(KeyCode.D);
                playerAnimator.Move(KeyCode.D);
            }
            // 상하 이동
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && playerScript.transform.position.y > -4.0f)
            {
                playerScript.Move(KeyCode.S);
                playerAnimator.Move(KeyCode.S);
            }
            else if (!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W) && playerScript.transform.position.y < playerScript.maxUpMove)
            {
                playerScript.Move(KeyCode.W);
                playerAnimator.Move(KeyCode.W);
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
            if (Input.GetKeyDown(KeyCode.J))
            {
                playerScript.Attack();
                playerAnimator.Attack();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                playerAnimator.UnderKick();
            }
        }
    }

    // setter
    public void SetCanMove(bool isCan) { canMove = isCan; }
    public void SetIsDash(bool isDash) { this.isDash = isDash; }
    // getter
    public bool GetCanMove() { return canMove; }

    public IEnumerator DashTimer(KeyCode key)
    {
        if (key == KeyCode.A) { canDashA = true; canDashD = false; }
        if (key == KeyCode.D) { canDashD = true; canDashA = false; }
        yield return new WaitForSeconds(0.3f);
        canDashA = false; canDashD = false;
        yield return null;
    }
}
