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

        // �� ��ȯ �� �÷��� Ÿ���� PlayerPrefs�� ����
        PlayerPrefs.SetFloat("GameTime", gameTime);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        // �� ��ũ��Ʈ�� �ı����� �ʵ��� ����
        DontDestroyOnLoad(this.gameObject);
    }
}

