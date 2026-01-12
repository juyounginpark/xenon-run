using UnityEngine;

public class ZenoTurtlePhysics : MonoBehaviour
{
    [Header("충돌체 설정")]
    [Tooltip("플레이어의 콜라이더를 직접 연결하세요.")]
    public Collider2D playerCollider; 

    [Tooltip("거북이 자신의 콜라이더 (자동으로 가져옵니다).")]
    public Collider2D myCollider;

    [Header("제논 설정")]
    [Tooltip("이 거리 안으로 들어오면 도망가기 시작합니다.")]
    public float detectionRange = 5.0f; 

    [Tooltip("밀어내는 힘 (민감도).")]
    public float repulsionFactor = 5.0f;

    void Start()
    {
        // 자신의 콜라이더가 연결 안 되어 있으면 자동으로 가져옴
        if (myCollider == null)
            myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (playerCollider == null || myCollider == null) return;

        // 1. 두 콜라이더의 '표면(외곽선)' 간 가장 가까운 거리를 정밀 계산
        // 플레이어가 회전해서 머리가 앞으로 튀어나오면 이 거리가 즉시 줄어듭니다.
        ColliderDistance2D distInfo = Physics2D.Distance(myCollider, playerCollider);

        // distInfo.distance: 두 물체 표면 사이의 거리 (겹치면 음수)
        float surfaceDistance = distInfo.distance;

        // 2. 플레이어의 X 위치가 거북이보다 왼쪽에 있는지 확인 (뒤에서 쫓아올 때만 도망)
        // (플레이어 중심점 X < 거북이 중심점 X)
        bool isPlayerBehind = playerCollider.bounds.center.x < myCollider.bounds.center.x;

        // 3. 거리가 감지 범위 내이고, 플레이어가 뒤에 있을 때
        if (isPlayerBehind && surfaceDistance < detectionRange)
        {
            // 이미 겹쳤다면(음수) 아주 큰 값으로 처리하여 즉시 튕겨나가게 함
            float effectiveDistance = Mathf.Max(surfaceDistance, 0.001f);

            // [제논의 역설] 거리가 가까울수록 속도 급증
            float fleeSpeed = repulsionFactor / effectiveDistance;

            // X축으로 이동
            transform.Translate(Vector3.right * fleeSpeed * Time.deltaTime);
        }
    }
}