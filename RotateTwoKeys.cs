using UnityEngine;
using UnityEngine.InputSystem;

public class RotateAddKeys : MonoBehaviour
{
    [Header("1번 키 설정 (빼기)")]
    public Key key1 = Key.O;       
    public float angle1 = -15f;    // 누르면 현재 각도에서 -15도

    [Header("2번 키 설정 (더하기)")]
    public Key key2 = Key.P;       
    public float angle2 = 15f;     // 누르면 현재 각도에서 +15도

    [Header("공통 설정")]
    public float rotationSpeed = 5f; 

    private float _targetZ; // 목표 각도를 숫자로 관리

    void Start()
    {
        // 시작할 때 현재 물체의 각도를 기준점으로 잡음
        _targetZ = transform.eulerAngles.z;
    }

    void Update()
    {
        // 키보드 연결 확인 (오류 방지)
        if (Keyboard.current == null) return;

        // --- 1번 키 입력 (각도 더하기/빼기) ---
        if (key1 != Key.None && Keyboard.current[key1].wasPressedThisFrame)
        {
            _targetZ += angle1; // 현재 목표값에 -15를 더함
        }

        // --- 2번 키 입력 (각도 더하기/빼기) ---
        if (key2 != Key.None && Keyboard.current[key2].wasPressedThisFrame)
        {
            _targetZ += angle2; // 현재 목표값에 +15를 더함
        }

        // 목표 각도로 변환 (Quaternion 계산)
        Quaternion targetRotation = Quaternion.Euler(0, 0, _targetZ);

        // 부드럽게 회전 적용
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}