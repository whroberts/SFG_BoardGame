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

        [SerializeField] TMP_Text _chosenPieceTitle = null;

        private Button _currentButton = null;
        public Button CurrentButton => _currentButton;

        private int BoardSizeX;
        private int BoardSizeY;
        private int Colors;
        private int Shapes;

        private bool[,] _isOccupied;
        public bool[,] IsOccupied => _isOccupied;

        private GameObject[] _playerPieces;
        private GameObject[] _enemyPieces;
        private Vector2[,] _gridID;
        private Vector2[,] _gridPosition;
        private Vector2[,] _playerPiecesGridPositions;
        private Color[,] _playerPiecesColor;
        private String[,] _playerPiecesShape;

        public GameObject[] EnemyPieces => _enemyPieces;

        public GameObject[] PlayerPieces => _playerPieces;
        public Vector2[,] GridPositions => _gridID;
        public Vector2[,] GridPosition => _gridPosition;
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
            _playerPieces = GenerateBoard.PlayerPieces;
            _enemyPieces = GenerateBoard.EnemyPieces;
            _playerPiecesColor = GenerateBoard.PlayerPiecesColor;
            _playerPiecesShape = GenerateBoard.PlayerPiecesShape;

            _isOccupied = new bool[_gridID.GetLength(0), _gridID.GetLength(1)];
        }

        public void SetCurrentButton(Button button)
        {
            _currentButton = button;
            SetOccupied();
            if (button != null)
            {
                _chosenPieceTitle.text = "Chosen Piece: \n" + button.name.ToString();
            }
            else
            {
                _chosenPieceTitle.text = "Chosen Piece: \n";
            }
        }

        public void SetOccupied()
        {
            foreach (GameObject pieces in _playerPieces)
            {
                PlayerGamePiece script = pieces.GetComponent<PlayerGamePiece>();

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

        public bool MovementValid(Button button, Vector2 moveToPosition)
        {
            PlayerGamePiece script = button.gameObject.GetComponent<PlayerGamePiece>();

            for (int i = 0; i < _gridPosition.GetLength(0); i++)
            {
                for (int j = 0; j < _gridPosition.GetLength(1); j++)
                {
                    if (_gridPosition[j, i] == moveToPosition)
                    {
                        if (_playerPiecesColor[j,i-1] != _playerPiecesColor[j,i])
                        {
                            Debug.Log("Cannot jump this piece");
                        }
                        else
                        {
                            Debug.Log("Test Color: " + _playerPiecesColor[j, i - 1]);
                            Debug.Log("Test Color: " + _playerPiecesColor[j, i]);
                            Debug.Log("Can jump this piece");
                        }
                    }
                }
            }
            return false;
        }

        public bool IsOccupiedCheck(PlayerGamePiece piece, Vector2 moveToPosition)
        {
            if (piece != null)
            {
                for (int i = 0; i < _gridPosition.GetLength(0); i++)
                {
                    for (int j = 0; j < _gridPosition.GetLength(1); j++)
                    {
                        if (_gridPosition[j, i] == moveToPosition)
                        {
                            if (_isOccupied[j, i])
                            {
                                SetOccupied();
                                return true;
                            }
                            else if (!_isOccupied[j, i])
                            {
                                _isOccupied[(int)piece.GridID.x, (int)piece.GridID.y] = false;
                                piece.GridID = _gridID[j, i];
                                _isOccupied[j, i] = true;
                                SetOccupied();
                            }
                        }
                    }
                }
            }
            return false;
        }

        public IEnumerator MovePiece(PlayerGamePiece piece, Vector2 moveToPosition, Vector2 savedPosition)
        {
            //Debug.Log(MovementValid(button, moveToPosition));

            yield return new WaitForSeconds(0.1f);

            if (!IsOccupiedCheck(piece, moveToPosition))
            {
                piece.gameObject.transform.position = moveToPosition;
                piece._moved = true;
            }
            else
            {
                piece.gameObject.transform.position = savedPosition;
                piece._moved = false;
            }

            yield return new WaitForSeconds(0.1f);
        }

        public void MovePieceBack(PlayerGamePiece piece, Vector2 moveToPosition, Vector2 savedPosition)
        {
            if (!IsOccupiedCheck(piece, savedPosition))
            {
                piece.gameObject.transform.position = savedPosition;
            }
            else
            {
                piece.gameObject.transform.position = moveToPosition;
            }

            piece._moved = false;
        }
    }
}
