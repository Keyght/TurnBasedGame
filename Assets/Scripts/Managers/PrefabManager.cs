using System.Collections.Generic;
using UnityEngine;

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
