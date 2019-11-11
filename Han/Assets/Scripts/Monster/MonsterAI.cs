using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState { idle = 0, move, beforeAttack, attack, hit, underHit }

public class MonsterAI : MonoBehaviour
{
    public GameObject player;
    public AIState curState;
    public Monster monster;
    public MonsterAnimator maScript;
    public MonsterAttack attack;
    public MonsterBeforeAttack beforeAttack;
    public MonsterHit hit;
    public MonsterIdle idle;
    public MonsterMove move;
    
    public bool isHit;
    public bool isMove;
    public bool attackSucceed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        curState = AIState.idle;
        isHit = false;
        attackSucceed = false;
        monster = transform.parent.GetComponent<Monster>();
        maScript = transform.parent.GetChild(0).GetComponent<MonsterAnimator>();
        attack = gameObject.GetComponent<MonsterAttack>();
        beforeAttack = gameObject.GetComponent<MonsterBeforeAttack>();
        hit = gameObject.GetComponent<MonsterHit>();
        idle = gameObject.GetComponent<MonsterIdle>();
        move = gameObject.GetComponent<MonsterMove>();

        SetBehaviorOn(AIState.idle);
        StartCoroutine(SetMoveTimer());
    }
    private void Update()
    {
        maScript.SetState((int)curState);
        if (isHit == true)
        {
            maScript.SetIsHit(isHit);
            isHit = false;
        }

        switch (curState) // State 변환 조건
        {
            case AIState.idle:
                if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 1.0f && // 플레이어와 캐릭터가 일직선상에 있을 때
                    Vector3.Distance(player.transform.position, transform.position) >= 1.0f) // 플레이어가 몬스터랑 가까이 있을 때
                {
                    SetBehaviorOn(AIState.idle, AIState.beforeAttack);
                }

                if (isHit == true)
                {
                    isHit = false;
                    SetBehaviorOn(AIState.idle, AIState.hit);
                }
                if (isMove == true)
                {
                    move.SetRandomKey();
                    SetBehaviorOn(AIState.idle, AIState.move);
                }
                break;

            case AIState.move:
                if (isHit == true)
                {
                    isHit = false;
                    isMove = false;
                    SetBehaviorOn(AIState.idle, AIState.hit);
                }
                if (isMove == false)
                {
                    SetBehaviorOn(AIState.move, AIState.idle);
                }
                break;

            case AIState.beforeAttack:

                break;

            case AIState.attack:
                if (attackSucceed == true)
                {
                    SetBehaviorOn(AIState.attack, AIState.beforeAttack);
                }
                else if (Random.Range(0, 2) == 1)
                {
                    SetBehaviorOn(AIState.attack, AIState.beforeAttack);
                }
                break;

            case AIState.hit:
                break;
        }
    }

    // setter
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
    public void SetIsHit(bool isHit) { this.isHit = isHit; }
    public IEnumerator SetMoveTimer()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 3.0f));
        if (curState == AIState.idle) { isMove = true; }
        yield return null;
    }
}
