using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTracking : MonoBehaviour
{
    private float startTime;
    private float playTime;
    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        //게임 플레이 중일 때 결과 시간 업데이트
        if (!IsGamePaused) // 게임이 일시정지 되지 않은 경우 
        {
            playTime = Time.time - startTime;
        }
    }


    private bool IsGamePaused
    {
        get
        {
            return Time.timeScale == 0f;
        }
    }

    public float GetPlayTime() //엔딩 씬으로 플레이 시간 전달 함수
    {
        return playTime;
    }
}
