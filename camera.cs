using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10f; // 값을 조금 더 높여보세요 (5 -> 10)
    public Vector3 offset;

    // 물리 이동(AddForce)을 따라갈 때는 FixedUpdate가 더 부드러울 수 있습니다.
    void FixedUpdate() 
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        
        // Z축 고정 (배경이 깜빡거리는 문제 방지)
        smoothedPosition.z = -10f; 

        transform.position = smoothedPosition;
    }
}