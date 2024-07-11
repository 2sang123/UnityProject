using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ������ ����ϴ� �̵� �ڵ�
public class PlayerController : MonoBehaviour
{
    private TopView topViewScript; // ž�� ��ũ��Ʈ ����

    [SerializeField]
    public float walkSpeed; // �ȴ� �ӵ��� �޴� ��

    [SerializeField]
    public float runSpeed; // �޸��� �ӵ��� �޴� ��
    [SerializeField]
    public float crouchSpeed; // �� ���� �ӵ��� �޴� ��
    private float applySpeed; // ����� �ӵ��� �޴� ��

    [SerializeField]
    private float jumpForce; // ���� ���̸� �޴� ��

    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    private CapsuleCollider capsuleCollider; // capsuleCollider�� ����

    [SerializeField]
    public float lookSensitivity; // ���콺 ������ ����

    [SerializeField]
    private float cameraRotationLimit; // ī�޶� ȸ�� ��
    private float currentCameraRotationX = 0; // ���� x�� ī�޶� ȸ���� 0����

    [SerializeField]
    private Camera theCamera; // ī�޶� ����
  
    private Rigidbody myRigid; // Rigidbody�� ����

    // Start is called before the first frame update
    void Start()
    {
        topViewScript = GetComponent<TopView>(); // ž�� ��ũ��Ʈ ������Ʈ ��������
        capsuleCollider = GetComponent<CapsuleCollider>(); // Component���� CapsuleCollider�� ����
        myRigid = GetComponent<Rigidbody>(); // Component���� Rigidbody�� ����
        applySpeed = walkSpeed; // ���� ����Ǵ� �ӵ��� �⺻ �ӵ�
        originPosY = theCamera.transform.localPosition.y; // ����
        applyCrouchPosY = originPosY; // ����
    }

    // Update is called once per frame
    void Update()
    {
        IsGround(); // �浹 ����
        TryJump(); // ����
        TryRun();  // �޸���
        TryCrouch();  // ���̱�
        Move(); // �̵�
        CameraRotation(); // ī�޶� ȸ��
        CharacterRotation(); // ĳ���� ȸ��
    }
    
    private void TryCrouch() 
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            Crouch();
        }
    }
        private void Crouch() //���̱�
        {
            isCrouch = !isCrouch; 
            if (isCrouch) // Lctrl Ű�� ������
            {
                applySpeed = crouchSpeed;
                applyCrouchPosY = crouchPosY;
            }
            else // �� ������
            {
                applySpeed = walkSpeed;
                applyCrouchPosY = originPosY;
            }
        StartCoroutine(CrouchCoroutine());  
        }
    IEnumerator CrouchCoroutine() // ���̱� ���� ���� ���� Ŭ����
    {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;

        while(_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f);
        
    }
    private void IsGround() // �浹 ����
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
    private void TryJump() // space�� ������ �۵��ϴ� ���� Ŭ����
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }
    private void Jump()
    {
        if (isCrouch)
            Crouch();
        myRigid.velocity = transform.up * jumpForce;
    }
    private void TryRun() // �޸���
    {
        if (Input.GetKey(KeyCode.LeftShift)) // Lshift Ű�� ������ ����
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) // Lshift Ű�� ��
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        if (isCrouch) // ���̱⸦ ����
            Crouch();
        isRun = true;
        applySpeed = runSpeed; 
    }
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }
    private void Move() // �̵� Ŭ����
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
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
}
