using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ��� �ڵ�

public class ExitButton : MonoBehaviour
{
   public void ExitGame() 
    {
#if UNITY_EDITOR
        Debug.Log("Exit");
        UnityEditor.EditorApplication.isPlaying = false;
        
#else
        Application.Quit();
#endif
    }
}
