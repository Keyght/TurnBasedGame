using UnityEngine.UI;
using UnityEngine;

public class ManagerOfTurns
{
    private Button _endTurnButton;
    private int _turn;
    private bool _isPlayerTurn
    {
        get
        {
            return _turn % 2 == 0;
        }
    }

    public ManagerOfTurns(Button button)
    {
        this._endTurnButton = button;
        this._turn = 0;
    }

    public void ChangeTurn()
    {
        _turn++;

        _endTurnButton.interactable = _isPlayerTurn;

    }

    private void EnemyTurn()
    {

    }
}
