using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ����ȭ������ scene ��ȯ�ϴ� ���
public class SceneChange : MonoBehaviour
{
    public void ChangeSceneBUT()
    {
        SceneManager.LoadScene("start_Scene");
    }
}
