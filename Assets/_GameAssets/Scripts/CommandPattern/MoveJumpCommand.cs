using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveJumpCommand : ICommand
{
    PlayerGamePiece _moveablePiece;

    public MoveJumpCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<PlayerGamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveJump();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
