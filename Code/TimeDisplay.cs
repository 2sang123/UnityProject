using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private TimeManager timeManager;
    private float gameTime;

    void Start()
    {
        // TimeManager ��ũ��Ʈ�� ã�� �����մϴ�.
        timeManager = FindObjectOfType<TimeManager>();
        // ��� �ð��� �����ͼ� �ؽ�Ʈ�� ǥ���մϴ�.
        gameTime = timeManager.GetGameTime();

        if(gameTime != null)
        {
            timeText.text = "Time : " + gameTime.ToString("F2");
        }
        
    }

}
