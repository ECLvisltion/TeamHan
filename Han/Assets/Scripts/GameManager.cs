using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainCamera;
    public PlayerMovement playerMovement;
    public FadeScript fade;
    public GameObject cutScene;
    public CutSceneScript cutSceneScript;
    public GameObject[] monster;
    public int progress;
    public bool nextProgress;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        fade = GameObject.Find("Fade").GetComponent<FadeScript>();
        cutScene = GameObject.Find("CutScene");
        cutSceneScript = cutScene.GetComponent<CutSceneScript>();
        progress = 0;
        nextProgress = true;
    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (cutScene.activeSelf == false) { cutScene.SetActive(true); }
            else { cutScene.SetActive(false); }
        }
        */

        if (nextProgress == true)
        {
            progress++;
            nextProgress = false;

            switch (progress)
            {
                case 1:
                    playerMovement.enabled = false;
                    cutSceneScript.SetScenarioNumber(2);
                    cutScene.SetActive(true);
                    break;

                case 2:
                    playerMovement.enabled = true;
                    cutScene.SetActive(false);
                    Instantiate(Resources.Load<GameObject>("Prefabs/Monster"),
                        new Vector3(mainCamera.transform.position.x - 12.0f, mainCamera.transform.position.y - 2.0f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Instantiate(Resources.Load<GameObject>("Prefabs/Monster"),
                        new Vector3(mainCamera.transform.position.x - 12.0f, mainCamera.transform.position.y - 3.0f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Instantiate(Resources.Load<GameObject>("Prefabs/Monster"),
                        new Vector3(mainCamera.transform.position.x + 12.0f, mainCamera.transform.position.y - 2.0f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Instantiate(Resources.Load<GameObject>("Prefabs/Monster"),
                        new Vector3(mainCamera.transform.position.x + 12.0f, mainCamera.transform.position.y - 3.0f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    break;

                case 3:
                    progress--;
                    Instantiate(Resources.Load<GameObject>("Prefabs/Monster"),
                        new Vector3(mainCamera.transform.position.x - 12.0f, mainCamera.transform.position.y - 2.0f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Instantiate(Resources.Load<GameObject>("Prefabs/Monster"),
                        new Vector3(mainCamera.transform.position.x - 12.0f, mainCamera.transform.position.y - 2.5f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Instantiate(Resources.Load<GameObject>("Prefabs/Monster"),
                        new Vector3(mainCamera.transform.position.x - 12.0f, mainCamera.transform.position.y - 3.0f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Instantiate(Resources.Load<GameObject>("Prefabs/Monster"),
                        new Vector3(mainCamera.transform.position.x + 12.0f, mainCamera.transform.position.y - 2.0f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Instantiate(Resources.Load<GameObject>("Prefabs/Monster"),
                        new Vector3(mainCamera.transform.position.x + 12.0f, mainCamera.transform.position.y - 2.5f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    Instantiate(Resources.Load<GameObject>("Prefabs/Monster"),
                        new Vector3(mainCamera.transform.position.x + 12.0f, mainCamera.transform.position.y - 3.0f, 0.0f),
                        Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    break;
            }
        }

        // 몬스터 개수 세기
        monster = GameObject.FindGameObjectsWithTag("Monster");

        if (progress == 2 || progress == 3)
        {
            if (monster.Length == 0)
            {
                nextProgress = true;
            }
        }
    }

    // getter
    public int GetProgress() { return progress; }

    public IEnumerator ChangeSceneWithFade(int sceneNumber)
    {
        StartCoroutine(fade.FadeOut());
        yield return new WaitForSeconds(fade.GetFadeTickTime() * 50.0f);
        SceneManager.LoadScene(sceneNumber);
        StartCoroutine(fade.FadeIn());
        yield return null;
    }
    public IEnumerator GameOver()
    {
        StartCoroutine(fade.FadeOut(240, 0.0f, 1.0f));
        yield return new WaitForSeconds(fade.GetFadeTickTime() * 240.0f);
        SceneManager.LoadScene(0);
        /*Destroy(fade.gameObject);
        Destroy(cutScene.gameObject);
        Destroy(gameObject);*/
        yield return null;
    }

    public void NextProgress() { nextProgress = true; }
}
