using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDiagonalUpRightCommand : ICommand
{
    GamePiece _moveablePiece;

    public MoveDiagonalUpRightCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveDiagonalUpRight();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
