using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ������ ��ư ���� �Ǵ� �ڵ�

public class questionHandler : MonoBehaviour
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

    private CoinCollection coins;

    public AudioClip correctSound; // �ùٸ� �� �Ҹ�
    public AudioClip wrongSound;   // Ʋ�� �� �Ҹ�

    private AudioSource audioSource;

    [SerializeField] int quizDifficulty; // ���� ���̵� ����
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        coins = FindAnyObjectByType<CoinCollection>();

        wrongText.gameObject.SetActive(false); // ó���� �߸��� �ؽ�Ʈ �Ⱥ��̰�
        correctText.gameObject.SetActive(false); // ó���� �ùٸ� �ؽ�Ʈ �Ⱥ��̰�

       lifeEvent = FindObjectOfType<Life>(); // Life ��ũ��Ʈ�� ã�Ƽ� �ʱ�ȭ

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
                    lifeEvent.decrease = 0f;
                    break;
                case 1:
                    lifeEvent.decrease = 5f;
                    break;
                case 2:
                    lifeEvent.decrease = 10f;
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

    public void ActivateCorrectText(AudioClip sound)
    {
        wrongText.gameObject.SetActive(false); // �߸��� �ؽ�Ʈ ����
        correctText.gameObject.SetActive(true); // �ùٸ� �ؽ�Ʈ ����
        door.SetActive(false); // �� �Ⱥ���        
        questionCorrect = true;

        coins.Coin += 1;
        coins.Score.text = coins.Coin.ToString();

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
