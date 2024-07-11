using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임 scene 전환 코드 
public class SceneChange_test : MonoBehaviour
{
    public float delayTime = 1f;
    public void ChangeSceneWithDelay()
    {
        // 일정 시간 뒤에 scene 전환을 실행합니다.
        Invoke("ChangeScenebtn", delayTime);
    }

    public void ChangeScenebtn()
    {
        SceneManager.LoadScene("GameScene");
    }
}
