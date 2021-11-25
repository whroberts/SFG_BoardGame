using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveJumpDiagonalRightCommand : ICommand
{
    GamePiece _moveablePiece;

    public MoveJumpDiagonalRightCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveJumpDiagonalUpRight();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
