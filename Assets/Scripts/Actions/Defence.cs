using UnityEngine;

public class Defence : Action
{
    [SerializeField]
    private int _defenceValue = 2;
    [SerializeField]
    private int _defenceTurns = 3;

    private new void Start()
    {
        base.Start();
        _isSelfTargeted = true;
    }

    public override void PerformAction()
    {
        _target.GetAnimator().SetTrigger("Defence");
        if (_target.Effects.ContainsKey(Effect.DEFENDED))
        {
            _target.Effects[Effect.DEFENDED] += _defenceTurns;
        }
        else
        {
            _target.Effects[Effect.DEFENDED] = _defenceTurns;
        }
        _target.GetHealth().AddArmour(_defenceValue);
    }
}
