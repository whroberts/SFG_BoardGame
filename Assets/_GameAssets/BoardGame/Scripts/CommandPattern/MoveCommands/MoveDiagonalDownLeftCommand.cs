using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDiagonalDownLeftCommand : ICommand
{
    GamePiece _moveablePiece;

    public MoveDiagonalDownLeftCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveDiagonalDownLeft();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
