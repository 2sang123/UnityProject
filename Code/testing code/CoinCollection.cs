using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public int Coin = 0; //코인 개수를 저장하는 변수

    public TextMeshProUGUI Score; //UI 스코어 텍스트

    public AudioClip pickupSound; // 아이템 획득 소리
    private AudioSource audioSource; // AudioSource 컴포넌트

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource 컴포넌트가 없을 경우 추가
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Coin") //충돌한 오브젝트의 태그가 "Coin"인지 확인
        {
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
            Coin++; // 코인 개수 증가
            Score.text = Coin.ToString(); // UI에 코인 개수 표시
            Destroy(other.gameObject); // 충돌한 코인 오브젝트 사라짐
        }

        if (other.transform.tag == "Jewel") //충돌한 오브젝트의 태그가 "Coin"인지 확인
        {
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
            Coin +=10; // 코인 개수 증가
            Score.text = Coin.ToString(); // UI에 코인 개수 표시
            Destroy(other.gameObject); // 충돌한 코인 오브젝트 사라짐
        }
    }

    private void OnDestroy()
    {
        //게임 종료 시 코인 개수를 PlayerPrefs에 저장
        PlayerPrefs.SetInt("CoinCount", Coin);
        PlayerPrefs.Save();
    }
}
