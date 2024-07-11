using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


// 아이템에 대한 툴팁을 보여주는 코드

public class ShowTooltip : MonoBehaviour
{
    public GameObject tooltipPrefab; // 툴팁 프리팹
    private GameObject tooltipInstance; // 생성된 툴팁 인스턴스
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 플레이어를 태그로 찾음
    }


    private void OnMouseEnter() // 마우스가 오브젝트를 가르키면
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // UI를 가리키고 있으면 아무것도 하지 않고 종료
        }

        // 플레이어와 아이템 사이 거리 계산
        Vector3 dist = transform.position - player.transform.position;

        if (dist.magnitude < 5f) // 거리가 5f 이내이면
        {
            // 툴팁 프리팹을 인스턴스화하고 표시
            tooltipInstance = Instantiate(tooltipPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
    }

    private void OnMouseExit() // 마우스가 해당 오브젝트를 벗어나면 
    {
        // 생성된 툴팁 인스턴스가 있다면 파괴
        if (tooltipInstance != null)
        {
            Destroy(tooltipInstance);
        }
    }

    private void OnDestroy()
    {
        // 아이템 파괴 시 생성된 툴팁 인스턴스도 파괴
        if (tooltipInstance != null)
        {
            Destroy(tooltipInstance);
        }

    }
}
