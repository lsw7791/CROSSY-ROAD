using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset; 
    public float smoothSpeed = 0.125f; 

    void LateUpdate()
    {
        // 목표 위치를 계산 (플레이어 위치 + 오프셋)
        Vector3 targetPosition = player.position + offset;

        // 카메라의 현재 위치를 목표 위치로 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}
