using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour
{
    public Player playerScript;
    public Image hpImage;

    public void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        hpImage = gameObject.GetComponent<Image>();
    }
    private void Update()
    {
        hpImage.fillAmount = playerScript.GetPlayerHP() / 100.0f;
    }
}
