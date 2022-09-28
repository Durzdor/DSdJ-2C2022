using UnityEngine;

public class GenericStatModifier : Interactable
{
    [Header("Display Name")] [Space(5)] [SerializeField]
    private string modifierName;

    [Header("Modifier Values")] [Space(5)] [SerializeField] [Tooltip("Stat has I for Int and F for float")]
    private StatNames stat;

    [SerializeField] private bool isBuff;

    //-2147483648
    [SerializeField] [Tooltip("Modify only the correct one")]
    private int modifierIntValue = int.MinValue;

    //-3.402823e+38
    [SerializeField] [Tooltip("Modify only the correct one")]
    private float modifierFloatValue = float.MinValue;

    private void Start()
    {
        InteractableName = modifierName;
    }

    public override void Interaction()
    {
        Character.CharacterM.NewCharacterStats.ChangeModifier(stat, isBuff, modifierIntValue, modifierFloatValue);
        Destroy(gameObject, 0.1f);
    }
}