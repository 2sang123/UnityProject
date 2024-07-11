using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Ȯ�� ��ȯ�� ���̵� ȿ���� �ִ� �ڵ�
public class GameStartFadeEffect : MonoBehaviour
{
    GameManager gameManager;
    public AudioClip backgroundMusic;

    private Image fadeimage; // image�� �޾ƿ�
    public TextMeshProUGUI fadetext;

    public float delayBeforeFade = 1.2f;

    float fadeSpeed = 0.8f;



    public void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>(); // �߰�: GameManager ������Ʈ ã��
    }
    private void Start()
    {
        StartCoroutine(StartFadeEffect());
    }
    IEnumerator StartFadeEffect()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        fadeimage = GetComponent<Image>();
        StartCoroutine(FadeEffect());
    }


    IEnumerator FadeEffect()
    {
        Color color1 = fadeimage.color;
        Color color2 = fadetext.color;

        while (color1.a > 0 && color2.a > 0)
        {
            color1.a -= fadeSpeed * Time.deltaTime;
            color2.a -= (fadeSpeed + 0.1f) * Time.deltaTime;
            fadeimage.color = color1;
            fadetext.color = color2;

            yield return null;
        }
        Destroy(gameObject);

        if (backgroundMusic != null && gameManager != null)
        {
            gameManager.PlayBackgroundMusic(backgroundMusic);
        }
    }
}
