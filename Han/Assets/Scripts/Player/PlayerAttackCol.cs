using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCol : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;

    private void Start()
    {
        player = transform.parent.gameObject;
        playerScript = player.GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MonsterHitbox")
        {
            playerScript.SetMonsterHitbox(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MonsterHitbox")
        {
            playerScript.DeleteMonsterHitbox(collision.gameObject);
        }
    }
}
