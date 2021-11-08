using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace BoardGame
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] SetupStateGenerateBoard GenerateBoard = null;
        [SerializeField] SetupBoardGameBaseState SetupBoard = null;

        private Button _currentButton = null;
        public Button CurrentButton => _currentButton;

        private int BoardSizeX;
        private int BoardSizeY;
        private int Colors;
        private int Shapes;

        private bool[,] _isOccupied;
        public bool[,] IsOccupied => _isOccupied;

        private GameObject[] _enemyPieces;
        private Vector2[,] _gridID;
        private Vector2[,] _gridPosition;

        private GameObject[] _playerPieces;
        private GameObject[,] _playerPiecesOnGrid;
        private Vector2[,] _playerPiecesGridPositions;
        private Color[,] _playerPiecesColor;
        private String[,] _playerPiecesShape;

        public GameObject[] EnemyPieces => _enemyPieces;

        public Vector2[,] GridID => _gridID;
        public Vector2[,] GridPosition => _gridPosition;
        public GameObject[,] PlayerPiecesOnGrid => _playerPiecesOnGrid;

        public GameObject[] PlayerPieces => _playerPieces;
        public Vector2[,] PlayerPiecesGridPosition => _playerPiecesGridPositions;
        public Color[,] PlayerPiecesColor => _playerPiecesColor;
        public String[,] PlayerPiecesShape => _playerPiecesShape;

        private void OnEnable()
        {
            SetupStateGenerateBoard.BoardData += GetData;
        }

        private void GetData()
        {
            BoardSizeX = SetupBoard.BoardSizeX;
            BoardSizeY = SetupBoard.BoardSizeY;
            Colors = SetupBoard.Colors;
            Shapes = SetupBoard.Shapes;

            _gridID = GenerateBoard.GridID;
            _gridPosition = GenerateBoard.GridPosition;
            _playerPiecesGridPositions = GenerateBoard.PlayerPiecesGridPosition;
            _playerPiecesOnGrid = GenerateBoard.PlayerPiecesOnGrid;
            _playerPieces = GenerateBoard.PlayerPieces;
            _enemyPieces = GenerateBoard.EnemyPieces;
            _playerPiecesColor = GenerateBoard.PlayerPiecesColor;
            _playerPiecesShape = GenerateBoard.PlayerPiecesShape;

            _isOccupied = new bool[_gridID.GetLength(0), _gridID.GetLength(1)];
        }

        public void SetCurrentButton(Button button)
        {
            _currentButton = button;
            SetBoard();
        }

        public void SetBoard()
        {
            foreach (GameObject pieces in _playerPieces)
            {
                GamePiece script = pieces.GetComponent<GamePiece>();

                if (script != null)
                {
                    for (int i = 0; i < _gridID.GetLength(0); i++)
                    {
                        for (int j = 0; j < _gridID.GetLength(1); j++)
                        {
                            if (_gridID[j, i] == script.GridID)
                            {
                                _isOccupied[j, i] = true;
                            }
                        }
                    }
                }
            }
        }

        public bool IsOccupiedCheck(GamePiece piece, Vector2 newGridID)
        {
            if (piece != null)
            {
                if (_isOccupied[(int)newGridID.x, (int)newGridID.y])
                {
                    SetBoard();
                    return true;
                }
                else
                {
                    _isOccupied[(int)piece.GridID.x, (int)piece.GridID.y] = false;
                    piece.GridID = _gridID[(int)newGridID.x, (int)newGridID.y];
                    _isOccupied[(int)newGridID.x, (int)newGridID.y] = true;
                    SetBoard();
                }
            }
            return false;
        }

        public bool JumpCheck(GamePiece piece, Vector2 jumpedGridID)
        {
            if (piece != null)
            {
                if (_isOccupied[(int)jumpedGridID.x, (int)jumpedGridID.y])
                {
                    GameObject jumpedPiece = _playerPiecesOnGrid[(int)jumpedGridID.x, (int)jumpedGridID.y];
                    GamePiece jumpedScript = jumpedPiece.GetComponent<GamePiece>();

                    Debug.Log(jumpedScript.Shape);

                    if (jumpedScript.Shape == piece.Shape || jumpedScript.Color == piece.Color)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public IEnumerator MovePiece(GamePiece piece, Vector2 newGridID, Vector2 savedGridID)
        {
            yield return new WaitForSeconds(0.1f);

            if (!IsOccupiedCheck(piece, newGridID))
            {
                piece.gameObject.transform.position = _gridPosition[(int)newGridID.x, (int)newGridID.y];
                piece._moved = true;
                _playerPiecesOnGrid[(int)newGridID.x, (int)newGridID.y] = piece.gameObject;
                _playerPiecesOnGrid[(int)savedGridID.x, (int)savedGridID.y] = null;
            }
            else
            {
                piece.gameObject.transform.position = _gridPosition[(int)savedGridID.x, (int)savedGridID.y];
                piece._moved = false;
            }
            yield return new WaitForSeconds(0.1f);
        }

        public void MovePieceBack(GamePiece piece, Vector2 newGridID, Vector2 savedGridID)
        {

            if (!IsOccupiedCheck(piece, savedGridID))
            {
                piece.gameObject.transform.position = _gridPosition[(int)savedGridID.x, (int)savedGridID.y];
            }
            else
            {
                piece.gameObject.transform.position = _gridPosition[(int)newGridID.x, (int)newGridID.y];
            }

            piece._moved = false;
        }
    }
}
