using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 이전에 사용하던 이동 코드
public class PlayerController : MonoBehaviour
{
    private TopView topViewScript; // 탑뷰 스크립트 참조

    [SerializeField]
    public float walkSpeed; // 걷는 속도를 받는 값

    [SerializeField]
    public float runSpeed; // 달리는 속도를 받는 값
    [SerializeField]
    public float crouchSpeed; // 기어서 가는 속도를 받는 값
    private float applySpeed; // 적용된 속도를 받는 값

    [SerializeField]
    private float jumpForce; // 점프 높이를 받는 값

    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    private CapsuleCollider capsuleCollider; // capsuleCollider를 받음

    [SerializeField]
    public float lookSensitivity; // 마우스 감도를 받음

    [SerializeField]
    private float cameraRotationLimit; // 카메라 회전 값
    private float currentCameraRotationX = 0; // 현재 x축 카메라 회전을 0으로

    [SerializeField]
    private Camera theCamera; // 카메라를 받음
  
    private Rigidbody myRigid; // Rigidbody를 받음

    // Start is called before the first frame update
    void Start()
    {
        topViewScript = GetComponent<TopView>(); // 탑뷰 스크립트 컴포넌트 가져오기
        capsuleCollider = GetComponent<CapsuleCollider>(); // Component에서 CapsuleCollider를 받음
        myRigid = GetComponent<Rigidbody>(); // Component에서 Rigidbody를 받음
        applySpeed = walkSpeed; // 현재 적용되는 속도는 기본 속도
        originPosY = theCamera.transform.localPosition.y; // 보류
        applyCrouchPosY = originPosY; // 보류
    }

    // Update is called once per frame
    void Update()
    {
        IsGround(); // 충돌 판정
        TryJump(); // 점프
        TryRun();  // 달리기
        TryCrouch();  // 숙이기
        Move(); // 이동
        CameraRotation(); // 카메라 회전
        CharacterRotation(); // 캐릭터 회전
    }
    
    private void TryCrouch() 
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            Crouch();
        }
    }
        private void Crouch() //숙이기
        {
            isCrouch = !isCrouch; 
            if (isCrouch) // Lctrl 키를 누르면
            {
                applySpeed = crouchSpeed;
                applyCrouchPosY = crouchPosY;
            }
            else // 안 누르면
            {
                applySpeed = walkSpeed;
                applyCrouchPosY = originPosY;
            }
        StartCoroutine(CrouchCoroutine());  
        }
    IEnumerator CrouchCoroutine() // 숙이기 시점 조절 관련 클래스
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
    private void IsGround() // 충돌 판정
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
    private void TryJump() // space를 누르면 작동하는 점프 클래스
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
    private void TryRun() // 달리기
    {
        if (Input.GetKey(KeyCode.LeftShift)) // Lshift 키를 누르고 있음
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) // Lshift 키를 땜
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        if (isCrouch) // 숙이기를 해제
            Crouch();
        isRun = true;
        applySpeed = runSpeed; 
    }
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }
    private void Move() // 이동 클래스
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
    private void CharacterRotation() // 플레이어 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); 
    }
    private void CameraRotation() // 카메라 회전
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
