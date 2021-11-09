using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveLeftCommand : ICommand
{
    GamePiece _moveablePiece;
    public MoveLeftCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveLeft();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
