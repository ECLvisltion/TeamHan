using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform playerTransform;
    public MonsterAI monsterAI;
    public float playerDistance;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        monsterAI = gameObject.transform.GetChild(3).GetComponent<MonsterAI>();
    }
    private void FixedUpdate()
    {
        
    }
}
