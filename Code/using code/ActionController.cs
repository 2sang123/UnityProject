using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 플레이어 시야와 관련된 기능들

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //인식 범위
    private bool pickupActivated = false; //아이템을 주울수 있는지
    private RaycastHit hitInfo; //레이캐스트로 부딪힌 오브젝트 정보를 저장할 변수

    [SerializeField]
    private LayerMask layerMask;// 충돌 체크 레이어 마스크

    [SerializeField]
    private Text actionText;// 아이템 정보를 표시할 UI 텍스트

    [SerializeField]
    private Inventory theInventory;// 인벤토리 클래스
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TryAction(); // E 키 입력에 따라 아이템을 주울 수 있는지 검사하는 메서드 호출
        CheckItem();// 레이캐스트를 사용하여 아이템과 충돌한 경우 UI 텍스트를 업데이트하는 메서드 호출
    
}

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem(); // 레이캐스트를 사용하여 아이템과 충돌한 경우 UI 텍스트를 업데이트하는 메서드 호출
            CanPickUp(); // 아이템을 주울 수 있는 상태인지 검사하는 메서드 호출
        }
    }
    private void CanPickUp()
    {
        if(pickupActivated) // 아이템을 주울 수 있는 상태인 경우
        {
            if(hitInfo.transform!=null) // 레이캐스트에 부딪힌 오브젝트가 있는 경우
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득"); // 디버그 로그 출력
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item); // 인벤토리에 아이템 추가
                Destroy(hitInfo.transform.gameObject);// 아이템 오브젝트 파괴
                InfoDisappear(); // 아이템 정보 UI 비활성화
                
            }
        }
    }
    private void CheckItem()  // 레이캐스트를 사용하여 아이템과 충돌한 경우
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item") // 충돌한 오브젝트의 태그가 "Item"인 경우
            {
                ItemInfoAppear();
            }
        }
        else
            InfoDisappear();
    }
    private void ItemInfoAppear()  // ItemInfoAppear() 함수는 pickupActivated 변수를 true로 설정하여 아이템을 주울 수 있는 상태로 만들고, actionText UI를 활성화
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "E키를 눌러" + "< " + hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " >" + " 획득";
        // 이후, hitInfo 변수에 저장된 충돌한 오브젝트에서 ItemPickUp 컴포넌트를 가져와 해당 아이템의 이름을 actionText UI에 출력

    }

    private void InfoDisappear() // InfoDisappear() 함수는 pickupActivated 변수를 false로 설정하여 아이템을 주울 수 없는 상태로 만들고, actionText UI를 비활성화
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

}