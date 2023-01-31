using UnityEngine;

public class Attack : Action
{
    [SerializeField]
    private int _attackValue = -3;

    public override void PerformAction()
    {
        Health.ChangeHealth(_attackValue);
    }
}
