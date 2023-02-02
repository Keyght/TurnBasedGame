using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Класс для определения действий союзников и противников
/// </summary>
public class Turn
{
    public List<GameObject> AllyActions, EnemyActions;

    public Turn()
    {
        AllyActions = new List<GameObject>();
        EnemyActions = new List<GameObject>();
    }
}

/// <summary>
/// Класс для управления ходами сторон
/// </summary>
public class ManagerOfTurns : MonoBehaviour
{
    [SerializeField]
    private Button _endTurnButton;
    [SerializeField]
    private Material _fadedMaterial;

    private int _turn;
    private Turn _currentTurn;
    private List<GameObject> _actionPrefabs;
    private List<GameObject> _attackActionPrefabs;
    private Game _currentGame;

    private bool _isPlayerTurn
    {
        get
        {
            return _turn % 2 != 0;
        }
    }

    private void Awake()
    {
        _currentTurn = new Turn();

    }

    private void Start()
    {
        _turn = 0;
        _actionPrefabs = ActionManager.PrefabManager.AllPrefabs;
        _attackActionPrefabs = GetAttackingPrefabs();

        ChangeTurn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_endTurnButton.interactable) _endTurnButton.onClick.Invoke();
        }
    }

    public void RestartTurns()
    {
        _turn = 0;
        ClearActions();
        ChangeTurn();
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        _turn++;

        _endTurnButton.interactable = _isPlayerTurn;

        if (_isPlayerTurn)
        {
            ClearActions();
            TickEffects(_currentGame.Enemies);
            CreateActions(_currentGame.Allies, _currentTurn.AllyActions, true);
        }
        else
        {
            ClearActions();
            TickEffects(_currentGame.Allies);
            CreateActions(_currentGame.Enemies, _currentTurn.EnemyActions, false);
        }
    }

    private void TickEffects(List<GameObject> persons)
    {
        foreach (var pers in persons)
        {
            EffectHandler.HandleEffect(pers.GetComponent<Character>());
        }
    }

    private void CreateActions(List<GameObject> charList, List<GameObject> actionList, bool isAlly)
    {
        foreach (var obj in charList)
        {
            var character = obj.GetComponent<Character>();
            if (!character.IsDead)
            {
                var actionPoint = character.GetActionPoint();
                if (isAlly)
                {
                    var currAction = Instantiate(_actionPrefabs[Random.Range(0, _actionPrefabs.Count)], actionPoint.position, Quaternion.identity);
                    currAction.transform.parent = obj.transform;
                    actionList.Add(currAction);
                }
                else
                {
                    var currEnemyAction = Instantiate(_attackActionPrefabs[Random.Range(0, _attackActionPrefabs.Count)], actionPoint.position, actionPoint.rotation);
                    currEnemyAction.transform.parent = obj.transform;
                    currEnemyAction.GetComponent<Renderer>().material = _fadedMaterial;
                    actionList.Add(currEnemyAction);
                }
            }
        }
    }

    private IEnumerator EnemyTurn(float sec)
    {
        yield return new WaitForSeconds(sec);

        foreach (var act in _currentTurn.EnemyActions)
        {
            var target = DefineTargetForEnemy();
            if (target != null) target.PerformingAction(act.GetComponent<Action>(), act);
        }

        ChangeTurn();
    }

    private Character DefineTargetForEnemy()
    {
        foreach (var pers in _currentGame.Allies)
        {
            var character = pers.GetComponent<Character>();
            if (character.IsDead)
            {
                continue;
            }
            else
            {
                return character;
            }
        }
        return null;
    }

    private List<GameObject> GetAttackingPrefabs()
    {
        List<GameObject> attackingActions = new List<GameObject>();
        foreach (var pref in _actionPrefabs)
        {
            if (pref.GetComponent<Action>().Flag == Flag.ATTACKING)
            {
                attackingActions.Add(pref);
            }
        }
        return attackingActions;
    }

    private void ClearActions()
    {
        foreach (var act in _currentTurn.AllyActions)
        {
            Destroy(act);
        }
        _currentTurn.AllyActions.Clear();
        foreach (var act in _currentTurn.EnemyActions)
        {
            Destroy(act);
        }
        _currentTurn.EnemyActions.Clear();
    }

    public void OnEndButtonClick()
    {
        ChangeTurn();
        StartCoroutine(EnemyTurn(5));
    }

    public void SetGame(Game game)
    {
        _currentGame = game;
    }
}
