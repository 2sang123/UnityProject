using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ��ź ������ ���� �ڵ�
public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 40f; // ����ź�� ���� �� ����Ǵ� ��
    public GameObject grenadePrefab; // ������ ����ź ������
                                     

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // K Ű�� ������ ��
        {
            ThrowGrenade(); // ����ź�� ������ �Լ� ȣ��
        }
    }
    void ThrowGrenade()  // ����ź ����
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);  // ����ź�� Rigidbody ������Ʈ�� �����ͼ� ���� ������
        Rigidbody rb=grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce,ForceMode.VelocityChange);
    }
}
