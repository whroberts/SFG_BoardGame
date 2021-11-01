using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GenerateBoardUI : MonoBehaviour
    {
        [SerializeField] RectTransform _boardGrid = null;
        [SerializeField] RectTransform _gamePieces = null;
        [SerializeField] Sprite _spriteTile = null;

        private float _tileSize = 100;

        Vector2 _gridSize;
        Vector2 _boardSize;

        public void GenerateGrid(int rows, int cols)
        {
            _gridSize = new Vector2(cols - 1, rows - 1);
            _boardSize = (_tileSize * _gridSize);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    float posX = col * _tileSize - _boardGrid.rect.x;
                    float posY = row * -_tileSize - _boardGrid.rect.y;

                    GameObject grid = new GameObject("grid", typeof(Image));
                    grid.gameObject.transform.SetParent(_boardGrid);
                    Image img = grid.GetComponent<Image>();
                    img.sprite = _spriteTile;

                    grid.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));

                }
            }
        }

        public void GenerateGamePieces(int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    float posX = col * _tileSize - _gamePieces.rect.x;
                    float posY = row * -_tileSize - _gamePieces.rect.y;

                    GameObject piece = new GameObject("piece", typeof(Image));
                    piece.gameObject.transform.SetParent(_gamePieces);
                    Image img = piece.GetComponent<Image>();
                    img.sprite = _spriteTile;

                    piece.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));

                }
            }
        }
    }
}
