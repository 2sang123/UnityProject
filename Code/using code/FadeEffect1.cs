using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ȯ�� ��ȯ�� ���̵� ȿ���� �ִ� �ڵ�
public class FadeEffect1 : MonoBehaviour
{
    private Image image; // image�� �޾ƿ�

    public void Awake()
    {
        image = GetComponent<Image>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Color color = image.color; // �̹����� ���� �޾ƿ�

        if(color.a > 0)
        {
            color.a -= Time.deltaTime; // �ð��� ������ �����̹��� ���� ����
        }

        image.color = color;
    }
}
