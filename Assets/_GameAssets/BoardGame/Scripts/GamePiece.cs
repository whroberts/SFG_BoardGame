using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BoardGame;

public class GamePiece : MonoBehaviour, IMoveable
{
    [SerializeField] public StateMachine StateMachine = null;
    [SerializeField] public BoardManager BoardManager = null;
    [SerializeField] public Vector2 GridID = new Vector2(0, 0);
    [SerializeField] public Color Color = new Color(0, 0, 0, 0);
    [SerializeField] public string Shape = "";

    [HideInInspector] public bool _moved = false;

    [HideInInspector] public Vector2 _savedGridID = new Vector2(0, 0);
    [HideInInspector] public Vector2 _newGridID = new Vector2(0, 0);

    void MovePiece(int newLocX, int newLocY)
    {
        if (!_moved)
        {
            _newGridID = new Vector2(GridID.x + newLocX, GridID.y + newLocY);
            _savedGridID = GridID;
            StartCoroutine(BoardManager.MovePiece(this, _newGridID, _savedGridID));
        }
    }

    public void MoveUp()
    {

        MovePiece(0, -1);
    }

    public void MoveDown()
    {
        MovePiece(0, 1);
    }

    public void MoveDiagonalUpLeft()
    {
        MovePiece(-1, -1);
    }

    public void MoveDiagonalUpRight()
    {
        MovePiece(1, -1);
    }

    public void MoveDiagonalDownLeft()
    {
        MovePiece(-1, 1);
    }

    public void MoveDiagonalDownRight()
    {
        MovePiece(1, 1);
    }

    public void MoveJumpUp()
    {
        if (BoardManager.JumpCheck(this, new Vector2(GridID.x, GridID.y-1)))
        {
            MovePiece(0, -2);
        }
    }

    public void MoveJumpDown()
    {
        MovePiece(0, 2);
    }

    public void MoveLeft()
    {
        MovePiece(-1, 0);
    }

    public void MoveRight()
    {
        MovePiece(1, 0);
    }

    public void UndoMove()
    {
        if (_moved)
        {
            BoardManager.MovePieceBack(this, _newGridID, _savedGridID);
            _moved = false;
        }
    }
}
