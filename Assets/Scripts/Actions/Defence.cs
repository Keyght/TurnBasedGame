using UnityEngine;

public class Defence : Action
{
    [SerializeField]
    private int _defenceValue = 2;

    public override void PerformAction()
    {
        Character.GetAnimator().SetTrigger("Defence");
        Character.GetHealth().AddArmour(_defenceValue);
        Character.Effects.Add(Effect.DEFENDED, 3);
    }
}
