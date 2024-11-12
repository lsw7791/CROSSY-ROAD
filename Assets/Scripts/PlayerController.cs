using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 curMovementInput;
    public float moveDistance;
    public float moveSpeed;
    public float jumpHeight;
    public float jumpDuration;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 fallbackPosition; 
    private bool isMoving = false;
    private bool isReturning = false; 
    private float jumpProgress;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isMoving && !isReturning)
        {
            curMovementInput = context.ReadValue<Vector2>();
            SetTargetPosition();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    private void SetTargetPosition()
    {
        Vector3 direction = Vector3.zero;

        if (curMovementInput.y > 0) // W 키 입력 
        {
            direction = Vector3.forward;
        }
        else if (curMovementInput.x < 0) // A 키 입력 
        {
            direction = Vector3.left;
        }
        else if (curMovementInput.x > 0) // D 키 입력 
        {
            direction = Vector3.right;
        }

        if (direction != Vector3.zero)
        {
            startPosition = transform.position;
            fallbackPosition = startPosition; // 현재 위치 저장
            targetPosition = transform.position + direction * moveDistance;
            isMoving = true;
            jumpProgress = 0f;
        }
    }

    private void Update()
    {
        if (isMoving || isReturning)
        {
            // 점프 진행도를 증가시킴
            jumpProgress += Time.deltaTime / jumpDuration;

            // 이동 중일 때와 되돌아올 때 위치 계산
            Vector3 currentPosition;
            if (isReturning)
            {
                currentPosition = Vector3.Lerp(targetPosition, fallbackPosition, jumpProgress);
            }
            else
            {
                currentPosition = Vector3.Lerp(startPosition, targetPosition, jumpProgress);
            }

            // y값을 포물선 형태로 변화
            currentPosition.y += Mathf.Sin(jumpProgress * Mathf.PI) * jumpHeight;
            transform.position = currentPosition;

            if (jumpProgress >= 1f)
            {
                if (isReturning)
                {
                    transform.position = fallbackPosition;
                    isReturning = false;
                }
                else
                {
                    transform.position = targetPosition;
                    isMoving = false;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            if (isMoving)
            {
                isMoving = false;
                isReturning = true;
                jumpProgress = 0f; // 되돌아오는 모션 초기화
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            GameManager.Instance.cherryCount++;
            Destroy(other.gameObject);
        }
    }
}
