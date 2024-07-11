using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 폭탄 던지기 관련 코드
public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 40f; // 수류탄을 던질 때 적용되는 힘
    public GameObject grenadePrefab; // 생성할 수류탄 프리팹
                                     

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // K 키를 눌렀을 때
        {
            ThrowGrenade(); // 수류탄을 던지는 함수 호출
        }
    }
    void ThrowGrenade()  // 수류탄 생성
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);  // 수류탄에 Rigidbody 컴포넌트를 가져와서 힘을 가해줌
        Rigidbody rb=grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce,ForceMode.VelocityChange);
    }
}
