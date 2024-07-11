using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tutorialUI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject sign;
    public GameObject player;
    public Button closeButton; // �ݴ� ��ư

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sign.gameObject.SetActive(false);

        closeButton.onClick.AddListener(() => CloseUI());
    }

    private void OnMouseDown()
    {
        Vector3 dist = transform.position - player.transform.position;

        if (dist.magnitude < 5f)
        {
            sign.SetActive(true);  //������Ʈ Ŭ���ϸ� �г� ����
            GameManager.isGamePaused = true;

        }
    }

    void CloseUI()
    {
        GameManager.isGamePaused = false;
        sign.SetActive(false); //�г� ����     
    }
}
