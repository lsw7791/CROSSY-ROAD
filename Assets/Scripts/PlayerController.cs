using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 curMovementInput;
    public float moveDistance;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            JumpMove();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    private void JumpMove()
    {
        Vector3 newPosition = transform.position;

        if (curMovementInput.y > 0) // W 키 입력 
        {
            newPosition += Vector3.forward * moveDistance;
        }
        else if (curMovementInput.x < 0) // A 키 입력 
        {
            newPosition += Vector3.left * moveDistance;
        }
        else if (curMovementInput.x > 0) // D 키 입력 
        {
            newPosition += Vector3.right * moveDistance;
        }

        transform.position = newPosition;
    }
}
