using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarScript : MonoBehaviour
{
    public Player playerScript;
    public Image hpImage;
    public RectTransform hpRT, effectRT;

    public void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        hpImage = transform.GetChild(2).GetComponent<Image>();
        hpRT = transform.GetChild(2).GetComponent<RectTransform>();
        effectRT = transform.GetChild(3).GetComponent<RectTransform>();
    }
    private void Update()
    {
        hpImage.fillAmount = playerScript.GetPlayerHP() / 100.0f;
        effectRT.localPosition = new Vector3(175.0f + (playerScript.GetPlayerHP() * 6.5f), hpRT.localPosition.y + 20.0f, 0.0f);
    }
}
