using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimatorScript : MonoBehaviour
{
    public MonsterAI monsterAI;

    private void Start()
    {
        monsterAI = transform.parent.GetChild(3).GetComponent<MonsterAI>();
    }

    public void IsAttack() { monsterAI.SetChangeAttackOn(); }
    public void ResetAttackCombo() { monsterAI.SetAttackCombo(0); }
    public void NextState() { monsterAI.SetDoNextStateTrue(); }
    public void SetState(int state) { monsterAI.SetState(state); }
}
