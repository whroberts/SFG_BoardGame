using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveJumpDiagonalLeftCommand : ICommand
{
    GamePiece _moveablePiece;

    public MoveJumpDiagonalLeftCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveJumpDiagonalUpLeft();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
