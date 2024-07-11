using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� �̵� ���� �ڵ�
public class Movement : MonoBehaviour
{
    private TopView topViewScript; // ž�� ��ũ��Ʈ ����

    [SerializeField]
    public float walkSpeed; // �ȴ� �ӵ��� �޴� ��

    private float applySpeed; // ����� �ӵ��� �޴� ��

    private CapsuleCollider capsuleCollider; // capsuleCollider�� ����

    [SerializeField]
    public float lookSensitivity; // ���콺 ������ ����

    [SerializeField]
    private float cameraRotationLimit; // ī�޶� ȸ�� ��
    private float currentCameraRotationX = 0; // ���� x�� ī�޶� ȸ���� 0����

    [SerializeField]
    private Camera theCamera; // ī�޶� ����

    private Rigidbody myRigid; // Rigidbody�� ����

    // private Animator myanim; �ִϸ��̼� �׽�Ʈ

    // Start is called before the first frame update
    void Start()
    {
        // myanim = GetComponent<Animator>(); �ִϸ��̼� �׽�Ʈ
        topViewScript = GetComponent<TopView>(); // ž�� ��ũ��Ʈ ������Ʈ ��������
        capsuleCollider = GetComponent<CapsuleCollider>(); // Component���� CapsuleCollider�� ����
        myRigid = GetComponent<Rigidbody>(); // Component���� Rigidbody�� ����
        applySpeed = walkSpeed; // ���� ����Ǵ� �ӵ��� �⺻ �ӵ�
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGamePaused == false) // pause �޴��� �� ���°� �ƴ϶�� <-- PauseMenu�ڵ� ����
        {
            Move(); // �̵�
            CameraRotation(); // ī�޶� ȸ��
            CharacterRotation(); // ĳ���� ȸ��
        }

    }

    private void Move() // �̵� Ŭ����
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        //myRigid.AddForce(_velocity * Time.deltaTime); // �̰ɷ� �ϸ� rigidbody ���� ���� �ؾ� ��
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

    }
    private void CharacterRotation() // �÷��̾� ȸ��
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
    private void CameraRotation() // ī�޶� ȸ��
    {
        if (topViewScript.isTopViewActive == false)
        {
            float _xRotation = Input.GetAxisRaw("Mouse Y");
            float _cameraRotationX = _xRotation * lookSensitivity;
            currentCameraRotationX -= _cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
            theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }
    public void SetSpeed(float speed)
    {
        applySpeed = speed;
    }

}
