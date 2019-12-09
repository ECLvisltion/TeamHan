using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAIState { spawn = -2, die, idle, move, attack, hit1, hit2, hit3, underHit, standing }

public class BossAI : MonoBehaviour
{
    private Monster monsterScript;
    private MonsterSpawn AIspawn;
    private MonsterDie AIdie;
    private MonsterIdle AIidle;
    private MonsterMove AImove;
    private BossAttack1 AIattack1;
    private BossAttack2 AIattack2;
    private MonsterHit1 AIhit1;
    private MonsterHit2 AIhit2;
    private MonsterHit3 AIhit3;
    private MonsterUnderHit AIunderHit;
    private MonsterStanding AIstanding;

    public int state; // 상태 변환
    public bool isDie; // 이 몬스터가 사망했을 때,
    public int isHitCombo; // 공격당할 시 콤보(1~3), 아래공격(4)
    public bool changeIdle; // idle 상태가 attack이나 move를 사용하기 이전 쿨타임
    public int changeMove; // move 상태가 attack이나 idle을 사용하기 위한 변수.(공격(1-랜덤), 대기(1-랜덤, 2))
    public bool changeAttack; // attack 애니메이션이 끝났을 때 다른 state로 이동하기 위한 변수
    public bool doNextState; // hit3이나 underhit, standing에서 약간의 딜레이를 주기 위한 변수.

    public GameObject monster, player, mainCamera;

    private void Start()
    {
        monsterScript = transform.parent.GetComponent<Monster>();
        AIspawn = gameObject.GetComponent<MonsterSpawn>();
        AIdie = gameObject.GetComponent<MonsterDie>();
        AIidle = gameObject.GetComponent<MonsterIdle>();
        AImove = gameObject.GetComponent<MonsterMove>();
        AIattack1 = gameObject.GetComponent<BossAttack1>();
        AIattack2 = gameObject.GetComponent<BossAttack2>();
        AIhit1 = gameObject.GetComponent<MonsterHit1>();
        AIhit2 = gameObject.GetComponent<MonsterHit2>();
        AIhit3 = gameObject.GetComponent<MonsterHit3>();
        AIunderHit = gameObject.GetComponent<MonsterUnderHit>();
        AIstanding = gameObject.GetComponent<MonsterStanding>();

        player = GameObject.FindGameObjectWithTag("Player");
        monster = transform.parent.gameObject;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        state = -2;
        isDie = false;
        isHitCombo = 0;
        changeIdle = false;
        changeMove = 0;
        changeAttack = false;
        doNextState = false;

        AIspawn.enabled = true;
        AIdie.enabled = false;
        AIidle.enabled = false;
        AImove.enabled = false;
        AIattack1.enabled = false;
        AIattack2.enabled = false;
        AIhit1.enabled = false;
        AIhit2.enabled = false;
        AIhit3.enabled = false;
        AIunderHit.enabled = false;
        AIstanding.enabled = false;
    }
    private void Update()
    {
        if (monsterScript.GetHP() <= 0)
        {
            isDie = true;
        }

        if (state == -2) // AIspawn
        {
            if (monster.transform.position.x > mainCamera.transform.position.x - 8.0f &&
                monster.transform.position.x < mainCamera.transform.position.x + 8.0f)
            {
                SetState((int)BossAIState.idle);
            }
        }
        if (state == -1) // AIdie
        {

        }
        if (state == 0) // AIidle
        {
            if (isDie == true)
            {
                SetState((int)BossAIState.hit3);
                StartCoroutine(EffectOn(3));
            }

            if (isHitCombo == 1)
            {
                AImove.SetTempIntZero();
                if (AImove.GetTempIntIsZero())
                {
                    SetState((int)BossAIState.hit1);
                    StartCoroutine(EffectOn(1));
                }
            }
            else if (isHitCombo == 2)
            {
                AImove.SetTempIntZero();
                if (AImove.GetTempIntIsZero())
                {
                    SetState((int)BossAIState.hit2);
                    StartCoroutine(EffectOn(2));
                }
            }
            else if (isHitCombo == 3)
            {
                AImove.SetTempIntZero();
                if (AImove.GetTempIntIsZero())
                {
                    SetState((int)BossAIState.hit3);
                    StartCoroutine(EffectOn(3));
                }
            }
            else if (isHitCombo == 4) // 아래 공격
            {
                AImove.SetTempIntZero();
                if (AImove.GetTempIntIsZero())
                {
                    SetState((int)BossAIState.underHit);
                    StartCoroutine(EffectOn(4));
                }
            }

            if (changeIdle == true)
            {
                if (monster.transform.position.y == player.transform.position.y &&
                    Mathf.Abs(monster.transform.position.x - player.transform.position.x) <= 0.5f)
                {
                    if (Random.Range(0, 10) < 5)
                    {
                        SetState((int)BossAIState.attack);
                    }
                    else
                    {
                        SetState((int)BossAIState.move);
                    }
                }
                else
                {
                    SetState((int)BossAIState.move);
                }
            }
        }
        if (state == 1) // AImove
        {
            if (isDie == true)
            {
                SetState((int)BossAIState.hit3);
                StartCoroutine(EffectOn(3));
            }

            if (isHitCombo == 1)
            {
                SetState((int)BossAIState.hit1);
                StartCoroutine(EffectOn(1));
            }
            else if (isHitCombo == 2)
            {
                SetState((int)BossAIState.hit2);
                StartCoroutine(EffectOn(2));
            }
            else if (isHitCombo == 3)
            {
                SetState((int)BossAIState.hit3);
                StartCoroutine(EffectOn(3));
            }
            else if (isHitCombo == 4) // 아래 공격
            {
                SetState((int)BossAIState.underHit);
                StartCoroutine(EffectOn(4));
            }

            if (changeMove == 1)
            {
                if (Random.Range(0, 10) < 5)
                {
                    SetState((int)BossAIState.attack);
                }
                else
                {
                    SetState((int)BossAIState.idle);
                }
            }
            else if (changeMove == 2)
            {
                SetState((int)BossAIState.idle);
            }
        }
        if (state == 2) // AIattack
        {
            if (isDie == true)
            {
                SetState((int)BossAIState.hit3);
                StartCoroutine(EffectOn(3));
            }

            if (isHitCombo == 1)
            {
                SetState((int)BossAIState.hit1);
                StartCoroutine(EffectOn(1));
            }
            else if (isHitCombo == 2)
            {
                SetState((int)BossAIState.hit2);
                StartCoroutine(EffectOn(2));
            }
            else if (isHitCombo == 3)
            {
                SetState((int)BossAIState.hit3);
                StartCoroutine(EffectOn(3));
            }
            else if (isHitCombo == 4) // 아래 공격
            {
                SetState((int)BossAIState.underHit);
                StartCoroutine(EffectOn(4));
            }

            if (changeAttack == true)
            {
                SetState((int)BossAIState.idle);
            }
        }
        if (state == 3) // AIhit1
        {
            if (isDie == true)
            {
                SetState((int)BossAIState.hit3);
                StartCoroutine(EffectOn(3));
            }

            if (isHitCombo == 0)
            {
                SetState((int)BossAIState.idle);
            }
            else if (isHitCombo == 2)
            {
                SetState((int)BossAIState.hit2);
                StartCoroutine(EffectOn(2));
            }
            else if (isHitCombo == 3)
            {
                SetState((int)BossAIState.hit3);
                StartCoroutine(EffectOn(3));
            }
            else if (isHitCombo == 4) // 아래 공격
            {
                SetState((int)BossAIState.underHit);
                StartCoroutine(EffectOn(4));
            }
        }
        if (state == 4) // AIhit2
        {
            if (isDie == true)
            {
                SetState((int)BossAIState.hit3);
                StartCoroutine(EffectOn(3));
            }

            if (isHitCombo == 0)
            {
                SetState((int)BossAIState.idle);
            }
            else if (isHitCombo == 3)
            {
                SetState((int)BossAIState.hit3);
                StartCoroutine(EffectOn(3));
            }
            else if (isHitCombo == 4) // 아래 공격
            {
                SetState((int)BossAIState.underHit);
                StartCoroutine(EffectOn(4));
            }
        }
        if (state == 5) // AIhit3
        {
            if (doNextState == true)
            {
                doNextState = false;
                if (isDie == true)
                {
                    SetState((int)BossAIState.die);
                }
                else
                {
                    SetState((int)BossAIState.standing);
                }
            }
        }
        if (state == 6) // AIunderHit
        {
            if (doNextState == true)
            {
                doNextState = false;
                SetState((int)BossAIState.idle);
            }
        }
        if (state == 7) // AIstanding
        {
            if (doNextState == true)
            {
                doNextState = false;
                SetState((int)BossAIState.idle);
            }
        }
    }

