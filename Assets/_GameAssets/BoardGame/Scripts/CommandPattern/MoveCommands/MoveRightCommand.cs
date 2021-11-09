using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveRightCommand : ICommand
{
    GamePiece _moveablePiece;
    public MoveRightCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveRight();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
