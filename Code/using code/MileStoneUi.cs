using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MileStoneUi : MonoBehaviour
{
    public GameObject mileUi;  //Ui
    public GameObject backimage;  //Ui

    private GameObject player;
    protected float playLookSensitivityNum; // ±âº» °¨µµ °ª ÀúÀå
    private float needDist = 5f;

    public Button closeButton; // ´Ý´Â ¹öÆ°

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mileUi.SetActive(false);
        backimage.SetActive(false);
        closeButton.onClick.AddListener(() => ClosePanel());
    }


    private void OnMouseDown()
    {
        Vector3 dist = transform.position - player.transform.position;

        if (dist.magnitude < needDist)
        {
            mileUi.SetActive(true);
            backimage.SetActive(true);
            GameManager.isGamePaused = true;
        }
    }

    void ClosePanel()
    {
        GameManager.isGamePaused = false;
        backimage.SetActive(false);
        mileUi.SetActive(false); //ÆÐ³Î ´ÝÈû     

    }

}
