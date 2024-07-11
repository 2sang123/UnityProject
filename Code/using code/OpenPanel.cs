using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 설명: 패널을 띄우는 코드

public class OpenPanel : MonoBehaviour
{ 
    public GameObject panel;  //패널
    public GameObject fadeUi; // 패널 주변 어둡게
    public GameObject fadeVolume; // 패널 주변 어둡게

    public Button closeButton; // 닫는 버튼

    private GameObject player;
    protected float playLookSensitivityNum; // 기본 감도 값 저장

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        panel.SetActive(false);  //처음엔 안보임
        fadeUi.SetActive(false);
        fadeVolume.SetActive(false);

        closeButton.onClick.AddListener(() => ClosePanel());
    }


    

    private void OnMouseDown()
    {
       Vector3 dist = transform.position - player.transform.position;

        if(dist.magnitude < 5f)
        {
            panel.SetActive(true);  //오브젝트 클릭하면 패널 열림
            fadeUi.SetActive(true);
            fadeVolume.SetActive(true);
            ShowAni(); // 애니메이션 받아옴
            GameManager.isGamePaused = true;
        }
            
    }

    void ClosePanel()
    {
        GameManager.isGamePaused = false;

        panel.SetActive(false); //패널 닫힘     
        fadeUi.SetActive(false);
        fadeVolume.SetActive(false);
    }

    private void ShowAni() // 애니메이션 받는 script
    {
        // 애니메이션 컴포넌트 가져오기
        Animator animator = panel.GetComponent<Animator>();
        Animator QSVanimator = fadeVolume.GetComponent<Animator>();

        if (animator != null)
        {
            animator.Play("uiAni_test", 0, 0); // 애니메이션 상태 이름과 재생 시간 설정
        }

        if (QSVanimator != null)
        {
            QSVanimator.Play("QuizScreenVolumeAnimati", 0, 0); // 애니메이션 상태 이름과 재생 시간 설정
        }

    }

}
