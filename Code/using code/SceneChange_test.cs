using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���� scene ��ȯ �ڵ� 
public class SceneChange_test : MonoBehaviour
{
    public float delayTime = 1f;
    public void ChangeSceneWithDelay()
    {
        // ���� �ð� �ڿ� scene ��ȯ�� �����մϴ�.
        Invoke("ChangeScenebtn", delayTime);
    }

    public void ChangeScenebtn()
    {
        SceneManager.LoadScene("GameScene");
    }
}
