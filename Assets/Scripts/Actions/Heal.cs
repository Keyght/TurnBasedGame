using UnityEngine;

public class Heal : Action
{
    [SerializeField]
    private int _healValue = 1;

    private new void Start()
    {
        base.Start();
        _isSelfTargeted = true;
    }

    public override void PerformAction()
    {
        _target.GetAnimator().SetTrigger("Cast");
        _target.Effects.Remove(Effect.POISONED);
        _target.GetHealth().ChangeHealth(_healValue, false);
    }
}
