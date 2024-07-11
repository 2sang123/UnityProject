using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 설명: 이 코드는 인벤토리 UI 창을 띄우는 형식에 코드 입니다.    
public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated=false;

    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    private Slot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots= go_SlotsParent.GetComponentsInChildren<Slot>(); //슬롯의 부모개체를 선언해줌
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory(); //TryOpenInventory함수를 호출
    }
    private void TryOpenInventory() //TryOpenInventory
    {
        if (Input.GetKeyDown(KeyCode.I)) //키보드에서 I를 눌렀을때 
        {
            inventoryActivated = !inventoryActivated; 

            if (inventoryActivated) //Inventory가 활성화 되었을때 OPenInventory함수를 호출
                OpenInventory();
            else
                CloseInventory(); //아니라면 CloseInventory함수를 호출
        }
    }
    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true); // OPenInventory함수는 인벤토리 창을 캔버스에 보여줌
        Time.timeScale = 0f; // 게임 일시 정지

        PlayerController controller = GameObject.FindObjectOfType<PlayerController>(); // 플레이어 컨트롤 코드를 받아옴
        controller.lookSensitivity = 0f; // 카메라 정지
    }

private void CloseInventory()
    {
        go_InventoryBase.SetActive(false); // CloseInventory함수는 인벤토리 창을 캔버스에서 사라지게함
        Time.timeScale = 1f; // 게임 재개
        PlayerController controller = GameObject.FindObjectOfType<PlayerController>(); // 플레이어 컨트롤 코드를 받아옴
        controller.lookSensitivity = 1f; // 카메라 이동을 다시 작동
    }
    public void AcquireItem(Item _item, int _count=1) // 아이템을 획득하는 함수
    {
        for (int i = 0; i < slots.Length; i++) // 슬롯 배열을 돌면서
        {
            if(slots[i].item!=null) // 해당 슬롯이 이미 아이템이 있는 슬롯이면
            {
                if (slots[i].item.itemName == _item.itemName) // 그 아이템의 이름이 획득한 아이템의 이름과 같다면
                
                    {
                    slots[i].SetSlotCount(_count);  // 해당 슬롯의 아이템 개수를 늘려줌
                    return; // 함수를 종료함

                }
            
            }
        }
        for(int i=0; i < slots.Length; i++) // 슬롯 배열을 다시 돌면서
        {
            if(slots[i].item == null) // 해당 슬롯에 아이템이 없다면
            {
                slots[i].AddItem(_item, _count); // 해당 슬롯에 아이템을 추가함
                return; // 함수를 종료함
            }
        }
    }
}
