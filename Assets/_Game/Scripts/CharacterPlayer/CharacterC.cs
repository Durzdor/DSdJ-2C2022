using System;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterC : MonoBehaviour
{
    public CharacterM CharacterM { get; private set; }
    private float _firingInterval;
    [CanBeNull] public Interactable Interactable { get; set; }
    private bool _isInInteractRange;
    public event Action OnCharacterInteract;
    public event Action<InteractionType> OnCharacterInteractRange;

    public bool IsInInteractRange
    {
        get => _isInInteractRange;
        set
        {
            _isInInteractRange = value;
            OnCharacterInteractRange?.Invoke(Interactable != null
                ? Interactable.InteractionType
                : InteractionType.None);
        }
    }

    private void Awake()
    {
        CharacterM = GetComponent<CharacterM>();
    }

    private void Start()
    {
        CharacterM.Health.OnConsumed += () => print($"Player HP: {CharacterM.Health.CurrentHealth}");
    }

    private void Update()
    {
        LookAtMouse();
        MoveUpdate();
        ShootUpdate();
        CharacterInteractionUpdate();
    }

    private void MoveUpdate()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0)
            CharacterM.Move(new Vector3(h, 0, v));
        else
            CharacterM.Move(Vector3.zero);
    }

    private void ShootUpdate()
    {
        _firingInterval -= Time.deltaTime;
        if (_firingInterval <= 0f)
            if (Input.GetButton("Fire1"))
            {
                CharacterM.Shoot();
                _firingInterval = CharacterM.Stats.TotalAttackSpeed;
            }
    }

    private void LookAtMouse()
    {
        var lookAt = CharacterM.GetMouseWorldPosition();
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    }

    private void CharacterInteractionUpdate()
    {
        if (Input.GetKeyDown(KeyCode.V) && IsInInteractRange)
        {
            if (Interactable == null) return;
            OnCharacterInteract?.Invoke();
            CharacterM.CharacterInteraction(Interactable);
        }
    }
}