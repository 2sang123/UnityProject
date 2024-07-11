using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �÷��̾�� ������ scene ��ȯ�ϴ� �ڵ�
public class SceneTrasition : MonoBehaviour
{
    public int endingSceneBuildIndex = 1; //EndigScene�� ���� �ε��� '1'
    
    private void OnTriggerEnter(Collider other)
    {
        // ���⼭ TimeManager.Instance�� ���� StopGameTime() �޼��� ȣ��
        TimeManager.Instance.StopGameTime();

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(endingSceneBuildIndex);
        }
        GameManager.isGameEnd = true;
    }

}