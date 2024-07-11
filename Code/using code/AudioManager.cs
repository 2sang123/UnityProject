using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //슬라이더 타입을 인식함

// 설정창에서 오디오를 조절하는 코드

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;
    private float currentVolume;
    
    private void Start()
    {
        currentVolume = PlayerPrefs.GetFloat("Volume", 1f); // 이전에 저장된 볼륨 값을 가져옵니다.
        volumeSlider.value = currentVolume; // 슬라이더의 값을 현재 볼륨 값으로 설정합니다.
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged); // 슬라이더 값이 변경되면 해당 메서드를 호출합니다.
    }
   
    private void OnVolumeChanged(float volume)
    {
        currentVolume = volume; // 현재 볼륨 값을 업데이트합니다.
        PlayerPrefs.SetFloat("Volume", currentVolume); // 볼륨 값을 저장합니다.

        // 볼륨 조절 로직을 추가로 구현합니다. (예: 뮤직 플레이어에 볼륨 값을 적용)
        AudioListener.volume = currentVolume;
    }
}
