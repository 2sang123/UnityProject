using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ź ������ ������ �޴� �ڵ�

public class Destuctibes : MonoBehaviour
{
    public GameObject destroyedVersion;  // �ı��� ������ ���� ������Ʈ�� ������ ����


    public void Destroy() // �ı��� ������ ���� ������Ʈ�� �����ϰ� ���� ���� ������Ʈ�� �ı���
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
