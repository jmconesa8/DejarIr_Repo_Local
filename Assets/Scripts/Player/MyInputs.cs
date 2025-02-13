using UnityEngine;
using UnityEngine.InputSystem;
public class MyInputs : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    [Tooltip("Interact is 'B' on GamePad and 'F' on Keyboard")]
    public bool interact;
    public bool jump;
    public bool sprint;
    public bool pause;
    public bool crouch;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }
    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }
    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }
    public void OnPause(InputValue value)
    {
        PauseInput(value.isPressed);
    }
    public void OnInteract(InputValue value)
    {
        InteractInput(value.isPressed);
    }
    public void OnCrouch(InputValue value)
    {
        CrouchInput(value.isPressed);   
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }
    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }
    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }
    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }
    public void PauseInput(bool newPauseState)
    {
        pause = newPauseState;
    }
    public void CrouchInput(bool newCrouchState)
    {
        crouch = newCrouchState;
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
    }
}
