using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 이동 관련 코드
public class Movement : MonoBehaviour
{
    private TopView topViewScript; // 탑뷰 스크립트 참조

    [SerializeField]
    public float walkSpeed; // 걷는 속도를 받는 값

    private float applySpeed; // 적용된 속도를 받는 값

    private CapsuleCollider capsuleCollider; // capsuleCollider를 받음

    [SerializeField]
    public float lookSensitivity; // 마우스 감도를 받음

    [SerializeField]
    private float cameraRotationLimit; // 카메라 회전 값
    private float currentCameraRotationX = 0; // 현재 x축 카메라 회전을 0으로

    [SerializeField]
    private Camera theCamera; // 카메라를 받음

    private Rigidbody myRigid; // Rigidbody를 받음

    // private Animator myanim; 애니메이션 테스트

    // Start is called before the first frame update
    void Start()
    {
        // myanim = GetComponent<Animator>(); 애니메이션 테스트
        topViewScript = GetComponent<TopView>(); // 탑뷰 스크립트 컴포넌트 가져오기
        capsuleCollider = GetComponent<CapsuleCollider>(); // Component에서 CapsuleCollider를 받음
        myRigid = GetComponent<Rigidbody>(); // Component에서 Rigidbody를 받음
        applySpeed = walkSpeed; // 현재 적용되는 속도는 기본 속도
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGamePaused == false) // pause 메뉴에 들어간 상태가 아니라면 <-- PauseMenu코드 참조
        {
            Move(); // 이동
            CameraRotation(); // 카메라 회전
            CharacterRotation(); // 캐릭터 회전
        }

    }

    private void Move() // 이동 클래스
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        //myRigid.AddForce(_velocity * Time.deltaTime); // 이걸로 하면 rigidbody 설정 많이 해야 됨
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
    public void SetSpeed(float speed)
    {
        applySpeed = speed;
    }

}
