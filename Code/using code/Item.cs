using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ������ �޴����� �������� ���������ϰ� �ϴ� �ڵ�

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")] //��ũ��Ʈ���� ��Ŭ���ؼ� �� ������ ������Ʈ�� ���� �� �ְ� 
public class Item : ScriptableObject
{

    public string itemName; //�������� �̸��� �����ϴ� ���ڿ� ����
    public ItemType itemType; //  �������� ������ �����ϴ� ������ ����
    public Sprite itemImage; // �������� �̹����� �����ϴ� ��������Ʈ ����
    public GameObject itemPrefab; // �������� �������� �����ϴ� ���� ������Ʈ ����

    public string weaponType; //���� �������� ���, �ش� ������ ������ �����ϴ� ���ڿ� ����

    public enum ItemType 
    {
        Used,
        ETC
    }

}