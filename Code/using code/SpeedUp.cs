using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ���ǵ�� ������ ���� �ڵ�
public class SpeedUp : MonoBehaviour
{
    
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode activationKey = KeyCode.Alpha1;
    public TextMeshProUGUI slotText; // ���� �ؽ�Ʈ
    public string pickupTag = "SpeedUp"; // �±�

    public float speedBoostDuration = 10f; // ���ǵ� �� ���� �ð�
    public float speedMultiplier = 2f; // ���ǵ� �� ���

    public GameObject effectUi; // ���ǵ�� �� �����ִ� ui
    public GameObject effectVolume; // ���ǵ�� �� �����ִ� ȭ�� ȿ��
    public AudioClip pickupSound; // ������ ȹ�� �Ҹ�

    private int speedUpItemCount = 0; // ������ ����

    private bool isSpeedBoostActive = false; // ���ǵ� �� ������ Ȱ��ȭ ����

    private float originalMoveSpeed; // ���� �̵� �ӵ�
    private AudioSource audioSource; // AudioSource ������Ʈ

    private void Start()
    {
        originalMoveSpeed = GetComponent<Movement>().walkSpeed; // ���� �̵� �ӵ� ����
        UpdateSlotText(); //�ؽ�Ʈ ������Ʈ

        effectUi.SetActive(false);
        effectVolume.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource ������Ʈ�� ���� ��� �߰�
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        //if (Application.platform == RuntimePlatform.WindowsEditor)
        {   // ���� �÷����� Window ����������

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
            if (Input.GetKeyDown(activationKey) && speedUpItemCount > 0 && !isSpeedBoostActive) // ���, ���� ����
            {
                ActivateSpeedBoost();

                speedUpItemCount--;
                UpdateSlotText();
            }
        }
    }
    private void PickUpItem(GameObject item)
    {
        // ���ǵ� �� �������� ȹ���ϸ� ���Կ� �����ϰ� ������ ������Ű�� ������Ʈ ����
        speedUpItemCount++;
        UpdateSlotText();
        Destroy(item);
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }
    private void ActivateSpeedBoost()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        isSpeedBoostActive = true;

        Movement playerController = GetComponent<Movement>();
        float originalWalkSpeed = playerController.walkSpeed;
        playerController.SetSpeed(originalWalkSpeed * speedMultiplier); // �̵� �ӵ� ����
        effectUi.SetActive(true);
        effectVolume.SetActive(true);

        yield return new WaitForSeconds(speedBoostDuration);

        playerController.SetSpeed(originalWalkSpeed); // �̵� �ӵ� ���� ������ ����
        isSpeedBoostActive = false; ;
        effectUi.SetActive(false);
        effectVolume.SetActive(false);
    }

    private void UpdateSlotText()
    {
        slotText.text = speedUpItemCount.ToString();
    }

}