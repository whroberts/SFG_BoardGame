using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDiagonalLeftCommand : ICommand
{
    PlayerGamePiece _moveablePiece;

    public MoveDiagonalLeftCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<PlayerGamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveDiagonalLeft();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
