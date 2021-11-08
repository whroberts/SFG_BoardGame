using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDiagonalRightCommand : ICommand
{
    PlayerGamePiece _moveablePiece;

    public MoveDiagonalRightCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<PlayerGamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveDiagonalRight();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
