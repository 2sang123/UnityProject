using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ����: �� �ڵ�� �κ��丮 UI â�� ���� ���Ŀ� �ڵ� �Դϴ�.    
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
        slots= go_SlotsParent.GetComponentsInChildren<Slot>(); //������ �θ�ü�� ��������
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory(); //TryOpenInventory�Լ��� ȣ��
    }
    private void TryOpenInventory() //TryOpenInventory
    {
        if (Input.GetKeyDown(KeyCode.I)) //Ű���忡�� I�� �������� 
        {
            inventoryActivated = !inventoryActivated; 

            if (inventoryActivated) //Inventory�� Ȱ��ȭ �Ǿ����� OPenInventory�Լ��� ȣ��
                OpenInventory();
            else
                CloseInventory(); //�ƴ϶�� CloseInventory�Լ��� ȣ��
        }
    }
    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true); // OPenInventory�Լ��� �κ��丮 â�� ĵ������ ������
        Time.timeScale = 0f; // ���� �Ͻ� ����

        PlayerController controller = GameObject.FindObjectOfType<PlayerController>(); // �÷��̾� ��Ʈ�� �ڵ带 �޾ƿ�
        controller.lookSensitivity = 0f; // ī�޶� ����
    }

private void CloseInventory()
    {
        go_InventoryBase.SetActive(false); // CloseInventory�Լ��� �κ��丮 â�� ĵ�������� ���������
        Time.timeScale = 1f; // ���� �簳
        PlayerController controller = GameObject.FindObjectOfType<PlayerController>(); // �÷��̾� ��Ʈ�� �ڵ带 �޾ƿ�
        controller.lookSensitivity = 1f; // ī�޶� �̵��� �ٽ� �۵�
    }
    public void AcquireItem(Item _item, int _count=1) // �������� ȹ���ϴ� �Լ�
    {
        for (int i = 0; i < slots.Length; i++) // ���� �迭�� ���鼭
        {
            if(slots[i].item!=null) // �ش� ������ �̹� �������� �ִ� �����̸�
            {
                if (slots[i].item.itemName == _item.itemName) // �� �������� �̸��� ȹ���� �������� �̸��� ���ٸ�
                
                    {
                    slots[i].SetSlotCount(_count);  // �ش� ������ ������ ������ �÷���
                    return; // �Լ��� ������

                }
            
            }
        }
        for(int i=0; i < slots.Length; i++) // ���� �迭�� �ٽ� ���鼭
        {
            if(slots[i].item == null) // �ش� ���Կ� �������� ���ٸ�
            {
                slots[i].AddItem(_item, _count); // �ش� ���Կ� �������� �߰���
                return; // �Լ��� ������
            }
        }
    }
}
