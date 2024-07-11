using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 설명: 이 코드 생명력 UI 및 기능을 담당하는 코드 입니다.

public class Life : MonoBehaviour
{
    public Slider lifeSlider; // 슬라이더

    private float recoveryTimer = 0f;
    public float recoveryInterval = 1f;

    public Image bloodScreen;
    private AudioSource audioSource;

    public AudioClip spearImpactSound; // Declare spear impact sound variable
    public AudioClip arrowImpactSound; // Declare arrow impact sound variable
    public AudioClip flameImpactSound; // Declare flame impact sound variable
    public AudioClip recoverySound;

    private bool isSoundPlaying = false;


    public float decrease = 10f; // 깍을 라이프 수치

    private void Start()
    {
        lifeSlider = GameObject.FindAnyObjectByType<Slider>();

        UpdateSlider(100); // 게임 시작 시 생명력 100으로 초기화

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (lifeSlider.value <= 0) // 생명력이 0 이하면 게임 오버
        {
            GameOver();
        }
    }

    public void DecreaseLife() // 생명력 감소
    {      
        Debug.Log("체력 감소");

        if (bloodScreen != null) // 피격 이미지
        {
            bloodScreen.gameObject.SetActive(true);
            StartCoroutine(HideBloodScreen());
        }

        UpdateSlider(lifeSlider.value - decrease); // lifeSlider.value에 감소된 값을 저장
    }

    public void RecoveryLife(float amount)
    {
        UpdateSlider(lifeSlider.value + 1); // Recover health, maintain maximum of 30
        if(!isSoundPlaying)
        {
            if (audioSource != null && recoverySound != null)
            {
                audioSource.PlayOneShot(recoverySound);
                StartCoroutine(WaitForRecoverySound());
                isSoundPlaying = true;
            }
        }
        
    }

    IEnumerator HideBloodScreen()
    {
        Color originalColor = bloodScreen.color; // 현재 색상 저장
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // 투명한 색상 생성

        yield return new WaitForSeconds(0.5f); // 원하는 시간만큼 대기

        // 투명화 애니메이션 (1초 동안)
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            bloodScreen.color = Color.Lerp(originalColor, transparentColor, elapsedTime);
            elapsedTime += Time.deltaTime / 0.3f; // 1초 동안 진행
            yield return null;
        }

        bloodScreen.gameObject.SetActive(false); // 이미지 비활성화
        bloodScreen.color = originalColor; // 원래 색상으로 복구
    }


    /* public void RecoverLife(float amount)
     {
         if (lifeSlider.value < 100f) // Check if the player is not already at full health
         {
             UpdateSlider(Mathf.Min(lifeSlider.value + amount, 100f)); // Recover health, maintain maximum of 30
         }
     }*/

    private void GameOver()
    {       
            Debug.Log("Game Over");
            TimeManager.Instance.StopGameTime();
            SceneManager.LoadScene("GameOverScene"); // 게임 오버 씬으로 전환 
    }
    private void UpdateSlider(float newValue)
    {
        // 슬라이더의 Value 값을 변경
        if(lifeSlider != null)
        {
            lifeSlider.value = newValue;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // 물체와의 충돌 처리 로직 추가
        if (lifeSlider != null)
        {
            if (other.CompareTag("Spear")) // spear 태그와 충돌한 경우
            {    
                decrease = 5f;
                Debug.Log(decrease + "Damage"); // 디버그 메시지 추가
                DecreaseLife(); // 생명력 감소
                DecreaseLifeWithSound("SpearHit"); // 생명력 감소 및 spear 피격음 재생
            }
            else if (other.CompareTag("Arrow")) // arrow 태그와 충돌한 경우
            {
                decrease = 15f;
                Debug.Log(decrease + "Damage"); // 디버그 메시지 추가
                DecreaseLife(); // 생명력 감소
                DecreaseLifeWithSound("ArrowHit"); // 생명력 감소 및 arrow 피격음 재생

            }
        }
    }

    private void DecreaseLifeWithSound(string collisionType)
    {
        Debug.Log("체력 감소");
        UpdateSlider(lifeSlider.value - decrease); // lifeSlider.value에 감소된 값을 저장

        AudioClip impactSound = null;

        // Choose the impact sound based on the collision type
        switch (collisionType)
        {
            case "SpearHit":
                impactSound = spearImpactSound;
                break;
            case "ArrowHit":
                impactSound = arrowImpactSound;
                break;
                // Add more cases for other collision types if needed
        }

        if (audioSource != null && impactSound != null)
        {
            audioSource.PlayOneShot(impactSound);
        }
    }

    private IEnumerator WaitForRecoverySound()
    {
        yield return new WaitForSeconds(recoverySound.length);
        isSoundPlaying = false;
    }

    private IEnumerator WaitForFlameSound()
    {
        yield return new WaitForSeconds(flameImpactSound.length);
        isSoundPlaying = false;
    }

    private void OnTriggerStay(Collider other)
    {

        if (lifeSlider != null)
        {
            if (other.CompareTag("Recovery"))
            {
                RecoveryLife(1f); // 초당 10 회복
            }
            else if (other.CompareTag("Flame"))
            {
                decrease = 1f;
                DecreaseLife();
                // Play flame impact sound
                if (!isSoundPlaying)
                {
                    if (audioSource != null && flameImpactSound != null)
                    {
                        audioSource.PlayOneShot(flameImpactSound);
                        StartCoroutine(WaitForFlameSound());
                        isSoundPlaying = true;
                    }
                }
               
            }
        } 
    }

}
