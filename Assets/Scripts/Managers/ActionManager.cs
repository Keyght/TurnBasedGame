public static class ActionManager
{
    public static PrefabManager PrefabManager;

    public static void Init()
    {
        PrefabManager = new PrefabManager("Actions");
        FillerForManagers.FillManager(PrefabManager);
    }
}
