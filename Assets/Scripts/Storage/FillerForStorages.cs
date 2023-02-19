using UnityEngine;

namespace Storage
{
    /// <summary>
    /// Статический класс для заполнения менеджеров префабов
    /// </summary>
    public static class FillerForStorages
    {
        public static void FillManager(PrefabStorage prefabStorage)
        {
            var allPrefs = Resources.LoadAll<GameObject>(prefabStorage.Path);
            foreach (var pref in allPrefs)
            {
                prefabStorage.AllPrefabs.Add(pref);
            }
        }
    }
}