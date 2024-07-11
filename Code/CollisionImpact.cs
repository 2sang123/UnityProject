using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionImpact : MonoBehaviour
{
    public AudioClip portalSound;  // ����� ���� ����
    public GameObject impactEffectPrefab;  // ����� ����Ʈ ����
    private AudioSource audioSource;  // AudioSource ����

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
            // ���� �÷��̾ ������ ���带 ���
            audioSource.PlayOneShot(portalSound);

            // �������� ����Ʈ ����
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