using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 끄는 기능 코드

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
