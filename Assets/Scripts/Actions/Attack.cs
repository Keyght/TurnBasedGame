using UnityEngine;

public class Attack : Action
{
    [SerializeField]
    protected static int _attackValue = -3;

    private new void Start()
    {
        base.Start();
        _isEnemyTargeted = true;
    }

    public override void PerformAction()
    {
        _target.GetHealth().ChangeHealth(_attackValue, _isAttacking);
    }

    public static int GetAttackValue()
    {
        return _attackValue;
    }
}
