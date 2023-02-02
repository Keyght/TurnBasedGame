public enum Effect
{
    POISONED,
    DEFENDED
}

/// <summary>
/// Статический класс для работы с эффектами
/// </summary>
public static class EffectHandler
{
    public static void HandleEffect(Character character)
    {
        if (character.Effects.ContainsKey(Effect.DEFENDED))
        {
            character.Effects[Effect.DEFENDED] -= 1;
            if (character.Effects[Effect.DEFENDED] == 0 || character.GetHealth().GetCurrentArmour() == 0)
            {
                character.GetHealth().ReduceArmour(-1 * Defence.GetDefenceValue());
                character.Effects.Remove(Effect.DEFENDED);
                character.GetAnimator().ResetTrigger("Defence");
                character.GetAnimator().SetTrigger("Idle");
            }
            else if (character.Effects[Effect.DEFENDED] != 0 && character.GetHealth().GetCurrentArmour() != 0)
            {
                character.GetAnimator().ResetTrigger("Idle");
                character.GetAnimator().SetTrigger("Defence");
            }
        }
        if (character.Effects.ContainsKey(Effect.POISONED))
        {
            character.Effects[Effect.POISONED] -= 1;
            if (character.Effects[Effect.POISONED] == 0)
            {
                character.Effects.Remove(Effect.POISONED);
            }
            character.GetHealth().ChangeHealth(Poison.GetPoisonValue(), true);
        }
    }
}
