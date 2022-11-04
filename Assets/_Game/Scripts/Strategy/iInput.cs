using UnityEngine;
public interface iInput
{    
    Vector3 GetMov { get; }
    Vector3 GetLookAt { get; }
    bool IsRunning();
    bool IsAttacking();
    void UpdateInputs();
}
