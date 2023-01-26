using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerOfGame : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefabs;
    [SerializeField]
    private Button _endTurnButton;

    private Game _currentGame;
    private int _turn;
    private bool _isPlayerTurn
    {
        get
        {
            return _turn % 2 == 0;
        }
    }

    private void Awake()
    {
        _currentGame = new Game();
    }

    private void Start()
    {
        _turn = 0;

        InititalizeSides(_currentGame.Allies, 3, true);
        InititalizeSides(_currentGame.Enemies, 4, false);
    }

    private void InititalizeSides(List<GameObject> list, int count, bool isAllies)
    {
        int side = isAllies ? 1 : -1; 
        for (int i = 0; i < count; i++)
        {
            var currChar = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], new Vector3(-4f + i * 3, 0, -5 * side), Quaternion.identity);
            list.Add(currChar);
        }
    }

    public void ChangeTurn()
    {
        _turn++;

        _endTurnButton.interactable = _isPlayerTurn;
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