using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 플레이어와 닿으면 scene 전환하는 코드
public class SceneTrasition : MonoBehaviour
{
    public int endingSceneBuildIndex = 1; //EndigScene의 빌드 인덱스 '1'
    
    private void OnTriggerEnter(Collider other)
    {
        // 여기서 TimeManager.Instance를 통해 StopGameTime() 메서드 호출
        TimeManager.Instance.StopGameTime();

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(endingSceneBuildIndex);
        }
        GameManager.isGameEnd = true;
    }

}