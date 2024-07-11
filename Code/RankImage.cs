using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankImage : MonoBehaviour
{
    public Image imageComponent; // UI 이미지 요소

    public Sprite[] scoreSprites; // 스코어에 따른 이미지 배열

    private int currentScore; // 현재 스코어

    private void Start()
    {
        currentScore = 0; // 기본적으로 0으로 설정
    }

    // 스코어 업데이트 시 호출될 메서드
    public void UpdateScore(int newScore)
    {
        currentScore = newScore;
        UpdateScoreImage();
    }

    // 스코어에 따른 이미지 업데이트 메서드
    private void UpdateScoreImage()
    {
        if (scoreSprites == null || scoreSprites.Length == 0 || imageComponent == null)
        {
            Debug.LogWarning("이미지나 컴포넌트가 설정되지 않았습니다.");
            return;
        }

        // 스코어 범위에 따라 적절한 이미지를 설정
        int spriteIndex = currentScore / 40; // 스코어 40점마다 이미지가 바뀌도록 설정

        // 배열 길이를 벗어나지 않도록 처리
        spriteIndex = Mathf.Clamp(spriteIndex, 0, scoreSprites.Length - 1);

        // 이미지 변경
        imageComponent.sprite = scoreSprites[spriteIndex];
    }
}
