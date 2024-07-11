using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controllui : MonoBehaviour
{
    [SerializeField] private Button StartGameCbtn; // 게임씬으로 이동 버튼
    [SerializeField] private Button OutGameCbtn; // 게임 나가기 버튼
    
    [SerializeField] GameObject controllPase;
    [SerializeField] GameObject backPase;


    // private bool isControllUIActive = false;  UI의 활성화 여부

    // Start is called before the first frame update
    void Start()
    {
        controllPase.SetActive(false);
        StartGameCbtn.onClick.AddListener(() => ToControllUI());
        OutGameCbtn.onClick.AddListener(() => BackFromControllUI());
    }

    // Update is called once per frame


    public void ToControllUI() // 조작방법 페이지로 이동
    {
        controllPase.SetActive(true);
        backPase.SetActive(false);
    }

    public void BackFromControllUI() // 조작방법 UI에서 돌아가기
    {
        controllPase.SetActive(false);
        backPase.SetActive(true);
    }
}
