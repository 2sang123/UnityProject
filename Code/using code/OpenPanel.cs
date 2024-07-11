using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ����: �г��� ���� �ڵ�

public class OpenPanel : MonoBehaviour
{ 
    public GameObject panel;  //�г�
    public GameObject fadeUi; // �г� �ֺ� ��Ӱ�
    public GameObject fadeVolume; // �г� �ֺ� ��Ӱ�

    public Button closeButton; // �ݴ� ��ư

    private GameObject player;
    protected float playLookSensitivityNum; // �⺻ ���� �� ����

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        panel.SetActive(false);  //ó���� �Ⱥ���
        fadeUi.SetActive(false);
        fadeVolume.SetActive(false);

        closeButton.onClick.AddListener(() => ClosePanel());
    }


    

    private void OnMouseDown()
    {
       Vector3 dist = transform.position - player.transform.position;

        if(dist.magnitude < 5f)
        {
            panel.SetActive(true);  //������Ʈ Ŭ���ϸ� �г� ����
            fadeUi.SetActive(true);
            fadeVolume.SetActive(true);
            ShowAni(); // �ִϸ��̼� �޾ƿ�
            GameManager.isGamePaused = true;
        }
            
    }

    void ClosePanel()
    {
        GameManager.isGamePaused = false;

        panel.SetActive(false); //�г� ����     
        fadeUi.SetActive(false);
        fadeVolume.SetActive(false);
    }

    private void ShowAni() // �ִϸ��̼� �޴� script
    {
        // �ִϸ��̼� ������Ʈ ��������
        Animator animator = panel.GetComponent<Animator>();
        Animator QSVanimator = fadeVolume.GetComponent<Animator>();

        if (animator != null)
        {
            animator.Play("uiAni_test", 0, 0); // �ִϸ��̼� ���� �̸��� ��� �ð� ����
        }

        if (QSVanimator != null)
        {
            QSVanimator.Play("QuizScreenVolumeAnimati", 0, 0); // �ִϸ��̼� ���� �̸��� ��� �ð� ����
        }

    }

}
