using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveJumpUpCommand : ICommand
{
    GamePiece _moveablePiece;

    public MoveJumpUpCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveJumpUp();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
