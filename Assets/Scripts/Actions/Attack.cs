using UnityEngine;

public class Attack : Damaging
{
    public override void PerformAction()
    {
        Health.ChangeHealth(_attackValue, true);
    }
}
