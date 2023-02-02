using UnityEngine;

public class Heal : Action
{
    [SerializeField]
    protected static int _healValue = 1;

    private new void Start()
    {
        base.Start();
        _isSelfTargeted = true;
    }

    public override void PerformAction()
    {
        _target.GetAnimator().SetTrigger("Cast");
        _target.Effects.Remove(Effect.POISONED);
        _target.GetHealth().ChangeHealth(_healValue, _isAttacking);
    }

    public static int GetHealValue()
    {
        return _healValue;
    }
}
