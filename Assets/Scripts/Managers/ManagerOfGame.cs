using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game
{
    public List<GameObject> Allies, Enemies;

    public Game()
    {
        Allies = new List<GameObject>();
        Enemies = new List<GameObject>();
    }
}

public class ManagerOfGame : MonoBehaviour
{
    private static ManagerOfGame _instance;
    private List<GameObject> _charPrefabs;
    private Game _currentGame;

    private void Awake()
    {
        _instance = this;

        CharManager.Init();
        ActionManager.Init();

        _currentGame = new Game();

        _charPrefabs = CharManager.PrefabManager.AllPrefabs;
        InstantCharacters(_currentGame.Allies, 2, true);
        InstantCharacters(_currentGame.Enemies, 3, false);
    }

    private void InstantCharacters(List<GameObject> list, int count, bool isAllies)
    {
        int side = isAllies ? 1 : -1; 
        for (int i = 0; i < count; i++)
        {
            var currChar = Instantiate(_charPrefabs[Random.Range(0, _charPrefabs.Count)], new Vector3(-4f + i * 3, 0, -5 * side), Quaternion.identity);
            list.Add(currChar);
        }
    }

    public static ManagerOfGame GetInstance()
    {
        return _instance;
    }

    public Game GetCurrentGame()
    {
        return _currentGame;
    }
}
