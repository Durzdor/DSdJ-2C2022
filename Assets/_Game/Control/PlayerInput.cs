using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour , iInput
{
    private PlayerKeyMap _joyInput;
    private InputAction _movementInputActions;
    private InputAction _lookAtInputActions;
    #region Axis
    private Vector2 _currMovementInput;
    private Vector3 _movement;
    private Vector3 _lookingAt;// = Mouse.current.position.ReadValue();
    public Vector3 GetMov => _movement;
    public Vector3 GetLookAt => _lookingAt;
    
    private bool _isMoving;
    #endregion
    private void Awake()
    {
        _joyInput = new PlayerKeyMap();
        _movementInputActions = _joyInput.PlayerInputEvents.Move;
        _lookAtInputActions = _joyInput.PlayerInputEvents.Look;
        
        _movementInputActions.started += OnMoveStarted;
        _movementInputActions.canceled += OnMoveCancelled;
        _lookAtInputActions.performed += MouseMoving;
        
    }

    private void MouseMoving(InputAction.CallbackContext ctx)
    {
        _lookingAt = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
    }

    #region Events Methods

    private void OnMoveStarted(InputAction.CallbackContext ctx)
    {
        _currMovementInput = ctx.ReadValue<Vector2>();
        _isMoving = true;
    }
    private void OnMoveCancelled(InputAction.CallbackContext ctx)
    {
        _currMovementInput = Vector2.zero;
        _isMoving = false;
    }

    #endregion
    public bool IsRunning()
    {
        return _isMoving; 
    }
    public bool IsAttacking()
    {
        return _joyInput.PlayerInputEvents.Attack.triggered;
    }
    public void UpdateInputs()
    {
        if (!_isMoving) return;
        _movement = new Vector3(_currMovementInput.x, 0, _currMovementInput.y);
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
