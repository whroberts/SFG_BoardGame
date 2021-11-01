using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GenerateBoardUI : MonoBehaviour
    {
        [SerializeField] RectTransform _boardGrid = null;
        [SerializeField] RectTransform _gamePiecesPanel = null;
        [SerializeField] Sprite _gridTile = null;

        private float _tileSize = 100;

        Vector2 _gridSize;
        Vector2 _boardSize;
        Sprite[] _gamePieces;

        private void Awake()
        {
            _gamePieces = (Sprite[])Resources.LoadAll<Sprite>("GamePieces");
        }


        public void GenerateGrid(int cols, int rows)
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
                    img.sprite = _gridTile;

                    grid.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                }
            }
        }

        public void GenerateGamePieces(int shapes, int colors)
        {
            int i = 0;
            for (int color = 0; color < colors; color++)
            {
                for (int shape = 0; shape < shapes; shape++)
                {
                    float posX = shape * _tileSize - _gamePiecesPanel.rect.x;
                    float posY = color * -_tileSize - _gamePiecesPanel.rect.y;

                    GameObject gamePiece = new GameObject("GamePiece", typeof(Image));
                    gamePiece.gameObject.transform.SetParent(_gamePiecesPanel);
                    Image img = gamePiece.GetComponent<Image>();
                    img.sprite = _gamePieces[i];

                    gamePiece.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                    i++;
                }
            }

            i = 0;
            for (int color = (int)_gridSize.x; color > (int)_gridSize.x - colors; color--)
            {
                for (int shape = (int)_gridSize.y; shape > (int)_gridSize.y - shapes; shape--)
                {
                    float posX = shape * _tileSize - _gamePiecesPanel.rect.x;
                    float posY = color * -_tileSize - _gamePiecesPanel.rect.y;

                    GameObject gamePiece = new GameObject("GamePiece", typeof(Image));
                    gamePiece.gameObject.transform.SetParent(_gamePiecesPanel);
                    Image img = gamePiece.GetComponent<Image>();
                    img.sprite = _gamePieces[i];

                    gamePiece.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                    i++;
                }
            }
        }
    }
}
