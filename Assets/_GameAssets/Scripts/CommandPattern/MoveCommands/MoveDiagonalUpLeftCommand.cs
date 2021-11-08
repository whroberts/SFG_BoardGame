using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDiagonalUpLeftCommand : ICommand
{
    GamePiece _moveablePiece;

    public MoveDiagonalUpLeftCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveDiagonalUpLeft();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
