using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Inventory 코드를 사용시 슬롯 역할을 하는 코드

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Item item; //획득한 아이템
    public int itemCount; //획득한 아이템의 개수
    public Image itemImage; //아이템의 이미지
    
  

    
    [SerializeField]
    private Text text_Count; 
    [SerializeField]
    //필요한 컴포넌트

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha; 
        itemImage.color = color; 
    }

    public void AddItem(Item _item, int _count = 1) 
    {
        item = _item; //파라미터의 아이템
        itemCount = _count; //아이템의 카운트
        itemImage.sprite = item.itemImage; //아이템의 이미지
        text_Count.text = itemCount.ToString(); //갯수를 나타내는 이미지를 Tostring으로 문자열로 형변환
        SetColor(1);

    }

    public void SetSlotCount(int _count) //슬롯의 개수를 변경할수있는 함수
    {
        itemCount += _count; 
        text_Count.text = "X"+itemCount.ToString();

        if (itemCount <= 0)  //if문을 사용하여 ItemCount가 0이거나 작을때 ClearSlot함수를 호출
            ClearSlot();


    }

    private void ClearSlot() 
    {
        item = null; //아이템이 null상태 일때 
        itemCount = 0; //아이템 갯수 0
        itemImage.sprite = null; //아이템이미지도 null
        SetColor(0);

        text_Count.text = "0";


    }
    [SerializeField]
    private float speedup = 2f;
    public void OnPointerClick(PointerEventData eventData) //
    {
        if (eventData.button == PointerEventData.InputButton.Right) //인벤토리의 slot을 우클릭하였을때
        {
            if (item != null) //아이템이 null이 아니라면 
            {

                Debug.Log(item.itemName + " 을 사용했습니다."); //디버그창에 item사용을 알려줌

                SetSlotCount(-1); //슬롯의 text개수를 -1감소
                PlayerController controller = GameObject.FindObjectOfType<PlayerController>(); //controller.cs문을 참조한다 
                
                
                
                controller.walkSpeed += speedup; //contoroller의 이동속도를 +2만큼 증가 
                controller.runSpeed += speedup; // 달리기 속도를 +2만큼 증가
                controller.crouchSpeed += speedup; // 숙이기 속도를 +2만큼 증가
            }
        }
    }

}

            
              
            
        
        

    
        

    

    




