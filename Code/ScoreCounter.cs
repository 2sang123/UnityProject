using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public int CountFPS = 30;
    public float Duration = 1f;
    public string NumberFormat = "N0";
    private int _value;
    private int plusPoint = 0; // 추가 포인트
    public RankImage rankImage;


    private void Update()
    {
        int coins = PlayerPrefs.GetInt("CoinCount");  //PlayerPrefs에 저장된 coin을 갖고 옴
        if (GameManager.isGameEnd == true)
        {
            TimeToScore();
        }     

        Value = coins + plusPoint;
        if (rankImage != null)
        {
            rankImage.UpdateScore(Value);
        }

    }

    void TimeToScore() // 저장된 시간 값을 포인트로 전환
    {
        float gameTime = PlayerPrefs.GetFloat("GameTime", 0f);

        if (gameTime >= 0f && gameTime <= 240f)
        {
            plusPoint = 50;
        }
        else if (gameTime > 240f && gameTime <= 600f)
        {
            plusPoint = 25;
        }
        else if (gameTime > 600f && gameTime <= 1000f)
        {
            plusPoint = 5;
        }
        else if (gameTime > 1000f)
        {
            plusPoint = 1;
        }
        else
        {
            plusPoint = 0;
        }
    }

    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            UpdateText(value);
            _value = value;
        }
    }
    private Coroutine CountingCoroutine;

    private void Awake()
    {
        if (Text == null)
        {
            Text = GetComponent<TextMeshProUGUI>();
        }
    }

    private void UpdateText(int newValue)
    {
        if (CountingCoroutine != null)
        {
            StopCoroutine(CountingCoroutine);
        }
        StartCoroutine(CountText(newValue));
    }

    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
        int previousValue = _value;
        int stepAmount;

        if (newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (CountFPS * Duration));
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (CountFPS * Duration));
        }
        if (previousValue < newValue)
        {
            while (previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                {
                    previousValue = newValue;
                }

                Text.SetText(previousValue.ToString(NumberFormat));

                yield return Wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount;
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                Text.SetText(previousValue.ToString(NumberFormat));

                yield return Wait;
            }
        }
    }
}
