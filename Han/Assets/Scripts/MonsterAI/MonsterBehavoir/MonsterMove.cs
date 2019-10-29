using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonsterState
{
    public GameObject player;
    public GameObject mainCamera;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void Update()
    {

    }

    public IEnumerator Move()
    {
        yield return new WaitForSecondsRealtime(Random.Range(0.1f, 3.0f));

        int key = Random.Range(0, 4);
        switch (key)
        {
            case 0:
                // 플레이어를 향해 이동
                Vector3.MoveTowards(transform.position, player.transform.position, 1.0f);
                break;

            case 1:
                // 화면 하단으로 이동
                Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, mainCamera.transform.position.y - 3, 0.0f), 1.0f);
                break;

            case 2:
                // 화면 위로 이동
                Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, mainCamera.transform.position.y + 1, 0.0f), 1.0f);
                break;

            case 3:
                // 뒷걸음질 친다
                break;
        }

        yield return null;
    }
}
