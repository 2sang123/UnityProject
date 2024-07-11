using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float gameTime = 0.0f;
    private bool isGameRunning = true;


    private static TimeManager _instance;

    public static TimeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TimeManager>();
            }
            return _instance;
        }
    }

    void Update()
    {
        if (isGameRunning)
        {
            gameTime += Time.deltaTime;
        }
    }

    public float GetGameTime()
    {
        return gameTime;
    }

    public void StopGameTime()
    {
        isGameRunning = false;

        // 씬 전환 시 플레이 타임을 PlayerPrefs에 저장
        PlayerPrefs.SetFloat("GameTime", gameTime);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        // 이 스크립트가 파괴되지 않도록 설정
        DontDestroyOnLoad(this.gameObject);
    }
}

