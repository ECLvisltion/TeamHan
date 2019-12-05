using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHit3 : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public bool effectOn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = transform.parent.GetChild(0).GetComponent<Animator>();
        effectOn = false;
    }
    private void Update()
    {
        if (animator.GetInteger("State") != 5) { animator.SetInteger("State", 5); }
        if (effectOn == true)
        {
            effectOn = false;

            GameObject go;
            go = Instantiate(Resources.Load<GameObject>("Prefabs/Effect/HitF_Strong0100"),
                new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f),
                Quaternion.Euler(0.0f, 0.0f, 0.0f));
            if (player.transform.localScale.x < 0)
            {
                go.transform.localScale = new Vector3(go.transform.localScale.x * -1.0f,
                go.transform.localScale.y,
                1.0f);
            }
            else
            {
                go.transform.localScale = new Vector3(go.transform.localScale.x * +1.0f,
                go.transform.localScale.y,
                1.0f);
            }
        }
    }

    public void SetEffectOn() { effectOn = true; }
}
