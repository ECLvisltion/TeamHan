﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public GameObject gameManager;
    public PlayerMovement playerMovement;
    public GameObject player;
    public Player playerScript;
    public Animator animator;
    public bool isCombo;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        playerMovement = gameManager.GetComponent<PlayerMovement>();
        player = gameObject.transform.parent.gameObject;
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();

        isCombo = false;
    }

    public void Idle()
    {
        isCombo = false;
        animator.SetInteger("Status", 0);
    }
    public void Move(KeyCode key)
    {
        isCombo = false;
        if (key == KeyCode.A) { player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); }
        else if (key == KeyCode.D) { player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }
        animator.SetInteger("Status", 1);
    }
    public void Attack()
    {
        isCombo = true;
        animator.SetBool("Attack", true);
    }
    public void Attacking()
    {
        playerScript.Attacking();
    }
    public void AttackOver()
    {
        animator.SetBool("Attack", false);
        animator.SetInteger("Status", -1);
    }
    public void CanMove()
    {
        SetCanMove(true);
    }
    public void CantMove()
    {
        SetCanMove(false);
    }

    // setter
    private void SetCanMove(bool isCan) { if (playerMovement.GetCanMove() != isCan) { playerMovement.SetCanMove(isCan); } }
    // getter
    public bool GetIsCombo() { return isCombo; }
}
