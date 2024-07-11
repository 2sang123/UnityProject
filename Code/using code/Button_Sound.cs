using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��ư�� ���� ȿ���� �ִ� �ڵ�

public class Button_Sound : MonoBehaviour
{
    public AudioClip buttonSound;
    private Button button;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
        button = GetComponent<Button>(); // ��ư ������Ʈ�� ã���ϴ�.

        
        audioSource = GetComponent<AudioSource>(); // AudioSource ������Ʈ�� ã���ϴ�.

        
        audioSource.clip = buttonSound; // AudioSource�� ���� ������ �Ҵ��մϴ�.

        
        button.onClick.AddListener(PlaySound); // ��ư Ŭ�� �� ���带 ����ϴ� �̺�Ʈ �����ʸ� �߰��մϴ�.
    }

    // Update is called once per frame
    private void PlaySound()
    {
        
        audioSource.PlayOneShot(buttonSound); // AudioSource�� ���� ���带 ����մϴ�.
    }
}
