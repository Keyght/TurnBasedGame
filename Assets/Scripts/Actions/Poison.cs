using UnityEngine;

public class Poison : Action
{
    [SerializeField]
    private int _poisonValue = -1;
    [SerializeField]
    private int _poisonTunrs = 1;

    private new void Start()
    {
        base.Start();
        _isEnemyTargeted = true;
    }

    public override void PerformAction()
    {
        if (_target.Effects.ContainsKey(Effect.POISONED))
        {
            _target.Effects[Effect.POISONED] += _poisonTunrs;
        }
        else
        {
            _target.Effects[Effect.POISONED] = _poisonTunrs;
        }
        _target.GetHealth().ChangeHealth(_poisonValue, true);
    }
}
