using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 시작화면으로 scene 전환하는 기능
public class SceneChange : MonoBehaviour
{
    public void ChangeSceneBUT()
    {
        SceneManager.LoadScene("start_Scene");
    }
}
