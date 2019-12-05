using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHit2 : MonoBehaviour
{
    public Animator animator;
    public bool effectOn;

    private void Start()
    {
        animator = transform.parent.GetChild(0).GetComponent<Animator>();
        effectOn = false;
    }
    private void Update()
    {
        if (animator.GetInteger("State") != 4) { animator.SetInteger("State", 4); }
        if (effectOn == true)
        {
            effectOn = false;
            Instantiate(Resources.Load<GameObject>("Prefabs/Effect/HitF_Week0100"),
                new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f),
                Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        }
    }

    public void SetEffectOn() { effectOn = true; }
}
