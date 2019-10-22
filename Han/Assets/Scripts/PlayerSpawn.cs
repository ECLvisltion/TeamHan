using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
    }
}
