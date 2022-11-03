using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour , iInput
{
    private PlayerKeyMap _joyInput;
    private InputAction _movementInputActionsX;
    private InputAction _movementInputActionsY;
    private InputAction _lookAtInputActions;
    #region Axis
    private Vector3 _currMovementInput;
    private Vector3 _movement;
    private Vector3 _lookingAt;
    public Vector3 GetMov => _movement;
    public Vector3 GetLookAt => _lookingAt;
    private bool IsMoving => _currMovementInput!=Vector3.zero;
    #endregion
    private void Awake()
    {
        _joyInput = new PlayerKeyMap();
        _movementInputActionsX = _joyInput.PlayerInputEvents.MoveX;
        _movementInputActionsY = _joyInput.PlayerInputEvents.MoveY;
        _lookAtInputActions = _joyInput.PlayerInputEvents.Look;

        _lookAtInputActions.performed   += MouseMoving;
        _movementInputActionsX.started   += OnMoveStartedX;
        _movementInputActionsX.canceled  += OnMoveCancelledX;
        _movementInputActionsY.started   += OnMoveStartedY;
        _movementInputActionsY.canceled  += OnMoveCancelledY;
    }

    private void MouseMoving(InputAction.CallbackContext ctx)
    {
        if (Camera.main != null) _lookingAt = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    #region Events Methods
    private void OnMoveStartedX(InputAction.CallbackContext ctx)
    {
        _currMovementInput.x = ctx.ReadValue<float>();
    } 
    private void OnMoveStartedY(InputAction.CallbackContext ctx)
    {
        _currMovementInput.z = ctx.ReadValue<float>();
    }
    private void OnMoveCancelledX(InputAction.CallbackContext ctx)
    {
        _currMovementInput.x = 0f;
    }
     private void OnMoveCancelledY(InputAction.CallbackContext ctx)
    {
        _currMovementInput.z = 0f;
    }
    #endregion
    public bool IsRunning()
    {
        return IsMoving; 
    }
    public bool IsAttacking()
    {
        return _joyInput.PlayerInputEvents.Attack.triggered;
    }
    public void UpdateInputs()
    {
        if (!IsMoving) return;
        _movement = _currMovementInput;
    }
    private void OnEnable()
    {
       _joyInput.Enable();
    } 
    private void OnDisable()
    {
       _joyInput.Disable();
    }
}
