using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 확면 전환시 페이드 효과를 주는 코드
public class FadeEffect1 : MonoBehaviour
{
    private Image image; // image를 받아옴

    public void Awake()
    {
        image = GetComponent<Image>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Color color = image.color; // 이미지에 색을 받아옴

        if(color.a > 0)
        {
            color.a -= Time.deltaTime; // 시간이 지남에 따라이미지 색을 지움
        }

        image.color = color;
    }
}
