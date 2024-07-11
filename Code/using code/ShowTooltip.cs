using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


// �����ۿ� ���� ������ �����ִ� �ڵ�

public class ShowTooltip : MonoBehaviour
{
    public GameObject tooltipPrefab; // ���� ������
    private GameObject tooltipInstance; // ������ ���� �ν��Ͻ�
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �÷��̾ �±׷� ã��
    }


    private void OnMouseEnter() // ���콺�� ������Ʈ�� ����Ű��
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // UI�� ����Ű�� ������ �ƹ��͵� ���� �ʰ� ����
        }

        // �÷��̾�� ������ ���� �Ÿ� ���
        Vector3 dist = transform.position - player.transform.position;

        if (dist.magnitude < 5f) // �Ÿ��� 5f �̳��̸�
        {
            // ���� �������� �ν��Ͻ�ȭ�ϰ� ǥ��
            tooltipInstance = Instantiate(tooltipPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
    }

    private void OnMouseExit() // ���콺�� �ش� ������Ʈ�� ����� 
    {
        // ������ ���� �ν��Ͻ��� �ִٸ� �ı�
        if (tooltipInstance != null)
        {
            Destroy(tooltipInstance);
        }
    }

    private void OnDestroy()
    {
        // ������ �ı� �� ������ ���� �ν��Ͻ��� �ı�
        if (tooltipInstance != null)
        {
            Destroy(tooltipInstance);
        }

    }
}
