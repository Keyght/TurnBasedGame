namespace Storage
{
    /// <summary>
    /// Статический класс для хранения префабов всех действий
    /// </summary>
    public static class ActionStorage
    {
        private static PrefabStorage _prefabStorage;

        public static PrefabStorage PrefabStorage => _prefabStorage;

        public static void Init()
        {
            _prefabStorage = new PrefabStorage("Actions");
            FillerForStorages.FillManager(_prefabStorage);
        }
    }
}