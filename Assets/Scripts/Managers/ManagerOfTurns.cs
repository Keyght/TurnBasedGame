using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Turn
{
    public List<GameObject> AllyActions, EnemyActions;

    public Turn()
    {
        AllyActions = new List<GameObject>();
        EnemyActions = new List<GameObject>();
    }
}

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
    

    private bool _isPlayerTurn
    {
        get
        {
            return _turn % 2 == 0;
        }
    }

    private void Awake()
    {
        _currentTurn = new Turn();
    }

    private void Start()
    {
        _turn = -1;
        _actionPrefabs = ActionManager.PrefabManager.AllPrefabs;
        _attackActionPrefabs = GetAttackingPrefabs();

        //StartCoroutine(TurnFunction());
        ChangeTurn();
    }

    private IEnumerator TurnFunction()
    {
        if (_isPlayerTurn)
        {
            yield return new WaitForSeconds(1);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
        ChangeTurn();
    }

    public void ChangeTurn()
    {
        //StopAllCoroutines();
        _turn++;

        _endTurnButton.interactable = _isPlayerTurn;

        if (_isPlayerTurn)
        {
            var currentGame = ManagerOfGame.GetInstance().GetCurrentGame();
            CreateActions(currentGame.Allies, _currentTurn.AllyActions, true);
            CreateActions(currentGame.Enemies, _currentTurn.EnemyActions, false);
        }
        //StartCoroutine(TurnFunction());
    }

    private void CreateActions(List<GameObject> charList, List<GameObject> actionList, bool isAlly)
    {
        foreach (var obj in charList)
        {
            var actionPoint = obj.GetComponent<Character>().GetActionPoint();
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

    private void EnemyTurn()
    {

    }

    private List<GameObject> GetAttackingPrefabs()
    {
        List<GameObject> attackingActions = new List<GameObject>();
        foreach (var pref in _actionPrefabs)
        {
            if (pref.TryGetComponent(out Damaging damaging))
            {
                attackingActions.Add(damaging.gameObject);
            }
        }
        return attackingActions;
    }

    public void OnEndButtonClick()
    {
        ChangeTurn();
    }
}
