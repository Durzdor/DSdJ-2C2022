using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Subscribe(EnemyController controller)
    {
        controller.OnMove += Move;
        controller.OnAttack += Attack;
    }

    private void Attack()
    {
        _animator.Play("Goblin Attack");
    }

    private void Move(Vector3 dir)
    {
        _animator.Play("Goblin run");
    }
}