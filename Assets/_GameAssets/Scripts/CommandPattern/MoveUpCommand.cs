using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveUpCommand : ICommand
{
    PlayerGamePiece _moveablePiece;
    public MoveUpCommand(Button currentButton)
    {
        _moveablePiece = currentButton.GetComponent<PlayerGamePiece>();
    }

    public void Execute()
    {
        _moveablePiece.MoveUp();
    }

    public void UndoMove()
    {
        _moveablePiece.UndoMove();
    }
}
