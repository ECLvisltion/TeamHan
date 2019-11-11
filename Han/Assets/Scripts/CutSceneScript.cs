using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneScript : MonoBehaviour
{
    public GameObject leftImage, rightImage, leftNameImage, rightNameImage;
    public Text leftName, rightName, text;

    private void Start()
    {
        leftImage = transform.GetChild(0).gameObject;
        rightImage = transform.GetChild(1).gameObject;
        leftNameImage = transform.GetChild(2).GetChild(0).gameObject;
        rightNameImage = transform.GetChild(2).GetChild(1).gameObject;
        leftName = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        rightName = transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>();
        text = transform.GetChild(2).GetChild(2).GetComponent<Text>();

        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            leftImage.SetActive(false);
            rightImage.SetActive(false);
            leftNameImage.SetActive(false);
            rightNameImage.SetActive(false);

            leftName.text = "";
            rightName.text = "";
            StartCoroutine(Typing("1111111111111111111111"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            leftImage.SetActive(true);
            rightImage.SetActive(false);
            leftNameImage.SetActive(true);
            rightNameImage.SetActive(false);

            leftImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/idle1");

            leftName.text = "왼";
            rightName.text = "";
            StartCoroutine(Typing("22222222222222222222222"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            leftImage.SetActive(false);
            rightImage.SetActive(true);
            leftNameImage.SetActive(false);
            rightNameImage.SetActive(true);

            rightImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/punch1_1");

            leftName.text = "";
            rightName.text = "오";
            StartCoroutine(Typing("333333333333333333333"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            leftImage.SetActive(true);
            rightImage.SetActive(true);
            leftNameImage.SetActive(true);
            rightNameImage.SetActive(true);

            leftImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/idle1");
            rightImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/punch1_1");

            leftName.text = "왼";
            rightName.text = "오";
            StartCoroutine(Typing("44444444444444444444"));
        }
    }

    public IEnumerator Typing(string type)
    {
        text.text = "";

        for (int i = 0; i < type.Length; i++)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                for (; i < type.Length; i++)
                {
                    text.text += type[i];
                }
                break;
            }

            text.text += type[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }
}
