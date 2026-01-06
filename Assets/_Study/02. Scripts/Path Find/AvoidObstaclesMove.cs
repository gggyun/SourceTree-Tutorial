using UnityEngine;

public class AvoidObstaclesMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mass = 5f;
    public float force = 50f;
    public float minDistToAvoid = 5f;

    private Vector3 targetPoint;
    public float steeringForce = 10f;

    public LayerMask obstacleMask;

    void Start()
    {
        targetPoint = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        { // 마우스 왼쪽 버튼 클릭 시
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f))
            { // 레이캐스트가 장애물에 닿으면
                targetPoint = hit.point; // 목표 지점 설정
            }
        }

        Vector3 dir = (targetPoint - transform.position).normalized; // 목적지로 이동하려는 방향

        dir = GetAvoidanceDirection(dir); // 장애물이 있는지 확인

        if (Vector3.Distance(targetPoint, transform.position) < 1f)
            return;

        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, steeringForce * Time.deltaTime);

    }

    private Vector3 GetAvoidanceDirection(Vector3 dir)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, minDistToAvoid, obstacleMask))
        {
            Vector3 hitNormal = hit.normal; // 충돌한 표면의 법선 벡터
            hitNormal.y = 0; // 수평면에서만 회피

            dir = transform.forward + hitNormal * force; // 회피 방향 계산
            dir.Normalize();
        }

        return dir;
    }
}
