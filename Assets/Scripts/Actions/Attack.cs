using UnityEngine;

public class Attack : Damaging
{
    public override void PerformAction()
    {
        Character.GetHealth().ChangeHealth(_attackValue, true);
    }
}
