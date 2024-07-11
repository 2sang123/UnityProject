using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템을 설정및 메뉴에서 에셋으로 생성가능하게 하는 코드

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")] //스크립트에서 우클릭해서 새 아이템 오브젝트를 만들 수 있게 
public class Item : ScriptableObject
{

    public string itemName; //아이템의 이름을 저장하는 문자열 변수
    public ItemType itemType; //  아이템의 유형을 저장하는 열거형 변수
    public Sprite itemImage; // 아이템의 이미지를 저장하는 스프라이트 변수
    public GameObject itemPrefab; // 아이템의 프리팹을 저장하는 게임 오브젝트 변수

    public string weaponType; //무기 아이템인 경우, 해당 무기의 유형을 저장하는 문자열 변수

    public enum ItemType 
    {
        Used,
        ETC
    }

}