using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ž�� ������ ��� �ڵ�
public class TopView : MonoBehaviour
{
    public KeyCode pickupKey = KeyCode.E; //eŰ�� ȹ��
    public KeyCode activationKey = KeyCode.Alpha2; //2������ ���
    public TextMeshProUGUI slotText; // ���� �ؽ�Ʈ
    public string pickupTag = "TopView"; // ž�� ������Ʈ�� �±�
    public AudioClip pickupSound; // ������ ȹ�� �Ҹ�
    public float topViewDuration = 10f;  // ž�� ���� �ð�
    private int topViewItemCount = 0; //������ ����
    public bool isTopViewActive = false; // ž�� ������ Ȱ��ȭ ����

    public GameObject effectUi; // ž�� ������ �����ִ� ui

    private AudioSource audioSource; // AudioSource ������Ʈ
    private Quaternion originalCameraRotation; // ���� ī�޶� ȸ������ ������ ����


    private void Start()
    {
        originalCameraRotation = Camera.main.transform.rotation; //ī�޶�
        UpdateSlotText(); //�ؽ�Ʈ ������Ʈ

        effectUi.SetActive(false);

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
                    
                    Vector3 dist = transform.position - hit.point;
                    Debug.Log(dist.magnitude);
                    if (dist.magnitude < 5f)
                    {
                        PickUpItem(hit.collider.gameObject); //�±� ������ ȹ��
                    }

                }
            }

        }


        if (Input.GetKeyDown(activationKey) && topViewItemCount > 0 && !isTopViewActive) //���, ���� ����
        {
            ActivateTopView();

            topViewItemCount--;
            UpdateSlotText();
        }
    }

    public void PickUpItem(GameObject item)
    {
        // ž�� �������� ȹ���ϸ� ���Կ� �����ϰ� ������ �����ð� ������Ʈ ����
        topViewItemCount++;
        UpdateSlotText();
        Destroy(item);
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }

    }

    private void ActivateTopView() //�ڷ�ƾ
    {
        StartCoroutine(ActivateTopViewCoroutine());
    }

    private IEnumerator ActivateTopViewCoroutine() //������ ����
    {
        isTopViewActive = true;
        effectUi.SetActive(true);

        // ī�޶� ��ġ �� ȸ�� ����
        Vector3 originalCameraPosition = Camera.main.transform.localPosition;
        Quaternion originalCameraRotation = Camera.main.transform.localRotation;

        Vector3 topViewCameraPosition = new Vector3(0f, originalCameraPosition.y + 50f, 0f);
        Quaternion topViewCameraRotation = Quaternion.Euler(90f, 0f, 0f);

        // ī�޶� ���� �������� ����
        Camera.main.orthographic = true;

        // ī�޶� ��ġ �� ȸ�� ����
        Camera.main.transform.localPosition = topViewCameraPosition;
        Camera.main.transform.localRotation = topViewCameraRotation;

        yield return new WaitForSeconds(topViewDuration);

        // ī�޶� ���� �������� ����
        Camera.main.orthographic = false;

        // ī�޶� ���� ���·� ����
        Camera.main.transform.localPosition = originalCameraPosition;
        Camera.main.transform.localRotation = originalCameraRotation;

        effectUi.SetActive(false);
        isTopViewActive = false;
    }


    private void UpdateSlotText() //������ ���� = �ؽ�Ʈ ����
    {
        slotText.text = topViewItemCount.ToString();
    }

}