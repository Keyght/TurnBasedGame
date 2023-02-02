using UnityEngine;

/// <summary>
/// Класс для действия защиты
/// </summary>
public class Defence : Action
{
    [SerializeField]
    protected static int DefenceValue = 2;
    [SerializeField]
    private int _defenceTurns = 3;

    private new void Start()
    {
        base.Start();
        IsSelfTargeted = true;
    }

    public override void PerformAction()
    {
        Target.GetAnimator().SetTrigger("Defence");
        if (Target.Effects.ContainsKey(Effect.DEFENDED))
        {
            Target.Effects[Effect.DEFENDED] += _defenceTurns;
        }
        else
        {
            Target.Effects[Effect.DEFENDED] = _defenceTurns;
        }
        Target.GetHealth().AddArmour(DefenceValue);
    }

    public static int GetDefenceValue()
    {
        return DefenceValue;
    }
}
