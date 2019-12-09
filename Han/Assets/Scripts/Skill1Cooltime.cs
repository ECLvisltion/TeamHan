using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill1Cooltime : MonoBehaviour
{
    public PlayerMovement pm;
    public Image image;

    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerMovement>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = pm.GetSkillCooltime(0);
    }
}
