using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Inventory �ڵ带 ���� ���� ������ �ϴ� �ڵ�

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Item item; //ȹ���� ������
    public int itemCount; //ȹ���� �������� ����
    public Image itemImage; //�������� �̹���
    
  

    
    [SerializeField]
    private Text text_Count; 
    [SerializeField]
    //�ʿ��� ������Ʈ

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha; 
        itemImage.color = color; 
    }

    public void AddItem(Item _item, int _count = 1) 
    {
        item = _item; //�Ķ������ ������
        itemCount = _count; //�������� ī��Ʈ
        itemImage.sprite = item.itemImage; //�������� �̹���
        text_Count.text = itemCount.ToString(); //������ ��Ÿ���� �̹����� Tostring���� ���ڿ��� ����ȯ
        SetColor(1);

    }

    public void SetSlotCount(int _count) //������ ������ �����Ҽ��ִ� �Լ�
    {
        itemCount += _count; 
        text_Count.text = "X"+itemCount.ToString();

        if (itemCount <= 0)  //if���� ����Ͽ� ItemCount�� 0�̰ų� ������ ClearSlot�Լ��� ȣ��
            ClearSlot();


    }

    private void ClearSlot() 
    {
        item = null; //�������� null���� �϶� 
        itemCount = 0; //������ ���� 0
        itemImage.sprite = null; //�������̹����� null
        SetColor(0);

        text_Count.text = "0";


    }
    [SerializeField]
    private float speedup = 2f;
    public void OnPointerClick(PointerEventData eventData) //
    {
        if (eventData.button == PointerEventData.InputButton.Right) //�κ��丮�� slot�� ��Ŭ���Ͽ�����
        {
            if (item != null) //�������� null�� �ƴ϶�� 
            {

                Debug.Log(item.itemName + " �� ����߽��ϴ�."); //�����â�� item����� �˷���

                SetSlotCount(-1); //������ text������ -1����
                PlayerController controller = GameObject.FindObjectOfType<PlayerController>(); //controller.cs���� �����Ѵ� 
                
                
                
                controller.walkSpeed += speedup; //contoroller�� �̵��ӵ��� +2��ŭ ���� 
                controller.runSpeed += speedup; // �޸��� �ӵ��� +2��ŭ ����
                controller.crouchSpeed += speedup; // ���̱� �ӵ��� +2��ŭ ����
            }
        }
    }

}

            
              
            
        
        

    
        

    

    




