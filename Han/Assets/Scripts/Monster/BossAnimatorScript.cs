using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorScript : MonoBehaviour
{
    public BossAI bossAI;

    private void Start()
    {
        bossAI = transform.parent.GetChild(3).GetComponent<BossAI>();
    }

    public void IsAttack() { bossAI.SetChangeAttackOn(); }
    public void ResetAttackCombo() { bossAI.SetAttackCombo(0); }
    public void NextState() { bossAI.SetDoNextStateTrue(); }
}
