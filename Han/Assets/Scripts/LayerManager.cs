using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] monster = new GameObject[16];

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        /*
        monster = GameObject.FindGameObjectsWithTag("Monster");
        for (int i = 0; i < 16; i++)
        {
            while (true)
            {
                if (i >= 16) { break; }

                if (monster[i] == null) { i++; }
                else { break; }
            }
            if (i >= 16) { break; }

            for (int j = 0; j < 16; j++)
            {
                while (true)
                {
                    if (j >= 16) { break; }

                    if (monster[j] == null || i == j) { j++; }
                    else { break; }
                }
                if (j >= 16) { break; }

                if (monster[i] == monster[j]) { monster[j] = null; }
            }
        }
        */
    }
}
