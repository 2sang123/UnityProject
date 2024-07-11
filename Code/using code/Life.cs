using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ����: �� �ڵ� ����� UI �� ����� ����ϴ� �ڵ� �Դϴ�.

public class Life : MonoBehaviour
{
    public Slider lifeSlider; // �����̴�

    private float recoveryTimer = 0f;
    public float recoveryInterval = 1f;

    public Image bloodScreen;
    private AudioSource audioSource;

    public AudioClip spearImpactSound; // Declare spear impact sound variable
    public AudioClip arrowImpactSound; // Declare arrow impact sound variable
    public AudioClip flameImpactSound; // Declare flame impact sound variable
    public AudioClip recoverySound;

    private bool isSoundPlaying = false;


    public float decrease = 10f; // ���� ������ ��ġ

    private void Start()
    {
        lifeSlider = GameObject.FindAnyObjectByType<Slider>();

        UpdateSlider(100); // ���� ���� �� ����� 100���� �ʱ�ȭ

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (lifeSlider.value <= 0) // ������� 0 ���ϸ� ���� ����
        {
            GameOver();
        }
    }

    public void DecreaseLife() // ����� ����
    {      
        Debug.Log("ü�� ����");

        if (bloodScreen != null) // �ǰ� �̹���
        {
            bloodScreen.gameObject.SetActive(true);
            StartCoroutine(HideBloodScreen());
        }

        UpdateSlider(lifeSlider.value - decrease); // lifeSlider.value�� ���ҵ� ���� ����
    }

    public void RecoveryLife(float amount)
    {
        UpdateSlider(lifeSlider.value + 1); // Recover health, maintain maximum of 30
        if(!isSoundPlaying)
        {
            if (audioSource != null && recoverySound != null)
            {
                audioSource.PlayOneShot(recoverySound);
                StartCoroutine(WaitForRecoverySound());
                isSoundPlaying = true;
            }
        }
        
    }

    IEnumerator HideBloodScreen()
    {
        Color originalColor = bloodScreen.color; // ���� ���� ����
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // ������ ���� ����

        yield return new WaitForSeconds(0.5f); // ���ϴ� �ð���ŭ ���

        // ����ȭ �ִϸ��̼� (1�� ����)
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            bloodScreen.color = Color.Lerp(originalColor, transparentColor, elapsedTime);
            elapsedTime += Time.deltaTime / 0.3f; // 1�� ���� ����
            yield return null;
        }

        bloodScreen.gameObject.SetActive(false); // �̹��� ��Ȱ��ȭ
        bloodScreen.color = originalColor; // ���� �������� ����
    }


    /* public void RecoverLife(float amount)
     {
         if (lifeSlider.value < 100f) // Check if the player is not already at full health
         {
             UpdateSlider(Mathf.Min(lifeSlider.value + amount, 100f)); // Recover health, maintain maximum of 30
         }
     }*/

    private void GameOver()
    {       
            Debug.Log("Game Over");
            TimeManager.Instance.StopGameTime();
            SceneManager.LoadScene("GameOverScene"); // ���� ���� ������ ��ȯ 
    }
    private void UpdateSlider(float newValue)
    {
        // �����̴��� Value ���� ����
        if(lifeSlider != null)
        {
            lifeSlider.value = newValue;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // ��ü���� �浹 ó�� ���� �߰�
        if (lifeSlider != null)
        {
            if (other.CompareTag("Spear")) // spear �±׿� �浹�� ���
            {    
                decrease = 5f;
                Debug.Log(decrease + "Damage"); // ����� �޽��� �߰�
                DecreaseLife(); // ����� ����
                DecreaseLifeWithSound("SpearHit"); // ����� ���� �� spear �ǰ��� ���
            }
            else if (other.CompareTag("Arrow")) // arrow �±׿� �浹�� ���
            {
                decrease = 15f;
                Debug.Log(decrease + "Damage"); // ����� �޽��� �߰�
                DecreaseLife(); // ����� ����
                DecreaseLifeWithSound("ArrowHit"); // ����� ���� �� arrow �ǰ��� ���

            }
        }
    }

    private void DecreaseLifeWithSound(string collisionType)
    {
        Debug.Log("ü�� ����");
        UpdateSlider(lifeSlider.value - decrease); // lifeSlider.value�� ���ҵ� ���� ����

        AudioClip impactSound = null;

        // Choose the impact sound based on the collision type
        switch (collisionType)
        {
            case "SpearHit":
                impactSound = spearImpactSound;
                break;
            case "ArrowHit":
                impactSound = arrowImpactSound;
                break;
                // Add more cases for other collision types if needed
        }

        if (audioSource != null && impactSound != null)
        {
            audioSource.PlayOneShot(impactSound);
        }
    }

    private IEnumerator WaitForRecoverySound()
    {
        yield return new WaitForSeconds(recoverySound.length);
        isSoundPlaying = false;
    }

    private IEnumerator WaitForFlameSound()
    {
        yield return new WaitForSeconds(flameImpactSound.length);
        isSoundPlaying = false;
    }

    private void OnTriggerStay(Collider other)
    {

        if (lifeSlider != null)
        {
            if (other.CompareTag("Recovery"))
            {
                RecoveryLife(1f); // �ʴ� 10 ȸ��
            }
            else if (other.CompareTag("Flame"))
            {
                decrease = 1f;
                DecreaseLife();
                // Play flame impact sound
                if (!isSoundPlaying)
                {
                    if (audioSource != null && flameImpactSound != null)
                    {
                        audioSource.PlayOneShot(flameImpactSound);
                        StartCoroutine(WaitForFlameSound());
                        isSoundPlaying = true;
                    }
                }
               
            }
        } 
    }

}
