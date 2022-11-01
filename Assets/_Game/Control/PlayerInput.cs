using UnityEngine;

public class PlayerInput : MonoBehaviour, iInput
{
    private PlayerKeyMap _joyInput;
    #region Axis
    public float GetH => _xAxis;
    public float GetV => _yAxis;
    float _xAxis;
    float _yAxis;
    private float currX;
    private float currY;

    #endregion

    private void Awake()
    {
        _joyInput = new PlayerKeyMap();
        _joyInput.PlayerInputEvents.MoveX.started += ctx => currX = ctx.ReadValue<float>();
        _joyInput.PlayerInputEvents.MoveX.canceled += ctx => currX = 0f;
        _joyInput.PlayerInputEvents.MoveY.started += ctx => currY = ctx.ReadValue<float>();
        _joyInput.PlayerInputEvents.MoveY.canceled += ctx => currY = 0f;
    }

    public bool IsRunning()
    {
        return (GetH != 0 || GetV != 0); 
    }

    public bool IsAttacking()
    {
        return _joyInput.PlayerInputEvents.Attack.triggered;
    }

    
    public void UpdateInputs()
    {
        if (currX == 0f && currY == 0f)
        {
            _xAxis = 0f;
            _yAxis = 0f;
            return;
        }
        _xAxis = currX > 0 ? 1f : -1f;
        _yAxis = currY > 0 ? 1f : -1f;
    }
    private void OnEnable()
    {
       _joyInput.Enable();
    }
}
