using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace BoardGame
{
    public class SetupStateGenerateBoard : BoardGameState
    {
        public static event Action BoardData;

        SetupBoardGameBaseState SetupBoard;

        [SerializeField] RectTransform _boardGrid = null;
        [SerializeField] RectTransform _gamePiecesPanel = null;

        private float _tileSize = 100;

        private Vector2 _gridSize;
        private Vector2 _boardSize;
        private Vector2[,] _gridPositions;
        private Sprite[] _gamePieces;
        private Sprite _gridTile;
        private GameObject[] _playerPieces;
        private GameObject[] _enemyPieces;

        public Vector2[,] GridPositions => _gridPositions;
        public GameObject[] PlayerPieces => _playerPieces;
        public GameObject[] EnemyPieces => _enemyPieces;

        float _waitTime = 0.01f;

        public override void Enter()
        {
            Debug.Log("Creating game board...");

            SetupBoard = GetComponent<SetupBoardGameBaseState>();

            _gamePieces = (Sprite[])Resources.LoadAll<Sprite>("GamePieces");
            _gridTile = (Sprite)Resources.Load<Sprite>("GridSprite");

            SetupBoard._createdBoard = false;

            GenerateGrid(SetupBoard.BoardSizeX, SetupBoard.BoardSizeY);
            StartCoroutine(GenerateGamePieces(SetupBoard.Shapes, SetupBoard.Colors));
        }

        public override void Exit()
        {
            BoardData?.Invoke();
            Debug.Log("Finished creating game board");
        }

        public void GenerateGrid(int cols, int rows)
        {
            _gridSize = new Vector2(cols - 1, rows - 1);
            _boardSize = (_tileSize * _gridSize);

            _gridPositions = new Vector2[(int)_gridSize.x+1, (int)_gridSize.y+1];
            

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    float posX = col * _tileSize - _boardGrid.rect.x;
                    float posY = row * -_tileSize - _boardGrid.rect.y;
                    _gridPositions[col,row] = new Vector2(posX, posY);

                    GameObject grid = new GameObject("grid", typeof(Image));
                    grid.gameObject.transform.SetParent(_boardGrid);
                    Image img = grid.GetComponent<Image>();
                    img.sprite = _gridTile;

                    grid.transform.position = new Vector2(_gridPositions[col,row].x - (_boardSize.x / 2), _gridPositions[col,row].y + (_boardSize.y / 2));
                }
            }
        }

        public IEnumerator GenerateGamePieces(int shapes, int colors)
        {
            int i = 0;
            _playerPieces = new GameObject[shapes * colors];
            _enemyPieces = new GameObject[shapes * colors];

            // Player Pieces creation

            for (int color = (int)_gridSize.x; color > (int)_gridSize.x - colors; color--)
            {
                for (int shape = (int)_gridSize.y; shape > (int)_gridSize.y - shapes; shape--)
                {
                    yield return new WaitForSeconds(_waitTime);

                    float posX = shape * _tileSize - _gamePiecesPanel.rect.x;
                    float posY = color * -_tileSize - _gamePiecesPanel.rect.y;

                    GameObject gamePiece = new GameObject("Player: " + _gamePieces[i].name, typeof(Image), typeof(Button));
                    Image img = gamePiece.GetComponent<Image>();
                    Button button = gamePiece.GetComponent<Button>();
                    ButtonSetup(button);

                    gamePiece.gameObject.transform.SetParent(_gamePiecesPanel);
                    img.sprite = _gamePieces[i];

                    gamePiece.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                    _playerPieces[i] = gamePiece;
                    i++;
                }
            }

            yield return new WaitForSeconds(_waitTime * 5);
            i = 0;

            // Enemy Pieces creation

            for (int color = 0; color < colors; color++)
            {
                for (int shape = 0; shape < shapes; shape++)
                {
                    yield return new WaitForSeconds(_waitTime);
                    float posX = shape * _tileSize - _gamePiecesPanel.rect.x;
                    float posY = color * -_tileSize - _gamePiecesPanel.rect.y;

                    GameObject gamePiece = new GameObject("Enemy: "+_gamePieces[i].name, typeof(Image));
                    Image img = gamePiece.GetComponent<Image>();

                    gamePiece.gameObject.transform.SetParent(_gamePiecesPanel);
                    img.sprite = _gamePieces[i];

                    gamePiece.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                    _enemyPieces[i] = gamePiece;
                    i++;
                }
            }
            yield return new WaitForSeconds(_waitTime * 5);
            SetupBoard._createdBoard = true;

            StateMachine.ChangeState<SetupBoardGameBaseState>();
        }

        private void ButtonSetup(Button button)
        {
            ColorBlock cb = button.colors;
            cb.highlightedColor = cb.pressedColor;
            cb.pressedColor = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);

            if (button.name.Contains("blue"))
            {
                cb.selectedColor = Color.blue;
            }
            else if (button.name.Contains("green"))
            {
                cb.selectedColor = Color.green;
            }
            else if (button.name.Contains("red"))
            {
                cb.selectedColor = Color.red;
            }
            button.colors = cb;
            button.interactable = false;
        }

    }
}
