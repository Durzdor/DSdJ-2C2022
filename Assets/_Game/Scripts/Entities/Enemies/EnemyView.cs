using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Subscribe(EnemyModel model)
    {
        model.OnRun += Move;
        model.OnAttack += Attack;
    }

    private void Attack()
    {
        print("Lala");
        _animator.Play("Goblin Attack");
    }

    private void Move()
    {
        _animator.Play("Goblin run");
    }
}