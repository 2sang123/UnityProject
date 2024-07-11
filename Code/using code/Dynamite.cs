using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 폭탄 아이템 관련 코드
public class Dynamite : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.E; // E 키로 획득
    public KeyCode activationKey = KeyCode.Alpha3; // 3번으로 사용
    public TextMeshProUGUI slotText; // 슬롯 텍스트
    public string pickupTag = "Dynamite"; //태그
    private int dynamiteCount = 0; // 다이너마이트 개수
    public GameObject dynamitePrefab; // 다이너마이트 프리팹
    public float explodeDelay = 3f; // 폭발 지연 시간
    public float explosionRadius = 3f; // 폭발 반경
    
    public AudioClip pickupSound; // 아이템 획득 소리
    private AudioSource audioSource; // AudioSource 컴포넌트
    
    private void Start()
    {
        UpdateSlotText(); // 텍스트 업데이트

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource 컴포넌트가 없을 경우 추가
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
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

        if (Input.GetKeyDown(activationKey) && dynamiteCount > 0)
        {
            UseDynamite();
            dynamiteCount--;
            UpdateSlotText(); // 텍스트 업데이트
            

        }
    }
    private void PickUpItem(GameObject item)
    {
        //아이템을 획득하면 슬롯에 저장하고 개수를 증가시키고 오브젝트 삭제
        dynamiteCount++;
        UpdateSlotText();
        Destroy(item);
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }

    private void UseDynamite()
    {
        if (dynamiteCount > 0)
        {

            // 다이너마이트 오브젝트 생성
            GameObject dynamiteObject = Instantiate(dynamitePrefab, transform.position, Quaternion.identity);
            StartCoroutine(ExplodeAfterDelay(dynamiteObject)); // 폭발 처리를 지연시키는 코루틴 호출
        }
    }
    private IEnumerator ExplodeAfterDelay(GameObject dynamiteObject)
    {
        yield return new WaitForSeconds(explodeDelay); // 폭발 지연 시간만큼 대기

        // 폭발 처리
        if (dynamiteObject != null) // 다이너마이트 오브젝트 파괴 전에 null 체크
        {
            Destroy(dynamiteObject); // 다이너마이트 오브젝트 삭제
        }
    }
    private void UpdateSlotText()
    {
        slotText.text = dynamiteCount.ToString();
    }
}
    
