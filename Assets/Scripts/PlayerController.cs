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
    private bool isMoving = false;
    private float jumpProgress;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isMoving)
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
            targetPosition = transform.position + direction * moveDistance;
            isMoving = true;
            jumpProgress = 0f; 
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            // 점프 진행도를 증가시킴
            jumpProgress += Time.deltaTime / jumpDuration;

            // 목표 위치로 이동하면서 y값을 포물선 형태로 변화
            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, jumpProgress);
            currentPosition.y += Mathf.Sin(jumpProgress * Mathf.PI) * jumpHeight;

            transform.position = currentPosition;

            if (jumpProgress >= 1f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            UIManager.Instance.cherryCount++;
            Destroy(other.gameObject); 
        }
    }

}

