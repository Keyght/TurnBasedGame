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
    private ManagerOfTurns _managerOfTurns;

    private void Awake()
    {
        _instance = this;
        _managerOfTurns = GetComponent<ManagerOfTurns>();

        CharManager.Init();
        ActionManager.Init();
        _charPrefabs = CharManager.PrefabManager.AllPrefabs;

        _currentGame = new Game();
        InitializeGame();
    }

    private void InitializeGame()
    {
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyCurrentChars(_currentGame.Allies);
            DestroyCurrentChars(_currentGame.Enemies);
            InitializeGame();
            _managerOfTurns.RestartTurns();
        }
    }

    private void DestroyCurrentChars(List<GameObject> list)
    {
        foreach (var character in list)
        {
            Destroy(character);
        }
        list.Clear();
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
