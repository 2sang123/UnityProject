using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ź ��� ���� �ڵ�

public class Grenade : MonoBehaviour
{
    public float delay = 3f; //���߱��� �������� �ð�
    public float radius = 5f; //���߹���
    public float force = 700f; //���� ��
    public GameObject explosionEffect; //���� ȿ�� ������
    float countdown; //���� �����̽ð�
    bool hasExploded = false; //�̹� �����ߴ��� ����
    void Start()
    {
        countdown = delay; //������ �ð����� �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime; //����� �ð��� ī��Ʈ�ٿ�� ���ش�
        if (countdown <= 0f && !hasExploded) 
        {
            Explode(); //����ó���Լ�ȣ��
            hasExploded = true; //�̹� ������ ���·� ����
        }
    }

    void Explode()
    {
        //���� ȿ�� ����
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); //���� ���� ���� ��� colider�� ������

        foreach (Collider nearbyobject in colliders) //���� ���� ���� ��� collider�� ������
        {
            Rigidbody rb = nearbyobject.GetComponent<Rigidbody>(); // // ���߿� ���� �ı��Ǵ� ������Ʈ�� ��� Destroy() �޼ҵ� ȣ��
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

             Destuctibes dest=nearbyobject.GetComponent<Destuctibes>(); //�ڱ� �ڽŵ� ���߿� ���� �ı���
            if(dest != null)
            {
                dest.Destroy();
            }

            Destroy(gameObject);

        }
    }
}