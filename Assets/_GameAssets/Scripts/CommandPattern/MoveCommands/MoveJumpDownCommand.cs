using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveJumpDownCommand : ICommand
{
    GamePiece _moveablePiece;

    public MoveJumpDownCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveJumpDown();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
