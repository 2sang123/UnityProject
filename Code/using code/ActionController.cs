using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾� �þ߿� ���õ� ��ɵ�

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; //�ν� ����
    private bool pickupActivated = false; //�������� �ֿ�� �ִ���
    private RaycastHit hitInfo; //����ĳ��Ʈ�� �ε��� ������Ʈ ������ ������ ����

    [SerializeField]
    private LayerMask layerMask;// �浹 üũ ���̾� ����ũ

    [SerializeField]
    private Text actionText;// ������ ������ ǥ���� UI �ؽ�Ʈ

    [SerializeField]
    private Inventory theInventory;// �κ��丮 Ŭ����
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TryAction(); // E Ű �Է¿� ���� �������� �ֿ� �� �ִ��� �˻��ϴ� �޼��� ȣ��
        CheckItem();// ����ĳ��Ʈ�� ����Ͽ� �����۰� �浹�� ��� UI �ؽ�Ʈ�� ������Ʈ�ϴ� �޼��� ȣ��
    
}

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem(); // ����ĳ��Ʈ�� ����Ͽ� �����۰� �浹�� ��� UI �ؽ�Ʈ�� ������Ʈ�ϴ� �޼��� ȣ��
            CanPickUp(); // �������� �ֿ� �� �ִ� �������� �˻��ϴ� �޼��� ȣ��
        }
    }
    private void CanPickUp()
    {
        if(pickupActivated) // �������� �ֿ� �� �ִ� ������ ���
        {
            if(hitInfo.transform!=null) // ����ĳ��Ʈ�� �ε��� ������Ʈ�� �ִ� ���
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "ȹ��"); // ����� �α� ���
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item); // �κ��丮�� ������ �߰�
                Destroy(hitInfo.transform.gameObject);// ������ ������Ʈ �ı�
                InfoDisappear(); // ������ ���� UI ��Ȱ��ȭ
                
            }
        }
    }
    private void CheckItem()  // ����ĳ��Ʈ�� ����Ͽ� �����۰� �浹�� ���
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item") // �浹�� ������Ʈ�� �±װ� "Item"�� ���
            {
                ItemInfoAppear();
            }
        }
        else
            InfoDisappear();
    }
    private void ItemInfoAppear()  // ItemInfoAppear() �Լ��� pickupActivated ������ true�� �����Ͽ� �������� �ֿ� �� �ִ� ���·� �����, actionText UI�� Ȱ��ȭ
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "EŰ�� ����" + "< " + hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " >" + " ȹ��";
        // ����, hitInfo ������ ����� �浹�� ������Ʈ���� ItemPickUp ������Ʈ�� ������ �ش� �������� �̸��� actionText UI�� ���

    }

    private void InfoDisappear() // InfoDisappear() �Լ��� pickupActivated ������ false�� �����Ͽ� �������� �ֿ� �� ���� ���·� �����, actionText UI�� ��Ȱ��ȭ
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

}