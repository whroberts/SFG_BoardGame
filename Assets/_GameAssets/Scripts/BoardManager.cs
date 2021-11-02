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

        private int BoardSizeX;
        private int BoardSizeY;
        private int Colors;
        private int Shapes;

        private GameObject[] _playerPieces;
        private GameObject[] _enemyPieces;
        private Vector2[,] _gridPositions;

        public GameObject[] PlayerPieces => _playerPieces;

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

            _gridPositions = GenerateBoard.GridPositions;
            _playerPieces = GenerateBoard.PlayerPieces;
            _enemyPieces = GenerateBoard.EnemyPieces;
        }
    }
}
