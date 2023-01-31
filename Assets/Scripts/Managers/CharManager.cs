public static class CharManager
{
    public static PrefabManager PrefabManager;

    public static void Init()
    {
        PrefabManager = new PrefabManager("Characters");
        FillerForManagers.FillManager(PrefabManager);
    }
}
