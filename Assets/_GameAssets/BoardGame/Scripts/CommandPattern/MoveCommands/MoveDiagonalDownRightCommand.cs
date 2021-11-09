using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDiagonalDownRightCommand : ICommand
{
    GamePiece _moveablePiece;

    public MoveDiagonalDownRightCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveDiagonalDownRight();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
