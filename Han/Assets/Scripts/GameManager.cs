using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cutScene;

    private void Start()
    {
        cutScene = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (cutScene.activeSelf == false) { cutScene.SetActive(true); }
            else { cutScene.SetActive(false); }
        }
    }
}
