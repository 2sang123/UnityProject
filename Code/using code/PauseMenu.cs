using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Esc 키를 누르면 설정 UI를 띄우는 코드
// 플레이어 정지 기능은 여기서 전부 다루기 어려워서 MoveMent 코드에 옮겨 놨음

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button startSceneB; // 시작화면으로 이동 버튼
    [SerializeField]
    private Button controllUIB; // 조작방법 UI를 띄우는 버튼
    [SerializeField]
    private Button controllUIBackB; // 조작방법 UI에서 돌아가는 버튼
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
    void PauseGame() // 게임 정지 및
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

    public void TofirstUI() // 처음 페이지로 돌아감
    {
        firstPase.SetActive(true);
        controllPase.SetActive(false);
    }

    public void ToControllUI() // 조작방법 페이지로 이동
    {
        Debug.Log("b1");
        firstPase.SetActive(false);
        controllPase.SetActive(true);
    }
}
