namespace Storage
{
    /// <summary>
    /// Статический класс для хранения префабов всех персонажей
    /// </summary>
    public static class CharStorage
    {
        private static PrefabStorage _prefabStorage;

        public static PrefabStorage PrefabStorage => _prefabStorage;

        public static void Init()
        {
            _prefabStorage = new PrefabStorage("Characters");
            FillerForStorages.FillManager(_prefabStorage);
        }
    }
}