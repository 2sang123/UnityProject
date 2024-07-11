using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameOff : MonoBehaviour
{
    private GameObject player;
    private float needDist = 5f;
   
    public GameObject Flame;

    public GameObject DestroyText;
    private GameObject tooltipInstance; // 생성된 툴팁 인스턴스
    private float textDuration = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnMouseDown()
    {
        Vector3 dist = transform.position - player.transform.position;

        if (dist.magnitude < needDist)
        {
            Destroy(Flame);
            StartCoroutine(TextCoroutine());
        }
    }

    private IEnumerator TextCoroutine()
    {
        tooltipInstance = Instantiate(DestroyText, transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(textDuration);

        if (tooltipInstance != null)
        {
            Destroy(tooltipInstance);
        }
    }

}
