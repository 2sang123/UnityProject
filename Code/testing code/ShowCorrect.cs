using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowCorrect : MonoBehaviour
{
    public Button button; //��ư
    public TMP_Text Correct; //�ؽ�Ʈ
    public GameObject door; // �� ����

    private void Start()
    {
        Correct.gameObject.SetActive(false); //ó���� �ؽ�Ʈ �Ⱥ��̰�
        button.onClick.AddListener(ActivateText); //��ư�� ������ �ؽ�Ʈ�� ���̰�
    }
    public void ActivateText()
    {
        Correct.gameObject.SetActive(true); //�ؽ�Ʈ ����
        door.SetActive(false);// �� �Ⱥ���
    }
}
