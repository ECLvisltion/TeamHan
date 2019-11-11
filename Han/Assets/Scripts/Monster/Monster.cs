using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform playerTransform;
    public MonsterAI monsterAI;
    public GameObject ma;
    public MonsterAnimator maScript;
    public float playerDistance;

    private void Start()
    {
        ma = transform.GetChild(0).gameObject;
        maScript = ma.GetComponent<MonsterAnimator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        monsterAI = gameObject.transform.GetChild(3).GetComponent<MonsterAI>();
    }
    private void FixedUpdate()
    {
        
    }

    
    public void Hit(int combo)
    {
        monsterAI.SetIsHit(true);
        maScript.Hit();
        if (combo == 3) { Instantiate(Resources.Load("Prefabs/Effect/HitF_Strong_00"), new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f)); }
        else { Instantiate(Resources.Load("Prefabs/Effect/HitF_Week_00"), new Vector3(transform.position.x, transform.position.y + 0.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))); }
        StartCoroutine(Shake());
    }
    public void AttackSucceed()
    {

    }

    private IEnumerator Shake()
    {
        ma.transform.Translate(0.1f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        ma.transform.Translate(-0.2f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        ma.transform.Translate(0.2f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        ma.transform.Translate(-0.2f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        ma.transform.Translate(0.15f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        ma.transform.Translate(-0.10f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        ma.transform.Translate(0.05f, 0.0f, 0.0f);
        yield return null;
    }
}
