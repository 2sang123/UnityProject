using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionImpact : MonoBehaviour
{
    public AudioClip portalSound;  // 사용할 사운드 변수
    public GameObject impactEffectPrefab;  // 사용할 임팩트 변수
    private AudioSource audioSource;  // AudioSource 변수

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (impactEffectPrefab != null)
        {

            Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            Debug.Log("Impact effect instantiated!");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 벽에 플레이어가 닿으면 사운드를 재생
            audioSource.PlayOneShot(portalSound);

            // 물리적인 임팩트 적용
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 impactDirection = transform.position - other.transform.position;
                impactDirection = impactDirection.normalized;
                rb.AddForce(impactDirection * 10f, ForceMode.Impulse);
            }

        }
    }
}