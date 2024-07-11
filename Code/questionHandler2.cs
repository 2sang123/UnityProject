using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class questionHandler2 : MonoBehaviour
{
    // Start is called before the first frame update

    public Button[] wrongButtons; // 잘못된 버튼
    public TMP_Text wrongText; // 잘못된 텍스트
    public Button correctButton; // 올바른 버튼
    public TMP_Text correctText; // 올바른 텍스트
    public GameObject door; // 문 역할
    private Life lifeEvent; // Life 스크립트를 받아옴
    public Button[] buttons; // 문제 정답 선택 버튼들
    private bool isButtonClickable = true; // 버튼 클릭 가능한 상태 변수
    private bool questionCorrect = false;

    public AudioClip correctSound; // 올바른 답 소리
    public AudioClip wrongSound;   // 틀린 답 소리

    private AudioSource audioSource;


    private CoinCollection coins; 

    [SerializeField] int quizDifficulty; // 퀴즈 난이도 설정
    static private int quizNum = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        door.SetActive(false);
        wrongText.gameObject.SetActive(false); // 처음엔 잘못된 텍스트 안보이게
        correctText.gameObject.SetActive(false); // 처음엔 올바른 텍스트 안보이게

        lifeEvent = FindObjectOfType<Life>(); // Life 스크립트를 찾아서 초기화
        coins = FindAnyObjectByType<CoinCollection>();

        foreach (Button wrongButton in wrongButtons) //틀린 버튼들에 적용
        {
            wrongButton.onClick.AddListener(() => ActivateWrongText(wrongSound));
        }

        correctButton.onClick.AddListener(() => ActivateCorrectText(correctSound)); // 올바른 버튼을 누르면 올바른 텍스트가 보이게

        foreach (Button button in buttons) //버튼들
        {
            button.onClick.AddListener(() => buttonBlocked()); //누르면 닫히게
        }
        
    }

    void ActivateWrongText(AudioClip sound)
    {
        if (lifeEvent != null) // lifeEvent의 값이 null이 아니면
        {
            switch (quizDifficulty) // 퀴즈 난이도 수치에 따라 주는 데미지 값 변화
            {
                case 0:
                    lifeEvent.decrease = 10f;
                    break;
                case 1:
                    lifeEvent.decrease = 15f;;
                    break;
                case 2:
                    lifeEvent.decrease = 30f;
                 
                    break;
                default:
                    break;
            }
            lifeEvent.DecreaseLife(); // lifeEvent에서 DecreaseLife 메서드 사용
            Debug.Log("체력 감소");
            wrongText.gameObject.SetActive(true); // 잘못된 텍스트 보임
        }

        if (sound != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }

    }
    public static int CorrectCount = 3;
    public void ActivateCorrectText(AudioClip sound)
    {
        wrongText.gameObject.SetActive(false); // 잘못된 텍스트 보임
        correctText.gameObject.SetActive(true); // 올바른 텍스트 보임
        coins.Coin += 10;
        coins.Score.text = coins.Coin.ToString();
        quizNum++;
        if (quizNum >= CorrectCount)
        {
            door.SetActive(true); // 문 안보임
        }
        questionCorrect = true;

        if (sound != null)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
    void buttonBlocked()
    {
        if (questionCorrect) // 정답을 맞친 상태라면 
        {
            if (isButtonClickable)
            {
                isButtonClickable = false; // 버튼 클릭 불가능 상태로 변경
                foreach (Button button in buttons) // 버튼들에 적용
                {
                    button.interactable = false; // 모든 버튼 비활성화
                }
            }
        }
    }
}