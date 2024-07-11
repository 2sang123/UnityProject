using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour 
{
    public TextMeshProUGUI coinCountText;
    void Start()
    {
        string baseText = coinCountText.text;
        int coinCount = PlayerPrefs.GetInt("CoinCount");
        coinCountText.text = baseText + coinCount.ToString();
    }
}
