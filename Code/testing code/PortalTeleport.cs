using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform secondPlaneTeleportLocation; // second plane �ڷ���Ʈ ��ġ�� ����Ű�� Transform
    private GameManager gameManager; // GameManager�� ���� ���� �߰�
    public AudioClip backgroundMusic;

    private void Start()
    {
        // ���� ������Ʈ���� GameManager ��ũ��Ʈ�� ã�� ���� ���
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = secondPlaneTeleportLocation.position;
            other.transform.rotation = secondPlaneTeleportLocation.rotation;

            if (gameManager != null)
            {
                gameManager.ChangeAudioSource(backgroundMusic);
            }
        }
    }
}
