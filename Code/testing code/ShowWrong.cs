using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowWrong : MonoBehaviour
{
    public Button button; //��ư
    public TMP_Text Wrong; //�ؽ�Ʈ
    private Life lifeEvent;
    private void Start()
    {
        Wrong.gameObject.SetActive(false); //ó���� �ؽ�Ʈ �Ⱥ��̰�
        button.onClick.AddListener(ActivateText); //��ư�� ������ �ؽ�Ʈ�� ���̰�
    }
    public void ActivateText()
    {
        lifeEvent.DecreaseLife(); // Ʋ�� 
        Wrong.gameObject.SetActive(true); //�ؽ�Ʈ ����
    }
}
