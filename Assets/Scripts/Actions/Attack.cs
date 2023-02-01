using UnityEngine;

public class Attack : Action
{
    [SerializeField]
    protected int _attackValue = -3;

    private new void Start()
    {
        base.Start();
        _isEnemyTargeted = true;
    }

    public override void PerformAction()
    {
        _target.GetHealth().ChangeHealth(_attackValue, true);
    }
}