    // setter
    private void SetState(int state)
    {
        if (this.state == state) { return; }
        this.state = state;

        AIspawn.enabled = false;
        AIdie.enabled = false;
        AIidle.enabled = false;
        AImove.enabled = false;
        AIattack1.enabled = false;
        AIattack2.enabled = false;
        AIhit1.enabled = false;
        AIhit2.enabled = false;
        AIhit3.enabled = false;
        AIunderHit.enabled = false;
        AIstanding.enabled = false;

        switch (state)
        {
            case -1:
                AIdie.enabled = true;
                break;

            case 0:
                changeIdle = false;
                AIidle.enabled = true;
                StartCoroutine(ChangingIdleState());
                break;

            case 1:
                changeMove = 0;
                AImove.enabled = true;
                break;

            case 2:
                changeAttack = false;
                AIattack1.enabled = true;
                // 나중에 보스 작업 때 Attack1, 2 구분해야함
                break;

            case 3:
                AIhit1.enabled = true;
                break;

            case 4:
                AIhit2.enabled = true;
                break;

            case 5:
                AIhit3.enabled = true;
                break;

            case 6:
                AIunderHit.enabled = true;
                break;

            case 7:
                AIstanding.enabled = true;
                break;
        }
    }
    /// <summary>
    /// hit의 combo는 1~3, underhit는 4
    /// </summary>
    public void SetAttackCombo(int combo)
    {
        isHitCombo = combo;
    }
    /// <summary>
    /// 1 = 랜덤으로 공격 혹은 대기, 2 = 대기
    /// </summary>
    public void SetChangeMove(int number)
    {
        changeMove = number;
    }
    public void SetChangeAttackOn()
    {
        changeAttack = true;
    }
    public void SetDoNextStateTrue()
    {
        doNextState = true;
    }
    // getter
    public int GetState() { return state; }

    private IEnumerator ChangingIdleState()
    {
        yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        changeIdle = true;
        yield return null;
    }
    private IEnumerator EffectOn(int combo)
    {
        yield return new WaitForSeconds(0.02f);
        switch (combo)
        {
            case 1:
                AIhit1.SetEffectOn();
                break;
            case 2:
                AIhit2.SetEffectOn();
                break;
            case 3:
                AIhit3.SetEffectOn();
                break;
            case 4:
                AIunderHit.SetEffectOn();
                break;
        }
        yield return null;
    }
}
