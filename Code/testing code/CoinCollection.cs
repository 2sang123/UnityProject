using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public int Coin = 0; //���� ������ �����ϴ� ����

    public TextMeshProUGUI Score; //UI ���ھ� �ؽ�Ʈ

    public AudioClip pickupSound; // ������ ȹ�� �Ҹ�
    private AudioSource audioSource; // AudioSource ������Ʈ

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource ������Ʈ�� ���� ��� �߰�
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Coin") //�浹�� ������Ʈ�� �±װ� "Coin"���� Ȯ��
        {
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
            Coin++; // ���� ���� ����
            Score.text = Coin.ToString(); // UI�� ���� ���� ǥ��
            Destroy(other.gameObject); // �浹�� ���� ������Ʈ �����
        }

        if (other.transform.tag == "Jewel") //�浹�� ������Ʈ�� �±װ� "Coin"���� Ȯ��
        {
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
            Coin +=10; // ���� ���� ����
            Score.text = Coin.ToString(); // UI�� ���� ���� ǥ��
            Destroy(other.gameObject); // �浹�� ���� ������Ʈ �����
        }
    }

    private void OnDestroy()
    {
        //���� ���� �� ���� ������ PlayerPrefs�� ����
        PlayerPrefs.SetInt("CoinCount", Coin);
        PlayerPrefs.Save();
    }
}
