using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controllui : MonoBehaviour
{
    [SerializeField] private Button StartGameCbtn; // ���Ӿ����� �̵� ��ư
    [SerializeField] private Button OutGameCbtn; // ���� ������ ��ư
    
    [SerializeField] GameObject controllPase;
    [SerializeField] GameObject backPase;


    // private bool isControllUIActive = false;  UI�� Ȱ��ȭ ����

    // Start is called before the first frame update
    void Start()
    {
        controllPase.SetActive(false);
        StartGameCbtn.onClick.AddListener(() => ToControllUI());
        OutGameCbtn.onClick.AddListener(() => BackFromControllUI());
    }

    // Update is called once per frame


    public void ToControllUI() // ���۹�� �������� �̵�
    {
        controllPase.SetActive(true);
        backPase.SetActive(false);
    }

    public void BackFromControllUI() // ���۹�� UI���� ���ư���
    {
        controllPase.SetActive(false);
        backPase.SetActive(true);
    }
}
