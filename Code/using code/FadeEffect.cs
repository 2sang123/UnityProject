using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 엔딩씬 페이드 효과 코드

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 30f)]
    private float fadeTime;

    private Image image;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("gameover");
        image = GetComponent<Image>();
        StartCoroutine(Fade(1, 0));
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a *= Mathf.Lerp(start, end, percent);
            image.color = color;
            yield return null;

        }
    }
}