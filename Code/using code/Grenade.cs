using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 폭탄 기능 관련 코드

public class Grenade : MonoBehaviour
{
    public float delay = 3f; //폭발까지 딜레이의 시간
    public float radius = 5f; //폭발범위
    public float force = 700f; //폭발 힘
    public GameObject explosionEffect; //폭발 효과 프리팹
    float countdown; //남은 딜레이시간
    bool hasExploded = false; //이미 폭발했는지 여부
    void Start()
    {
        countdown = delay; //딜레이 시간으로 초기화
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime; //경과된 시간을 카운트다운에서 빼준다
        if (countdown <= 0f && !hasExploded) 
        {
            Explode(); //폭발처리함수호출
            hasExploded = true; //이미 폭발한 상태로 변경
        }
    }

    void Explode()
    {
        //폭발 효과 생성
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); //폭발 범위 내의 모든 colider를 가져옴

        foreach (Collider nearbyobject in colliders) //폭발 범위 내의 모든 collider를 가해줌
        {
            Rigidbody rb = nearbyobject.GetComponent<Rigidbody>(); // // 폭발에 의해 파괴되는 오브젝트인 경우 Destroy() 메소드 호출
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

             Destuctibes dest=nearbyobject.GetComponent<Destuctibes>(); //자기 자신도 폭발에 의해 파괴됨
            if(dest != null)
            {
                dest.Destroy();
            }

            Destroy(gameObject);

        }
    }
}