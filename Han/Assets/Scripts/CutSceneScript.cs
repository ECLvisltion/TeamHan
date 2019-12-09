using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneScript : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject leftImage, rightImage, leftNameImage, rightNameImage;
    public Text leftName, rightName, text;
    public int scenario;
    public bool start;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        leftImage = transform.GetChild(0).gameObject;
        rightImage = transform.GetChild(1).gameObject;
        leftNameImage = transform.GetChild(2).GetChild(0).gameObject;
        rightNameImage = transform.GetChild(2).GetChild(1).gameObject;
        leftName = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        rightName = transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>();
        text = transform.GetChild(2).GetChild(2).GetComponent<Text>();

        scenario = -1;
        start = false;
    }
    private void Update()
    {
        /*
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
        */

        switch(scenario)
        {
            case -1:
                gameObject.SetActive(false);
                break;

            case 2:
                {
                    leftImage.SetActive(false);
                    rightImage.SetActive(false);
                    leftNameImage.SetActive(false);
                    rightNameImage.SetActive(false);

                    leftName.text = "";
                    rightName.text = "";

                    if (start == false)
                    {
                        start = true;
                        StartCoroutine(Typing("여긴.. 어디지?"));
                    }

                    if (Input.anyKeyDown && NextScript("여긴.. 어디지?"))
                    {
                        StartCoroutine(Typing("일본 놈들 칼에 맞을 뻔했던 기억은 나지만.. 내가 왜 여기에 있는지 모르겠군."));
                    }

                    if (Input.anyKeyDown && NextScript("일본 놈들 칼에 맞을 뻔했던 기억은 나지만.. 내가 왜 여기에 있는지 모르겠군."))
                    {
                        StartCoroutine(Typing("이 어둠 속.. 유일하게 보이는 건 내 앞에 앉아있는 자네 뿐이군."));
                    }

                    if (Input.anyKeyDown && NextScript("이 어둠 속.. 유일하게 보이는 건 내 앞에 앉아있는 자네 뿐이군."))
                    {
                        StartCoroutine(Typing("자네가 날 이곳에 데리고 온 건가?"));
                    }

                    if (Input.anyKeyDown && NextScript("자네가 날 이곳에 데리고 온 건가?")) // 선택지 버튼 "아니요."
                    {
                        StartCoroutine(Typing("그렇다면 자네도 여기 왜 와있는지 모르겠단 말이로근."));
                    }

                    if (Input.anyKeyDown && NextScript("그렇다면 자네도 여기 왜 와있는지 모르겠단 말이로근."))
                    {
                        StartCoroutine(Typing("이거 난감하게 됐는데.. 것보다 자네. 희한한 옷을 입고 있군 그래."));
                    }

                    if (Input.anyKeyDown && NextScript("이거 난감하게 됐는데.. 것보다 자네. 희한한 옷을 입고 있군 그래."))
                    {
                        StartCoroutine(Typing("참 기묘한 모습이야. 마치 다른 시대에서 온 것처럼.. "));
                    }

                    if (Input.anyKeyDown && NextScript("참 기묘한 모습이야. 마치 다른 시대에서 온 것처럼.. ")) // 선택지 버튼 "나는 미래에서 온 사람입니다."
                    {
                        StartCoroutine(Typing("뭐? 미래에서 왔다고?"));
                    }

                    if (Input.anyKeyDown && NextScript("뭐? 미래에서 왔다고?"))
                    {
                        StartCoroutine(Typing("거 참, 믿을 수 없는 얘기군. "));
                    }

                    if (Input.anyKeyDown && NextScript("거 참, 믿을 수 없는 얘기군. "))
                    {
                        StartCoroutine(Typing("거기는 어떤가. 우리 나라는.. 독립이 되었는가?"));
                    }

                    if (Input.anyKeyDown && NextScript("거기는 어떤가. 우리 나라는.. 독립이 되었는가?")) // 선택지 버튼 "독립이 되었습니다."
                    {
                        StartCoroutine(Typing("되었다고.. 독립이 되었다고..!"));
                    }

                    if (Input.anyKeyDown && NextScript("되었다고.. 독립이 되었다고..!"))
                    {
                        StartCoroutine(Typing("하하하.. 하하하하!"));
                    }

                    if (Input.anyKeyDown && NextScript("하하하.. 하하하하!"))
                    {
                        StartCoroutine(Typing("기쁘지 아니할 수가 없구나. 대경사요, 대경사."));
                    }

                    if (Input.anyKeyDown && NextScript("기쁘지 아니할 수가 없구나. 대경사요, 대경사."))
                    {
                        StartCoroutine(Typing("얼른 이 사실을 독립군들에게 알려야 겠어!"));
                    }

                    if (Input.anyKeyDown && NextScript("얼른 이 사실을 독립군들에게 알려야 겠어!"))
                    {
                        StartCoroutine(Typing("잠깐.. 그나저나 몸을 움직일 수가 없군."));
                    }

                    if (Input.anyKeyDown && NextScript("잠깐.. 그나저나 몸을 움직일 수가 없군.")) // 플레이어가 움직여야 진행됨. 몸이 해당 방향으로 움직이고 다시 정지 상태가 된다.
                    {
                        StartCoroutine(Typing("이럴 수가! 자네가 날 움직인 건가?"));
                    }

                    if (Input.anyKeyDown && NextScript("이럴 수가! 자네가 날 움직인 건가?")) // 선택지 버튼 "그런 것 같은데.."
                    {
                        StartCoroutine(Typing("거 참 이상한 일이로군."));
                    }

                    if (Input.anyKeyDown && NextScript("거 참 이상한 일이로군."))
                    {
                        StartCoroutine(Typing("무슨 운명의 장난인지.. "));
                    }

                    if (Input.anyKeyDown && NextScript("무슨 운명의 장난인지.. "))
                    {
                        StartCoroutine(Typing("아마도 자네만이 날 움직일 수 있는 것 같군."));
                    }

                    if (Input.anyKeyDown && NextScript("아마도 자네만이 날 움직일 수 있는 것 같군."))
                    {
                        StartCoroutine(Typing("…"));
                    }

                    if (Input.anyKeyDown && NextScript("…"))
                    {
                        StartCoroutine(Typing("부탁이 있네. 부디 날 도와 독립군들이 있는 처소로 안내해주겠나?"));
                    }

                    if (Input.anyKeyDown && NextScript("부탁이 있네. 부디 날 도와 독립군들이 있는 처소로 안내해주겠나?"))
                    {
                        StartCoroutine(Typing("내 동지들에게 한 시라도 빨리 이 사실을 알리고 싶군."));
                    }

                    if (Input.anyKeyDown && NextScript("내 동지들에게 한 시라도 빨리 이 사실을 알리고 싶군.")) // 선택지 버튼 "알았어요."
                    {
                        // 스크립트 종료.
                        scenario = -1;
                        gameManager.NextProgress();
                    }
                }
                break;
        }
    }

    // setter
    public void SetScenarioNumber(int number) { scenario = number; start = false; }

    public IEnumerator Typing(string type)
    {
        text.text = "";

        for (int i = 0; i < type.Length; i++)
        {
            if (Input.anyKey && text.text.Length > 3)
            {
                text.text = type;
                break;
            }

            text.text += type[i];
            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }
    public bool NextScript(string type) { return text.text == type; }
}
