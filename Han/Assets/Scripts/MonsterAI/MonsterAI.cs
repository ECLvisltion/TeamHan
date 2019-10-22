using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState { idle = 0, move, beforeAttack, attack, hit }

public class MonsterAI : MonoBehaviour
{
    public GameObject player;
    public AIState curState;
    public MonsterAttack attack;
    public MonsterBeforeAttack beforeAttack;
    public MonsterHit hit;
    public MonsterIdle idle;
    public MonsterMove move;
    private bool canAction;

    Dictionary<AIState, MonsterState> mobAI;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        curState = AIState.idle;
        attack = gameObject.GetComponent<MonsterAttack>();
        beforeAttack = gameObject.GetComponent<MonsterBeforeAttack>();
        hit = gameObject.GetComponent<MonsterHit>();
        idle = gameObject.GetComponent<MonsterIdle>();
        move = gameObject.GetComponent<MonsterMove>();

        canAction = true;

        // mobAI.Add(AIState.idle, MonsterIdle);
        SetBehaviorOn(AIState.idle);
    }
    private void Update()
    {
        if (false) // 
    }

    // setter
    public void SetCanAction(bool action) { canAction = action; }
    /// <summary>
    /// 몬스터의 행동을 설정할 때 사용
    /// 몬스터의 전 행동을 알 수 없을 때 사용하는 함수
    /// </summary>
    /// <param name="nextState">다음 행동</param>
    public void SetBehaviorOn(AIState nextState)
    {
        attack.enabled = false;
        beforeAttack.enabled = false;
        hit.enabled = false;
        idle.enabled = false;
        move.enabled = false;

        switch (nextState)
        {
            case AIState.idle:
                idle.enabled = true;
                break;

            case AIState.move:
                move.enabled = true;
                break;

            case AIState.beforeAttack:
                beforeAttack.enabled = true;
                break;

            case AIState.attack:
                attack.enabled = true;
                break;

            case AIState.hit:
                hit.enabled = true;
                break;
        }
    }
    /// <summary>
    /// 몬스터의 행동을 설정할 때 사용
    /// 몬스터의 전 행동을 알 때 사용하는 함수
    /// </summary>
    /// <param name="beforeState">이전 행동</param>
    /// <param name="nextState">다음 행동</param>
    public void SetBehaviorOn(AIState beforeState, AIState nextState)
    {
        switch (beforeState)
        {
            case AIState.idle:
                idle.enabled = false;
                break;

            case AIState.move:
                move.enabled = false;
                break;

            case AIState.beforeAttack:
                beforeAttack.enabled = false;
                break;

            case AIState.attack:
                attack.enabled = false;
                break;

            case AIState.hit:
                hit.enabled = false;
                break;
        }
        switch (nextState)
        {
            case AIState.idle:
                idle.enabled = true;
                break;

            case AIState.move:
                move.enabled = true;
                break;

            case AIState.beforeAttack:
                beforeAttack.enabled = true;
                break;

            case AIState.attack:
                attack.enabled = true;
                break;

            case AIState.hit:
                hit.enabled = true;
                break;
        }
    }
    // getter
    public bool GetCanAction() { return canAction; }
    public Transform GetPlayerTransform() { return player.transform; }
}
