using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveDownCommand : ICommand
{
    GamePiece _moveablePiece;
    public MoveDownCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<GamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveDown();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
