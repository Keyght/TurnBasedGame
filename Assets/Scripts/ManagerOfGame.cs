using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerOfGame : MonoBehaviour
{
    [SerializeField]
    private Button _endTurnButton;

    private List<GameObject> _prefabs;
    private Game _currentGame;
    ManagerOfTurns _managerOfTurns;

    private void Awake()
    {
        ManagerOfCharacters.FillCharManager();
        _currentGame = new Game();
        _managerOfTurns = new ManagerOfTurns(_endTurnButton);
    }

    private void Start()
    {
        _prefabs = CharManager.AllChars;
        InstantCharacters(_currentGame.Allies, 2, true);
        InstantCharacters(_currentGame.Enemies, 3, false);
    }

    private void InstantCharacters(List<GameObject> list, int count, bool isAllies)
    {
        int side = isAllies ? 1 : -1; 
        for (int i = 0; i < count; i++)
        {
            var currChar = Instantiate(_prefabs[Random.Range(0, _prefabs.Count)], new Vector3(-4f + i * 3, 0, -5 * side), Quaternion.identity);
            list.Add(currChar);
        }
    }

    public void OnEndButtonClick()
    {
        _managerOfTurns.ChangeTurn();
    }

}

public class Game
{
    public List<GameObject> Allies, Enemies;

    public Game()
    {
        Allies = new List<GameObject>();
        Enemies = new List<GameObject>();
    }
}