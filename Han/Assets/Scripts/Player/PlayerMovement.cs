using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player playerScript;
    public PlayerAnimator playerAnimator;
    public GameObject mainCamera;
    public Transform mainCameraTransform;
    public bool canMove;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<PlayerAnimator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraTransform = mainCamera.GetComponent<Transform>();

        canMove = true;
    }
    private void FixedUpdate()
    {
        if (canMove == true)
        {
            // 좌우를 같이 누르거나, 상하를 같이 누르거나, 모두 누르고 있거나, 모두 누르지 않을 때
            if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) ||
                (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) ||
                (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) ||
                (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                && !(playerAnimator.GetIsCombo()))
            {
                Debug.Log("1");
                playerScript.Idle();
                playerAnimator.Idle();
            }
            // 좌우 이동
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && playerScript.transform.position.x > mainCameraTransform.position.x - 8.5f)
            {
                Debug.Log("2");
                playerScript.Move(KeyCode.A);
                playerAnimator.Move(KeyCode.A);
            }
            else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && playerScript.transform.position.x < mainCameraTransform.position.x + 8.5f)
            {
                Debug.Log("2");
                playerScript.Move(KeyCode.D);
                playerAnimator.Move(KeyCode.D);
            }
            // 상하 이동
            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && playerScript.transform.position.y > -4.0f)
            {
                Debug.Log("2");
                playerScript.Move(KeyCode.S);
                playerAnimator.Move(KeyCode.S);
            }
            else if (!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W) && playerScript.transform.position.y < playerScript.maxUpMove)
            {
                Debug.Log("2");
                playerScript.Move(KeyCode.W);
                playerAnimator.Move(KeyCode.W);
            }
            // 기본 공격
            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("3");
                playerScript.Attack();
                playerAnimator.Attack();
            }
        }
    }

    // setter
    public void SetCanMove(bool isCan) { canMove = isCan; }
    // getter
    public bool GetCanMove() { return canMove; }
}
