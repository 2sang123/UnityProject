using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowCorrect : MonoBehaviour
{
    public Button button; //버튼
    public TMP_Text Correct; //텍스트
    public GameObject door; // 문 역할

    private void Start()
    {
        Correct.gameObject.SetActive(false); //처음엔 텍스트 안보이게
        button.onClick.AddListener(ActivateText); //버튼을 누르면 텍스트가 보이게
    }
    public void ActivateText()
    {
        Correct.gameObject.SetActive(true); //텍스트 보임
        door.SetActive(false);// 문 안보임
    }
}
