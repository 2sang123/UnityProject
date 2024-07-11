using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 폭탄 아이템 영향을 받는 코드

public class Destuctibes : MonoBehaviour
{
    public GameObject destroyedVersion;  // 파괴된 버전의 게임 오브젝트를 저장할 변수


    public void Destroy() // 파괴된 버전의 게임 오브젝트를 생성하고 현재 게임 오브젝트를 파괴함
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
