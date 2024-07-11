using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 버튼에 사운드 효과를 넣는 코드

public class Button_Sound : MonoBehaviour
{
    public AudioClip buttonSound;
    private Button button;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
        button = GetComponent<Button>(); // 버튼 컴포넌트를 찾습니다.

        
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 찾습니다.

        
        audioSource.clip = buttonSound; // AudioSource에 사운드 파일을 할당합니다.

        
        button.onClick.AddListener(PlaySound); // 버튼 클릭 시 사운드를 재생하는 이벤트 리스너를 추가합니다.
    }

    // Update is called once per frame
    private void PlaySound()
    {
        
        audioSource.PlayOneShot(buttonSound); // AudioSource를 통해 사운드를 재생합니다.
    }
}
