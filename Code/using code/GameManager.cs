using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static bool isGamePaused = false; // 게임이 정지 중인지
    public static bool isGameEnd = false;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ChangeAudioSource(AudioClip newClip)
    {
        audioSource.clip = newClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
