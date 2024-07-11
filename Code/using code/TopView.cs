using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 탑뷰 아이템 기능 코드
public class TopView : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.E; //e키로 획득
    public KeyCode activationKey = KeyCode.Alpha2; //2번으로 사용
    public TextMeshProUGUI slotText; // 슬롯 텍스트
    public string pickupTag = "TopView"; // 탑뷰 오브젝트의 태그
    public AudioClip pickupSound; // 아이템 획득 소리
    public float topViewDuration = 10f;  // 탑뷰 지속 시간
    private int topViewItemCount = 0; //아이템 개수
    public bool isTopViewActive = false; // 탑뷰 아이템 활성화 여부

    public GameObject effectUi; // 탑뷰 진행중 보여주는 ui

    private AudioSource audioSource; // AudioSource 컴포넌트
    private Quaternion originalCameraRotation; // 원래 카메라 회전값을 저장할 변수


    private void Start()
    {
        originalCameraRotation = Camera.main.transform.rotation; //카메라
        UpdateSlotText(); //텍스트 업데이트

        effectUi.SetActive(false);

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
                    
                    Vector3 dist = transform.position - hit.point;
                    Debug.Log(dist.magnitude);
                    if (dist.magnitude < 5f)
                    {
                        PickUpItem(hit.collider.gameObject); //태그 아이템 획득
                    }

                }
            }

        }


        if (Input.GetKeyDown(activationKey) && topViewItemCount > 0 && !isTopViewActive) //사용, 개수 감소
        {
            ActivateTopView();

            topViewItemCount--;
            UpdateSlotText();
        }
    }

    public void PickUpItem(GameObject item)
    {
        // 탑뷰 아이템을 획득하면 슬롯에 저장하고 개수를 증가시고 오브젝트 삭제
        topViewItemCount++;
        UpdateSlotText();
        Destroy(item);
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }

    }

    private void ActivateTopView() //코루틴
    {
        StartCoroutine(ActivateTopViewCoroutine());
    }

    private IEnumerator ActivateTopViewCoroutine() //아이템 구현
    {
        isTopViewActive = true;
        effectUi.SetActive(true);

        // 카메라 위치 및 회전 조정
        Vector3 originalCameraPosition = Camera.main.transform.localPosition;
        Quaternion originalCameraRotation = Camera.main.transform.localRotation;

        Vector3 topViewCameraPosition = new Vector3(0f, originalCameraPosition.y + 50f, 0f);
        Quaternion topViewCameraRotation = Quaternion.Euler(90f, 0f, 0f);

        // 카메라를 직교 투영으로 변경
        Camera.main.orthographic = true;

        // 카메라 위치 및 회전 조정
        Camera.main.transform.localPosition = topViewCameraPosition;
        Camera.main.transform.localRotation = topViewCameraRotation;

        yield return new WaitForSeconds(topViewDuration);

        // 카메라를 원근 투영으로 변경
        Camera.main.orthographic = false;

        // 카메라 원래 상태로 복구
        Camera.main.transform.localPosition = originalCameraPosition;
        Camera.main.transform.localRotation = originalCameraRotation;

        effectUi.SetActive(false);
        isTopViewActive = false;
    }


    private void UpdateSlotText() //아이템 개수 = 텍스트 숫자
    {
        slotText.text = topViewItemCount.ToString();
    }

}