using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public GameObject monster, mainCamera, player;
    public CameraManager cameraManager;
    public Monster monsterScript;
    public MonsterAI monsterAI;
    public Animator animator;
    public int tempInt, randomKey;

    private void Start()
    {
        monster = transform.parent.gameObject;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        cameraManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CameraManager>();
        monsterScript = monster.GetComponent<Monster>();
        monsterAI = gameObject.GetComponent<MonsterAI>();
        animator = monster.transform.GetChild(0).GetComponent<Animator>();

        tempInt = 0;
        randomKey = -1;
    }
    private void Update()
    {
        if (animator.GetInteger("State") != 1) { animator.SetInteger("State", 1); }

        if (randomKey == -1) { randomKey = Random.Range(0, 6); }
        
        switch (randomKey)
        {
            case 0: // 플레이어에게 이동
                if (tempInt == 0)
                {
                    if (monster.transform.position.x > player.transform.position.x)
                    {
                        tempInt = 1;
                    }
                    else
                    {
                        tempInt = -1;
                    }
                }

                monster.transform.localScale = new Vector3(-tempInt, 1.0f, 1.0f);
                
                if (Mathf.Abs(monster.transform.position.y - player.transform.position.y) < 0.25f &&
                    Mathf.Abs(monster.transform.position.x - player.transform.position.x) > 1.25f &&
                    Mathf.Abs(monster.transform.position.x - player.transform.position.x) < 1.75f)
                {
                    tempInt = 0;
                    randomKey = -1;
                    monsterAI.SetChangeMove(1);
                }

                monster.transform.position = Vector3.MoveTowards(monster.transform.position,
                    new Vector3(player.transform.position.x + tempInt * 1.5f, player.transform.position.y, 0.0f),
                    monsterScript.GetSpeed() * Time.deltaTime);
                break;

            case 1: // 화면 아래 이동
                if (monster.transform.position.y <= mainCamera.transform.position.y + cameraManager.GetYMin())
                {
                    randomKey = -1;
                    monsterAI.SetChangeMove(2);
                }

                monster.transform.Translate(0.0f, -monsterScript.GetSpeed() * Time.deltaTime, 0.0f);
                break;

            case 2: // 화면 아래 대각선 이동
                if (tempInt == 0)
                {
                    if (monster.transform.position.x > player.transform.position.x)
                    {
                        tempInt = -1;
                    }
                    else
                    {
                        tempInt = 1;
                    }
                }

                monster.transform.localScale = new Vector3(tempInt, 1.0f, 1.0f);
                
                if (monster.transform.position.y < mainCamera.transform.position.y + cameraManager.GetYMin())
                {
                    monster.transform.position = new Vector3(monster.transform.position.x,
                        mainCamera.transform.position.y + cameraManager.GetYMin(),
                        0.0f);
                    tempInt = 0;
                    randomKey = -1;
                    monsterAI.SetChangeMove(2);
                }
                else if (tempInt == -1 &&
                    monster.transform.position.x < mainCamera.transform.position.x + cameraManager.GetXMin())
                {
                    monster.transform.position = new Vector3(mainCamera.transform.position.x + cameraManager.GetXMin(),
                        monster.transform.position.y,
                        0.0f);
                    tempInt = 0;
                    randomKey = -1;
                    monsterAI.SetChangeMove(2);
                }
                else if (tempInt == 1 &&
                    monster.transform.position.x > mainCamera.transform.position.x + cameraManager.GetXMax())
                {
                    monster.transform.position = new Vector3(mainCamera.transform.position.x + cameraManager.GetXMax(),
                        monster.transform.position.y,
                        0.0f);
                    tempInt = 0;
                    randomKey = -1;
                    monsterAI.SetChangeMove(2);
                }

                monster.transform.Translate(tempInt * monsterScript.GetSpeed() * Mathf.Sqrt(2.0f) * 0.5f * Time.deltaTime,
                    -monsterScript.GetSpeed() * Mathf.Sqrt(2.0f) * 0.5f * Time.deltaTime, 0.0f);
                break;

            case 3: // 화면 위로 이동
                if (monster.transform.position.y >= mainCamera.transform.position.y + cameraManager.GetYMax())
                {
                    randomKey = -1;
                    monsterAI.SetChangeMove(2);
                }

                monster.transform.Translate(0.0f, monsterScript.GetSpeed() * Time.deltaTime, 0.0f);
                break;

            case 4: // 화면 위 대각선 이동
                if (tempInt == 0)
                {
                    if (monster.transform.position.x > player.transform.position.x)
                    {
                        tempInt = -1;
                    }
                    else
                    {
                        tempInt = 1;
                    }
                }

                monster.transform.localScale = new Vector3(tempInt, 1.0f, 1.0f);

                if (monster.transform.position.y > mainCamera.transform.position.y + cameraManager.GetYMax())
                {
                    monster.transform.position = new Vector3(monster.transform.position.x,
                        mainCamera.transform.position.y + cameraManager.GetYMax(),
                        0.0f);
                    tempInt = 0;
                    randomKey = -1;
                    monsterAI.SetChangeMove(2);
                }
                else if (tempInt == -1 &&
                    monster.transform.position.x < mainCamera.transform.position.x + cameraManager.GetXMin())
                {
                    monster.transform.position = new Vector3(mainCamera.transform.position.x + cameraManager.GetXMin(),
                        monster.transform.position.y,
                        0.0f);
                    tempInt = 0;
                    randomKey = -1;
                    monsterAI.SetChangeMove(2);
                }
                else if (tempInt == 1 &&
                    monster.transform.position.x > mainCamera.transform.position.x + cameraManager.GetXMax())
                {
                    monster.transform.position = new Vector3(mainCamera.transform.position.x + cameraManager.GetXMax(),
                        monster.transform.position.y,
                        0.0f);
                    tempInt = 0;
                    randomKey = -1;
                    monsterAI.SetChangeMove(2);
                }

                monster.transform.Translate(tempInt * monsterScript.GetSpeed() * Mathf.Sqrt(2.0f) * 0.5f * Time.deltaTime,
                    monsterScript.GetSpeed() * Mathf.Sqrt(2.0f) * 0.5f * Time.deltaTime, 0.0f);
                break;

            case 5: // 뒷걸음질
                if (tempInt == 0)
                {
                    if (monster.transform.position.x > player.transform.position.x)
                    {
                        tempInt = 1;
                    }
                    else
                    {
                        tempInt = -1;
                    }
                }

                monster.transform.localScale = new Vector3(tempInt, 1.0f, 1.0f);

                if (tempInt == -1 &&
                    monster.transform.position.x < mainCamera.transform.position.x + cameraManager.GetXMin())
                {
                    monster.transform.position = new Vector3(mainCamera.transform.position.x + cameraManager.GetXMin(),
                        monster.transform.position.y,
                        0.0f);
                    tempInt = 0;
                    randomKey = -1;
                    monsterAI.SetChangeMove(2);
                }
                else if (tempInt == 1 &&
                    monster.transform.position.x > mainCamera.transform.position.x + cameraManager.GetXMax())
                {
                    monster.transform.position = new Vector3(mainCamera.transform.position.x + cameraManager.GetXMax(),
                        monster.transform.position.y,
                        0.0f);
                    tempInt = 0;
                    randomKey = -1;
                    monsterAI.SetChangeMove(2);
                }

                monster.transform.Translate(tempInt * monsterScript.GetSpeed() * Time.deltaTime, 0.0f, 0.0f);
                break;
        }
    }

    // setter
    public void SetTempIntZero() { tempInt = 0; }
    // getter
    public bool GetTempIntIsZero() { return tempInt == 0; }
}
