/// <summary>
/// Статический класс для хранения префабов всех действий
/// </summary>
public static class ActionManager
{
    public static PrefabManager PrefabManager;

    public static void Init()
    {
        PrefabManager = new PrefabManager("Actions");
        FillerForManagers.FillManager(PrefabManager);
    }
}
