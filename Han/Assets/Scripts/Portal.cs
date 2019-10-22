using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private SceneManager sceneManager;
    private GameObject player;
    public int goSceneNumber;

    private void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SceneManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            sceneManager.SceneChange(goSceneNumber, player, new Vector3(0.0f, 0.0f, 0.0f));
        }
    }
}
