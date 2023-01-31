using UnityEngine;

public class Defence : Action
{
    [SerializeField]
    private int _defenceValue = 2;

    public override void PerformAction()
    {
        Health.ChangeHealth(_defenceValue, false);
    }
}
