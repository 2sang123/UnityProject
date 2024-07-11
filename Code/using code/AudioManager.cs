using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //�����̴� Ÿ���� �ν���

// ����â���� ������� �����ϴ� �ڵ�

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;
    private float currentVolume;
    
    private void Start()
    {
        currentVolume = PlayerPrefs.GetFloat("Volume", 1f); // ������ ����� ���� ���� �����ɴϴ�.
        volumeSlider.value = currentVolume; // �����̴��� ���� ���� ���� ������ �����մϴ�.
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged); // �����̴� ���� ����Ǹ� �ش� �޼��带 ȣ���մϴ�.
    }
   
    private void OnVolumeChanged(float volume)
    {
        currentVolume = volume; // ���� ���� ���� ������Ʈ�մϴ�.
        PlayerPrefs.SetFloat("Volume", currentVolume); // ���� ���� �����մϴ�.

        // ���� ���� ������ �߰��� �����մϴ�. (��: ���� �÷��̾ ���� ���� ����)
        AudioListener.volume = currentVolume;
    }
}
