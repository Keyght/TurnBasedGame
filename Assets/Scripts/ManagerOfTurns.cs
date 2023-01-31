using UnityEngine.UI;
using UnityEngine;

public class ManagerOfTurns
{
    private Button _endTurnButton;
    private int _turn;
    

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
    private bool _isPlayerTurn
    {
        get
        {
            return _turn % 2 == 0;
        }
    }

    private void EnemyTurn()
    {

    }
}
