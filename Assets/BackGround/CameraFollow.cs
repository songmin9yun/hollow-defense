using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // 플레이어

    [SerializeField] private float smoothSpeed = 0.2f;

    // 카메라와 플레이어 사이 거리
    [SerializeField] private Vector3 offset;


    private Vector3 velocity = Vector3.zero;


    void LateUpdate()
    {
        if(target == null)
            return;


        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            transform.position.y + offset.y,
            transform.position.z
        );


        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothSpeed
        );
    }
}