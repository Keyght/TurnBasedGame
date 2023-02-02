using UnityEngine;

/// <summary>
/// Статический класс для заполнения менеджеров префабов
/// </summary>
public static class FillerForManagers
{
    public static void FillManager(PrefabManager prefabManager)
    {
        var allPrefs = Resources.LoadAll<GameObject>(prefabManager.Path);
        foreach (var pref in allPrefs)
        {
            prefabManager.AllPrefabs.Add(pref);
        }
    }
}