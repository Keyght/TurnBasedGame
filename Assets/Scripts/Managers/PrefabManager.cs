using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Структуда для определения пути и списка всех префабов
/// </summary>
public struct PrefabManager
{
    public string Path;
    public List<GameObject> AllPrefabs;

    public PrefabManager(string path)
    {
        Path = path;
        AllPrefabs = new List<GameObject>();
    }
}
