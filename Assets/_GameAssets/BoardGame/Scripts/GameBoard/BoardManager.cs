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

        private Button _playerCurrentButton = null;
        private Button _enemyCurrentButton = null;
        public Button EnemyCurrentButton => _enemyCurrentButton;
        public Button PlayerCurrentButton => _playerCurrentButton;

        private int BoardSizeX;
        private int BoardSizeY;
        private int Colors;
        private int Shapes;

        private bool[,] _isOccupied;
        public bool[,] IsOccupied => _isOccupied;


        private Vector2[,] _gridID;
        private Vector2[,] _gridPosition;

        private GameObject[,] _allPiecesOnBoard;
        //private GameObject[] _allPieces;

        List<GameObject> _allPiecesList = new List<GameObject>();

        private GameObject[] _playerPieces;
        private GameObject[,] _playerPiecesOnGrid;
        private Vector2[,] _playerPiecesGridPositions;
        private Color[,] _playerPiecesColor;
        private String[,] _playerPiecesShape;

        private GameObject[] _enemyPieces;
        public GameObject[,] _enemyPiecesOnGrid;
        public Vector2[,] _enemyPiecesGridPositions;
        public Color[,] _enemyPiecesColor;
        public String[,] _enemyPiecesShape;

        public Vector2[,] GridID => _gridID;
        public Vector2[,] GridPosition => _gridPosition;

        public GameObject[,] AllPiecesOnBoard => _allPiecesOnBoard;
        //public GameObject[] AllPieces => _allPieces;

        public GameObject[,] PlayerPiecesOnGrid => _playerPiecesOnGrid;

        public GameObject[] PlayerPieces => _playerPieces;
        public Vector2[,] PlayerPiecesGridPosition => _playerPiecesGridPositions;
        public Color[,] PlayerPiecesColor => _playerPiecesColor;
        public String[,] PlayerPiecesShape => _playerPiecesShape;

        public GameObject[] EnemyPieces => _enemyPieces;
        public GameObject[,] EnemyPiecesOnGrid => _enemyPiecesOnGrid;
        public Vector2[,] EnemyPiecesGridPositions => _enemyPiecesGridPositions;
        public Color[,] EnemyPiecesColor => _enemyPiecesColor;
        public String[,] EnemyPiecesShape => _enemyPiecesShape;

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

            //_allPieces = GenerateBoard.AllPieces;
            _allPiecesList = GenerateBoard.AllPiecesList;

            _playerPiecesGridPositions = GenerateBoard.PlayerPiecesGridPosition;
            _playerPiecesOnGrid = GenerateBoard.PlayerPiecesOnGrid;
            _playerPieces = GenerateBoard.PlayerPieces;
            _playerPiecesColor = GenerateBoard.PlayerPiecesColor;
            _playerPiecesShape = GenerateBoard.PlayerPiecesShape;
            _enemyPieces = GenerateBoard.EnemyPieces;
            _enemyPiecesOnGrid = GenerateBoard.EnemyPiecesOnGrid;
            _enemyPiecesGridPositions = GenerateBoard.EnemyPiecesGridPositions;
            _enemyPiecesColor = GenerateBoard.EnemyPiecesColor;
            _enemyPiecesShape = GenerateBoard.EnemyPiecesShape;

            _isOccupied = new bool[_gridID.GetLength(0), _gridID.GetLength(1)];
        }

        public void SetCurrentButton(Button button)
        {
            _playerCurrentButton = button;
            _enemyCurrentButton = button;
            SetBoard();
        }

        public void SetBoard()
        {
            foreach (GameObject pieces in _allPiecesList)
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

            _allPiecesOnBoard = new GameObject[BoardSizeX, BoardSizeY];

            foreach (GameObject piece in _allPiecesList)
            {
                GamePiece script = piece.GetComponent<GamePiece>();

                if (script != null)
                {
                    _allPiecesOnBoard[(int)script.GridID.x, (int)script.GridID.y] = piece;
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
                    GameObject jumpedPiece = _allPiecesOnBoard[(int)jumpedGridID.x, (int)jumpedGridID.y];
                    GamePiece jumpedScript = jumpedPiece.GetComponent<GamePiece>();


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

        public IEnumerator MovePiece(GamePiece piece, Vector2 gridMovement, Vector2 savedGridID)
        {
            yield return new WaitForSeconds(0.1f);

            Vector2 newGridID = new Vector2(piece.GridID.x + gridMovement.x, piece.GridID.y + gridMovement.y);
            Vector2 jumpGridMovement = gridMovement * 2;

            if (!IsOccupiedCheck(piece, newGridID))
            {
                piece.gameObject.transform.position = _gridPosition[(int)newGridID.x, (int)newGridID.y];
                piece._moved = true;
                piece._cantMove = false;
                _allPiecesOnBoard[(int)newGridID.x, (int)newGridID.y] = piece.gameObject;
                _allPiecesOnBoard[(int)savedGridID.x, (int)savedGridID.y] = null;
                Debug.Log("Succeeded On Move");
            }
            else
            {
                Debug.Log("Failed On Move");
                Vector2 jumpGridID = new Vector2(piece.GridID.x + jumpGridMovement.x, piece.GridID.y + jumpGridMovement.y);

                if (!IsOccupiedCheck(piece, jumpGridID))
                {
                    if (JumpCheck(piece, newGridID))
                    {
                        piece.gameObject.transform.position = _gridPosition[(int)jumpGridID.x, (int)jumpGridID.y];
                        piece._moved = true;
                        piece._cantMove = false;
                        _allPiecesOnBoard[(int)jumpGridID.x, (int)jumpGridID.y] = piece.gameObject;
                        _allPiecesOnBoard[(int)savedGridID.x, (int)savedGridID.y] = null;
                        Debug.Log("Succeeded On Jump Retry");
                    }
                    else
                    {
                        piece.gameObject.transform.position = _gridPosition[(int)savedGridID.x, (int)savedGridID.y];
                        piece._moved = false;
                        piece._cantMove = true;
                        piece.GridID = piece._savedGridID;
                        Debug.Log("Failed On Jump Retry");
                    }
                }
                else
                {
                    piece.gameObject.transform.position = _gridPosition[(int)savedGridID.x, (int)savedGridID.y];
                    piece._moved = false;
                    piece._cantMove = true;
                    piece.GridID = piece._savedGridID;
                    Debug.Log("Failed On Jump Retry");
                }
                
            }
            Attacking(piece);
            yield return new WaitForSeconds(0.1f);
        }

        public void MovePieceBack(GamePiece piece, Vector2 newGridID, Vector2 savedGridID)
        {

            if (!IsOccupiedCheck(piece, savedGridID))
            {
                piece.gameObject.transform.position = _gridPosition[(int)savedGridID.x, (int)savedGridID.y];
                piece._cantMove = false;
            }
            else
            {
                piece.gameObject.transform.position = _gridPosition[(int)newGridID.x, (int)newGridID.y];
                piece._cantMove = true;
            }
            piece._moved = false;
        }

        public void Attacking(GamePiece piece)
        {
            for (int i = -1 + (int)piece.GridID.y; i <= 1 + (int)piece.GridID.y; i++)
            {
                for (int j = -1 + (int)piece.GridID.x; j <= 1 + (int)piece.GridID.x; j++)
                {

                    if (i >= 0 && i <= 8 && j >= 0 && j <= 8)
                    {
                        if (_allPiecesOnBoard[j, i] != null)
                        {
                            GamePiece script = _allPiecesOnBoard[j, i].GetComponent<GamePiece>();

                            //Debug.Log("Piece Name: " + script.name + " Shape: " + script.Shape);

                            if (script.isPlayerPiece != piece.isPlayerPiece)
                            {
                                piece.numPiecesInContact++;
                            }
                        }
                    }
                }
            }

            Debug.Log(piece.numPiecesInContact);
        }

        public void Attacked(Button piece)
        {
            GamePiece script = piece.GetComponent<GamePiece>();

            if (script.numPiecesInContact > 1)
            {
                /*
                _allPiecesOnBoard;
                _allPieces;
                _playerPieces;
                _playerPiecesOnGrid;
                _enemyPieces;
                _enemyPiecesOnGrid;
                */
                _isOccupied[(int)script.GridID.x, (int)script.GridID.y] = false;
                Destroy(piece.gameObject);
            }
        }

        public void EqualAttacking()
        {

        }
    }
}
