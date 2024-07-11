using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ��ź ������ ���� �ڵ�
public class Dynamite : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.E; // E Ű�� ȹ��
    public KeyCode activationKey = KeyCode.Alpha3; // 3������ ���
    public TextMeshProUGUI slotText; // ���� �ؽ�Ʈ
    public string pickupTag = "Dynamite"; //�±�
    private int dynamiteCount = 0; // ���̳ʸ���Ʈ ����
    public GameObject dynamitePrefab; // ���̳ʸ���Ʈ ������
    public float explodeDelay = 3f; // ���� ���� �ð�
    public float explosionRadius = 3f; // ���� �ݰ�
    
    public AudioClip pickupSound; // ������ ȹ�� �Ҹ�
    private AudioSource audioSource; // AudioSource ������Ʈ
    
    private void Start()
    {
        UpdateSlotText(); // �ؽ�Ʈ ������Ʈ

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource ������Ʈ�� ���� ��� �߰�
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            Ray cast = Camera.main.ScreenPointToRay(Input.mousePosition); //���콺 ��ġ ����

            RaycastHit hit; //����ĳ��Ʈ

            if (Physics.Raycast(cast, out hit)) //���콺 ��ġ�� ����ĳ��Ʈ
            {

                if (hit.collider.gameObject.CompareTag(pickupTag))
                {
                    PickUpItem(hit.collider.gameObject); //�±� ������ ȹ��
                }
            }

        }

        if (Input.GetKeyDown(activationKey) && dynamiteCount > 0)
        {
            UseDynamite();
            dynamiteCount--;
            UpdateSlotText(); // �ؽ�Ʈ ������Ʈ
            

        }
    }
    private void PickUpItem(GameObject item)
    {
        //�������� ȹ���ϸ� ���Կ� �����ϰ� ������ ������Ű�� ������Ʈ ����
        dynamiteCount++;
        UpdateSlotText();
        Destroy(item);
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }

    private void UseDynamite()
    {
        if (dynamiteCount > 0)
        {

            // ���̳ʸ���Ʈ ������Ʈ ����
            GameObject dynamiteObject = Instantiate(dynamitePrefab, transform.position, Quaternion.identity);
            StartCoroutine(ExplodeAfterDelay(dynamiteObject)); // ���� ó���� ������Ű�� �ڷ�ƾ ȣ��
        }
    }
    private IEnumerator ExplodeAfterDelay(GameObject dynamiteObject)
    {
        yield return new WaitForSeconds(explodeDelay); // ���� ���� �ð���ŭ ���

        // ���� ó��
        if (dynamiteObject != null) // ���̳ʸ���Ʈ ������Ʈ �ı� ���� null üũ
        {
            Destroy(dynamiteObject); // ���̳ʸ���Ʈ ������Ʈ ����
        }
    }
    private void UpdateSlotText()
    {
        slotText.text = dynamiteCount.ToString();
    }
}
    
