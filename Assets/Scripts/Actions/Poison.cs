using UnityEngine;

/// <summary>
/// Класс для действия отравления
/// </summary>
public class Poison : Action
{
    [SerializeField]
    protected static int PoisonValue = -1;
    [SerializeField]
    protected int PoisonTunrs = 1;

    private new void Start()
    {
        base.Start();
        IsEnemyTargeted = true;
    }

    public override void PerformAction()
    {
        if (Target.Effects.ContainsKey(Effect.POISONED))
        {
            Target.Effects[Effect.POISONED] += PoisonTunrs;
        }
        else
        {
            Target.Effects[Effect.POISONED] = PoisonTunrs;
        }
        Target.GetHealth().ChangeHealth(PoisonValue, IsAttacking);
    }

    public static int GetPoisonValue()
    {
        return PoisonValue;
    }
}
