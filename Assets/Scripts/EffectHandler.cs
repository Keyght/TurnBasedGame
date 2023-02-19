using Actions;
using UnityEngine;

public enum Effect
{
    Poisoned,
    Defended
}

/// <summary>
/// Статический класс для работы с эффектами
/// </summary>
public static class EffectHandler
{
    private static readonly int _defence = Animator.StringToHash("Defence");
    private static readonly int _idle = Animator.StringToHash("Idle");

    public static void HandleEffect(Character character)
    {
        if (character.Effects.ContainsKey(Effect.Defended))
        {
            character.Effects[Effect.Defended] -= 1;
            if (character.Effects[Effect.Defended] == 0 || character.Health.Armour.Value == 0)
            {
                character.Health.Armour.ReduceArmour(-1 * Defence.DefenceValue);
                character.Effects.Remove(Effect.Defended);
                character.AnimCtrl.ResetTrigger(_defence);
                character.AnimCtrl.SetTrigger(_idle);
            }
            else if (character.Effects[Effect.Defended] != 0 && character.Health.Armour.Value != 0)
            {
                character.AnimCtrl.ResetTrigger(_idle);
                character.AnimCtrl.SetTrigger(_defence);
            }
        }

        if (character.Effects.ContainsKey(Effect.Poisoned))
        {
            character.Effects[Effect.Poisoned] -= 1;
            if (character.Effects[Effect.Poisoned] == 0)
            {
                character.Effects.Remove(Effect.Poisoned);
            }

            character.Health.ChangeHealth(Poison.PoisonValue, true);
        }
    }
}