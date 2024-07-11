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
        // TimeManager 스크립트를 찾아 연결합니다.
        timeManager = FindObjectOfType<TimeManager>();
        // 경과 시간을 가져와서 텍스트로 표시합니다.
        gameTime = timeManager.GetGameTime();

        if(gameTime != null)
        {
            timeText.text = "Time : " + gameTime.ToString("F2");
        }
        
    }

}
