using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class questionHandler2 : MonoBehaviour
{
    // Start is called before the first frame update

    public Button[] wrongButtons; // �߸��� ��ư
    public TMP_Text wrongText; // �߸��� �ؽ�Ʈ
    public Button correctButton; // �ùٸ� ��ư
    public TMP_Text correctText; // �ùٸ� �ؽ�Ʈ
    public GameObject door; // �� ����
    private Life lifeEvent; // Life ��ũ��Ʈ�� �޾ƿ�
    public Button[] buttons; // ���� ���� ���� ��ư��
    private bool isButtonClickable = true; // ��ư Ŭ�� ������ ���� ����
    private bool questionCorrect = false;

    public AudioClip correctSound; // �ùٸ� �� �Ҹ�
    public AudioClip wrongSound;   // Ʋ�� �� �Ҹ�

    private AudioSource audioSource;


    private CoinCollection coins; 

    [SerializeField] int quizDifficulty; // ���� ���̵� ����
    static private int quizNum = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        door.SetActive(false);
        wrongText.gameObject.SetActive(false); // ó���� �߸��� �ؽ�Ʈ �Ⱥ��̰�
        correctText.gameObject.SetActive(false); // ó���� �ùٸ� �ؽ�Ʈ �Ⱥ��̰�

        lifeEvent = FindObjectOfType<Life>(); // Life ��ũ��Ʈ�� ã�Ƽ� �ʱ�ȭ
        coins = FindAnyObjectByType<CoinCollection>();

        foreach (Button wrongButton in wrongButtons) //Ʋ�� ��ư�鿡 ����
        {
            wrongButton.onClick.AddListener(() => ActivateWrongText(wrongSound));
        }

        correctButton.onClick.AddListener(() => ActivateCorrectText(correctSound)); // �ùٸ� ��ư�� ������ �ùٸ� �ؽ�Ʈ�� ���̰�

        foreach (Button button in buttons) //��ư��
        {
            button.onClick.AddListener(() => buttonBlocked()); //������ ������
        }
        
    }

    void ActivateWrongText(AudioClip sound)
    {
        if (lifeEvent != null) // lifeEvent�� ���� null�� �ƴϸ�
        {
            switch (quizDifficulty) // ���� ���̵� ��ġ�� ���� �ִ� ������ �� ��ȭ
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
            lifeEvent.DecreaseLife(); // lifeEvent���� DecreaseLife �޼��� ���
            Debug.Log("ü�� ����");
            wrongText.gameObject.SetActive(true); // �߸��� �ؽ�Ʈ ����
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
        wrongText.gameObject.SetActive(false); // �߸��� �ؽ�Ʈ ����
        correctText.gameObject.SetActive(true); // �ùٸ� �ؽ�Ʈ ����
        coins.Coin += 10;
        coins.Score.text = coins.Coin.ToString();
        quizNum++;
        if (quizNum >= CorrectCount)
        {
            door.SetActive(true); // �� �Ⱥ���
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
        if (questionCorrect) // ������ ��ģ ���¶�� 
        {
            if (isButtonClickable)
            {
                isButtonClickable = false; // ��ư Ŭ�� �Ұ��� ���·� ����
                foreach (Button button in buttons) // ��ư�鿡 ����
                {
                    button.interactable = false; // ��� ��ư ��Ȱ��ȭ
                }
            }
        }
    }
}