using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 스피드업 아이템 관련 코드
public class SpeedUp : MonoBehaviour
{
    
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode activationKey = KeyCode.Alpha1;
    public TextMeshProUGUI slotText; // 슬롯 텍스트
    public string pickupTag = "SpeedUp"; // 태그

    public float speedBoostDuration = 10f; // 스피드 업 지속 시간
    public float speedMultiplier = 2f; // 스피드 업 배수

    public GameObject effectUi; // 스피드업 시 보여주는 ui
    public GameObject effectVolume; // 스피드업 시 보여주는 화면 효과
    public AudioClip pickupSound; // 아이템 획득 소리

    private int speedUpItemCount = 0; // 아이템 개수

    private bool isSpeedBoostActive = false; // 스피드 업 아이템 활성화 여부

    private float originalMoveSpeed; // 원래 이동 속도
    private AudioSource audioSource; // AudioSource 컴포넌트

    private void Start()
    {
        originalMoveSpeed = GetComponent<Movement>().walkSpeed; // 원래 이동 속도 저장
        UpdateSlotText(); //텍스트 업데이트

        effectUi.SetActive(false);
        effectVolume.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource 컴포넌트가 없을 경우 추가
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        //if (Application.platform == RuntimePlatform.WindowsEditor)
        {   // 현재 플랫폼이 Window 에디터인지

            if (Input.GetKeyDown(pickupKey))
            {
                Ray cast = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 위치 저장

                RaycastHit hit; //레이캐스트

                if (Physics.Raycast(cast, out hit)) //마우스 위치로 레이캐스트
                {

                    if (hit.collider.gameObject.CompareTag(pickupTag))
                    {
                        PickUpItem(hit.collider.gameObject); //태그 아이템 획득
                    }
                }

            }
            if (Input.GetKeyDown(activationKey) && speedUpItemCount > 0 && !isSpeedBoostActive) // 사용, 개수 감소
            {
                ActivateSpeedBoost();

                speedUpItemCount--;
                UpdateSlotText();
            }
        }
    }
    private void PickUpItem(GameObject item)
    {
        // 스피드 업 아이템을 획득하면 슬롯에 저장하고 개수를 증가시키고 오브젝트 삭제
        speedUpItemCount++;
        UpdateSlotText();
        Destroy(item);
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }
    private void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        isSpeedBoostActive = true;

        Movement playerController = GetComponent<Movement>();
        float originalWalkSpeed = playerController.walkSpeed;
        playerController.SetSpeed(originalWalkSpeed * speedMultiplier); // 이동 속도 증가
        effectUi.SetActive(true);
        effectVolume.SetActive(true);

        yield return new WaitForSeconds(speedBoostDuration);

        playerController.SetSpeed(originalWalkSpeed); // 이동 속도 원래 값으로 복구
        isSpeedBoostActive = false; ;
        effectUi.SetActive(false);
        effectVolume.SetActive(false);
    }

    private void UpdateSlotText()
    {
        slotText.text = speedUpItemCount.ToString();
    }

}