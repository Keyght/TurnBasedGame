using System.Collections.Generic;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    /// <summary>
    /// Класс для определения союзников и противников
    /// </summary>
    public class Game
    {
        public List<GameObject> Allies, Enemies;

        public Game()
        {
            Allies = new List<GameObject>();
            Enemies = new List<GameObject>();
        }
    }

    /// <summary>
    /// Класс для управления ходом игры
    /// </summary>
    public class ManagerOfGame : MonoBehaviour
    {
        [SerializeField]
        private GameObject _endCanvases;
        [SerializeField]
        private TextMeshProUGUI _winLoseText;
        [SerializeField]
        private Image _winLoseColor;
        [SerializeField]
        private int _numberOfCharactersOnSide;

        private List<GameObject> _charPrefabs;
        private Game _currentGame;
        private ManagerOfTurns _managerOfTurns;

        private void Awake()
        {
            CharStorage.Init();
            ActionStorage.Init();
            _charPrefabs = CharStorage.PrefabStorage.AllPrefabs;

            _currentGame = new Game();
            _managerOfTurns = GetComponent<ManagerOfTurns>();
            _managerOfTurns.SetGame(_currentGame);

            InitializeGame();
        }

        private void InitializeGame()
        {
            InstantCharacters(_currentGame.Allies, _numberOfCharactersOnSide, true);
            InstantCharacters(_currentGame.Enemies, _numberOfCharactersOnSide, false);
        }

        private void InstantCharacters(List<GameObject> list, int count, bool isAllies)
        {
            int side = isAllies ? 1 : -1;
            for (int i = 0; i < count; i++)
            {
                var currChar = Instantiate(_charPrefabs[Random.Range(0, _charPrefabs.Count)], new Vector3(-4f + i * 3, 0, -5 * side), Quaternion.identity);
                currChar.GetComponent<Character>().OnDeath += OnCharacterDeath;
                list.Add(currChar);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _endCanvases.SetActive(false);
                DestroyCurrentChars(_currentGame.Allies);
                DestroyCurrentChars(_currentGame.Enemies);
                InitializeGame();
                _managerOfTurns.RestartTurns();
            }
        }

        private void OnCharacterDeath(bool isDead)
        {
            if (isDead)
            {
                if (CheckForDeaths(_currentGame.Allies))
                {
                    _endCanvases.SetActive(true);
                    _winLoseText.text = "You Lose!";
                    _winLoseColor.color = Color.red;
                }
                else if (CheckForDeaths(_currentGame.Enemies))
                {
                    _endCanvases.SetActive(true);
                    _winLoseText.text = "You Win!";
                    _winLoseColor.color = Color.green;
                }
            }
        }

        private static bool CheckForDeaths(List<GameObject> persons)
        {
            int checker = 1;
            foreach (var pers in persons)
            {
                if (pers.GetComponent<Character>().IsDead)
                {
                    checker *= 1;
                }
                else
                {
                    checker *= 0;
                }
            }
            return checker == 1;
        }

        private void DestroyCurrentChars(List<GameObject> list)
        {
            foreach (var character in list)
            {
                character.GetComponent<Character>().OnDeath -= OnCharacterDeath;
                Destroy(character);
            }
            list.Clear();
        }
    }
}