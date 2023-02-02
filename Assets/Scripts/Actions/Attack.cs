using UnityEngine;

/// <summary>
/// Класс для действия атаки
/// </summary>
public class Attack : Action
{
    [SerializeField]
    protected static int AttackValue = -3;

    private new void Start()
    {
        base.Start();
        IsEnemyTargeted = true;
    }

    public override void PerformAction()
    {
        Target.GetHealth().ChangeHealth(AttackValue, IsAttacking);
    }

    public static int GetAttackValue()
    {
        return AttackValue;
    }
}
