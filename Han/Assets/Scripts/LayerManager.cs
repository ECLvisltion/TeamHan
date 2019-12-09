using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    private const int monsterCount = 16;
    
    public GameObject player;
    public GameObject[] monster;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        monster = GameObject.FindGameObjectsWithTag("Monster");

        for(int i = 0; i < monster.Length; i++)
        {
            int layer = 0;

            if (monster[i].transform.position.y > player.transform.position.y)
            {
                layer = -1;
            }
            else
            {
                layer = 1;
            }

            for (int j = 0; j < monster.Length; j++)
            {
                if (monster[i] != monster[j])
                {
                    if (layer > 0)
                    {
                        if (monster[j].transform.position.y < player.transform.position.y)
                        {
                            if (monster[i].transform.position.y < monster[j].transform.position.y) { layer++; }
                        }
                    }
                    else
                    {
                        if (monster[j].transform.position.y > player.transform.position.y)
                        {
                            if (monster[i].transform.position.y > monster[j].transform.position.y) { layer--; }
                        }
                    }
                }
            }

            monster[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = layer;
        }
    }

    public int GetMonsterCount() { return monster.Length; }
}
