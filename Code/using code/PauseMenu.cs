using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Esc Ű�� ������ ���� UI�� ���� �ڵ�
// �÷��̾� ���� ����� ���⼭ ���� �ٷ�� ������� MoveMent �ڵ忡 �Ű� ����

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button startSceneB; // ����ȭ������ �̵� ��ư
    [SerializeField]
    private Button controllUIB; // ���۹�� UI�� ���� ��ư
    [SerializeField]
    private Button controllUIBackB; // ���۹�� UI���� ���ư��� ��ư
    [SerializeField] GameObject pauseMenu; //
    [SerializeField] GameObject controllPase;
    [SerializeField] GameObject firstPase;

    private void Start()
    {
        startSceneB.onClick.AddListener(GoToStartScene);
        controllUIB.onClick.AddListener(() => ToControllUI());
        controllUIBackB.onClick.AddListener(() => TofirstUI());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.isGamePaused==true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        

    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        GameManager.isGamePaused = false;
        
    }
    void PauseGame() // ���� ���� ��
    {
        pauseMenu.SetActive(true);
        firstPase.SetActive(true);
        controllPase.SetActive(false);
        GameManager.isGamePaused = true;

    }

    private void GoToStartScene() 
    {
        SceneManager.LoadScene("start_Scene");
        //SceneManager.LoadScene(1);
        //Debug.Log("Test");
    }

    public void TofirstUI() // ó�� �������� ���ư�
    {
        firstPase.SetActive(true);
        controllPase.SetActive(false);
    }

    public void ToControllUI() // ���۹�� �������� �̵�
    {
        Debug.Log("b1");
        firstPase.SetActive(false);
        controllPase.SetActive(true);
    }
}
