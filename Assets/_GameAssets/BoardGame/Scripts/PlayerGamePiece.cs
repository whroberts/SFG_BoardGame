using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BoardGame;

public class PlayerGamePiece : MonoBehaviour, IMoveable
{
    [SerializeField] public StateMachine StateMachine = null;
    [SerializeField] public BoardManager BoardManager = null;
    [SerializeField] public Vector2 GridID = new Vector2(0, 0);
    [SerializeField] public Color Color = new Color(0, 0, 0, 0);
    [SerializeField] public string Shape = "";

    [HideInInspector] public bool _moved = false;

    Vector2 _savedPosition = new Vector2(0, 0);
    Vector2 _moveToPosition = new Vector2(0, 0);

    void MovePiece(int increaseX, int increaseY)
    {
        if (!_moved)
        {
            _moveToPosition = new Vector2(transform.position.x + increaseX, transform.position.y + increaseY);
            _savedPosition = transform.position;

            transform.position = _moveToPosition;
            StartCoroutine(BoardManager.MovePiece(this, _moveToPosition, _savedPosition));
        }
    }

    public void MoveUp()
    {
        MovePiece(0, 100);
    }

    public void MoveDiagonalLeft()
    {
        MovePiece(-100, 100);
    }

    public void MoveDiagonalRight()
    {
        MovePiece(100, 100);
    }

    public void MoveJump()
    {
        MovePiece(0, 200);
    }

    public void UndoMove()
    {
        if (_moved)
        {
            BoardManager.MovePieceBack(this, _moveToPosition, _savedPosition);
            _moved = false;
        }
    }
}
