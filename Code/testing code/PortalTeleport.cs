using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform secondPlaneTeleportLocation; // second plane 텔레포트 위치를 가리키는 Transform
    private GameManager gameManager; // GameManager에 대한 참조 추가
    public AudioClip backgroundMusic;

    private void Start()
    {
        // 게임 오브젝트에서 GameManager 스크립트를 찾아 참조 얻기
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
