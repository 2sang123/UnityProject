using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowWrong : MonoBehaviour
{
    public Button button; //버튼
    public TMP_Text Wrong; //텍스트
    private Life lifeEvent;
    private void Start()
    {
        Wrong.gameObject.SetActive(false); //처음엔 텍스트 안보이게
        button.onClick.AddListener(ActivateText); //버튼을 누르면 텍스트가 보이게
    }
    public void ActivateText()
    {
        lifeEvent.DecreaseLife(); // 틀린 
        Wrong.gameObject.SetActive(true); //텍스트 보임
    }
}
