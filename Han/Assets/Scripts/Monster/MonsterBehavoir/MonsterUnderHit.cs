using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUnderHit : MonoBehaviour
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
        if (animator.GetInteger("State") != 6) { animator.SetInteger("State", 6); }
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
