using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankImage : MonoBehaviour
{
    public Image imageComponent; // UI �̹��� ���

    public Sprite[] scoreSprites; // ���ھ ���� �̹��� �迭

    private int currentScore; // ���� ���ھ�

    private void Start()
    {
        currentScore = 0; // �⺻������ 0���� ����
    }

    // ���ھ� ������Ʈ �� ȣ��� �޼���
    public void UpdateScore(int newScore)
    {
        currentScore = newScore;
        UpdateScoreImage();
    }

    // ���ھ ���� �̹��� ������Ʈ �޼���
    private void UpdateScoreImage()
    {
        if (scoreSprites == null || scoreSprites.Length == 0 || imageComponent == null)
        {
            Debug.LogWarning("�̹����� ������Ʈ�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ���ھ� ������ ���� ������ �̹����� ����
        int spriteIndex = currentScore / 40; // ���ھ� 40������ �̹����� �ٲ�� ����

        // �迭 ���̸� ����� �ʵ��� ó��
        spriteIndex = Mathf.Clamp(spriteIndex, 0, scoreSprites.Length - 1);

        // �̹��� ����
        imageComponent.sprite = scoreSprites[spriteIndex];
    }
}
