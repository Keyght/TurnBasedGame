using UnityEngine;

/// <summary>
/// Класс для действия лечения
/// </summary>
public class Heal : Action
{
    [SerializeField]
    protected static int HealValue = 1;

    private new void Start()
    {
        base.Start();
        IsSelfTargeted = true;
    }

    public override void PerformAction()
    {
        Target.GetAnimator().SetTrigger("Cast");
        Target.Effects.Remove(Effect.POISONED);
        Target.GetHealth().ChangeHealth(HealValue, IsAttacking);
    }

    public static int GetHealValue()
    {
        return HealValue;
    }
}
