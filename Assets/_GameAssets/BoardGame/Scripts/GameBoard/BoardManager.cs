using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    
        private GameObject[] _playerPieces;
        private GameObject[] _enemyPieces;
        private Vector2[,] _gridID;
        private Vector2[,] _gridPosition;
        private Vector2[,] _playerPiecesGridPositions;

        public GameObject[] PlayerPieces => _playerPieces;
        public Vector2[,] GridPositions => _gridID;
        public Vector2[,] GridPosition => _gridPosition;
        public Vector2[,] PlayerPiecesGridPosition => _playerPiecesGridPositions;

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

            _isOccupied = new bool[_gridID.GetLength(0), _gridID.GetLength(1)];
            SetOccupied();
        }
    

        public void GetCurrentButton(Button button)
        {
            _currentButton = button;

            for (int i = 0; i < _playerPieces.Length; i ++)
            {
                if (_currentButton.name == _playerPieces[i].name)
                {
                    ButtonScript script = _currentButton.gameObject.GetComponent<ButtonScript>();

                    if (script != null)
                    {
                    
                    }
                }
            }
        }

        private void SetOccupied()
        {
            foreach (GameObject pieces in _playerPieces)
            {
                ButtonScript script = pieces.GetComponent<ButtonScript>();

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

        public bool MoveLocationCheck(Button button, Vector2 moveToPosition)
        {
            ButtonScript script = button.gameObject.GetComponent<ButtonScript>();

            if (script != null)
            {
                for (int i = 0; i < _gridPosition.GetLength(0); i++)
                {
                    for (int j = 0; j < _gridPosition.GetLength(1); j++)
                    {
                        if (_gridPosition[j, i] == moveToPosition)
                        {
                            Debug.Log(_isOccupied[j, i]);
                            if (_isOccupied[j, i])
                            {
                                SetOccupied();
                                return true;
                            }
                            else if (!_isOccupied[j, i])
                            {
                                _isOccupied[(int)script.GridID.x, (int)script.GridID.y] = false;
                                script.GridID = _gridID[j, i];
                                _isOccupied[j, i] = true;
                                SetOccupied();
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
