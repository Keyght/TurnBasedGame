using System.Collections.Generic;
using UnityEngine;

namespace Storage
{
    /// <summary>
    /// Структуда для определения пути и списка всех префабов
    /// </summary>
    public struct PrefabStorage
    {
        private string _path;
        private List<GameObject> _allPrefabs;

        public string Path => _path;
        public List<GameObject> AllPrefabs => _allPrefabs;

        public PrefabStorage(string path)
        {
            _path = path;
            _allPrefabs = new List<GameObject>();
        }
    }
}