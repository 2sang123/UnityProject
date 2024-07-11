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
        //���� �÷��� ���� �� ��� �ð� ������Ʈ
        if (!IsGamePaused) // ������ �Ͻ����� ���� ���� ��� 
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

    public float GetPlayTime() //���� ������ �÷��� �ð� ���� �Լ�
    {
        return playTime;
    }
}
