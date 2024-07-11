using UnityEngine;

public class ProjectileWithoutRigidbody : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 15f;

    [SerializeField] float destroyTime = 2f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾ ������ ��� �ı�
            Destroy(gameObject);
        }
    }


    private void Update()   //you can change this to a virtual function for multiple projectile types
    {
        transform.Translate(new Vector3(0f, 0f, projectileSpeed * Time.deltaTime));
    }
}
