using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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
        private Vector2[,] _gridID;
        private Vector2[,] _gridPosition;

        private Sprite[] _gamePieces;
        private Sprite _gridTile;
        private GameObject[] _enemyPieces;

        private Vector2[,] _playerPiecesGridPositions;
        private GameObject[] _playerPieces;
        private Color[,] _playerPiecesColor;
        private String[,] _playerPiecesShape;

        public Vector2[,] GridID => _gridID;
        public Vector2[,] GridPosition => _gridPosition;
        public Vector2[,] PlayerPiecesGridPosition => _playerPiecesGridPositions;
        public GameObject[] PlayerPieces => _playerPieces;
        public GameObject[] EnemyPieces => _enemyPieces;
        public Color[,] PlayerPiecesColor => _playerPiecesColor;
        public String[,] PlayerPiecesShape => _playerPiecesShape;

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

            _gridID = new Vector2[(int)_gridSize.x+1, (int)_gridSize.y+1];
            _gridPosition = new Vector2[(int)_gridSize.x + 1, (int)_gridSize.y + 1];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    float posX = col * _tileSize - _boardGrid.rect.x;
                    float posY = row * -_tileSize - _boardGrid.rect.y;
                    _gridID[col,row] = new Vector2(col, row);
                    

                    GameObject grid = new GameObject("grid", typeof(Image), typeof(GridScript));
                    grid.gameObject.transform.SetParent(_boardGrid);
                    Image img = grid.GetComponent<Image>();
                    GridScript script = grid.GetComponent<GridScript>();
                    script.GridPosition = new Vector2(col, row);

                    img.sprite = _gridTile;

                    grid.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                    _gridPosition[col, row] = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                }
            }
        }

        public IEnumerator GenerateGamePieces(int shapes, int colors)
        {
            int i = _gamePieces.Length - 1;
            _playerPieces = new GameObject[shapes * colors];
            _enemyPieces = new GameObject[shapes * colors];
            _playerPiecesColor = new Color[shapes, (int)_gridSize.y + 1];
            _playerPiecesShape = new String[shapes, (int)_gridSize.y + 1];
            _playerPiecesGridPositions = new Vector2[shapes, (int)_gridSize.y+1];
            // Player Pieces creation

            for (int color = (int)_gridSize.x - colors + 1; color < (int)_gridSize.x + 1; color++)
            {
                //for (int shape = 0; shape < shapes; shape++)
                for (int shape = shapes-1; shape >= 0; shape--)
                {
                    yield return new WaitForSeconds(_waitTime);

                    _playerPiecesGridPositions[shape, color] = new Vector2(shape, color);

                    float posX = shape * _tileSize - _gamePiecesPanel.rect.x;
                    float posY = color * -_tileSize - _gamePiecesPanel.rect.y;

                    GameObject gamePiece = new GameObject("Player: " + _gamePieces[i].name, typeof(Image), typeof(Button), typeof(PlayerGamePiece));
                    Image img = gamePiece.GetComponent<Image>();
                    Button button = gamePiece.GetComponent<Button>();
                    PlayerGamePiece script = gamePiece.GetComponent<PlayerGamePiece>();
                    script.GridID = _playerPiecesGridPositions[shape, color];
                    script.BoardManager = StateMachine.BoardManager;
                    ButtonSetup(button);

                    _playerPiecesColor[shape, color] = script.Color;
                    _playerPiecesShape[shape, color] = script.Shape;

                    gamePiece.gameObject.transform.SetParent(_gamePiecesPanel);
                    img.sprite = _gamePieces[i];

                    gamePiece.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                    _playerPieces[i] = gamePiece;
   
                    i--;
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

                    GameObject gamePiece = new GameObject("Enemy: "+_gamePieces[i].name, typeof(Image), typeof(Button), typeof(PlayerGamePiece));
                    Image img = gamePiece.GetComponent<Image>();
                    Button button = gamePiece.GetComponent<Button>();
                    PlayerGamePiece script = gamePiece.GetComponent<PlayerGamePiece>();
                    script.GridID = _playerPiecesGridPositions[shape, color];
                    ButtonSetup(button);

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

            foreach (GameObject pieces in _playerPieces)
            {
                PlayerGamePiece script = button.GetComponent<PlayerGamePiece>();

                if (script != null)
                {
                    if (button.name.Contains("blue"))
                    {
                        cb.selectedColor = Color.blue;
                        script.Color = Color.blue;
                    }
                    else if (button.name.Contains("green"))
                    {
                        cb.selectedColor = Color.green;
                        script.Color = Color.green;
                    }
                    else if (button.name.Contains("red"))
                    {
                        cb.selectedColor = Color.red;
                        script.Color = Color.red;
                    }
                    button.colors = cb;

                    if (button.name.Contains("Circle"))
                    {
                        script.Shape = "Circle";
                    }
                    else if (button.name.Contains("Lamda"))
                    {
                        script.Shape = "Lamda";
                    }
                    else if (button.name.Contains("Omega"))
                    {
                        script.Shape = "Omega";
                    }
                    else if (button.name.Contains("Plus"))
                    {
                        script.Shape = "Plus";
                    }
                    else if (button.name.Contains("Star"))
                    {
                        script.Shape = "Star";
                    }
                    else if (button.name.Contains("Trap"))
                    {
                        script.Shape = "Trap";
                    }
                    else if (button.name.Contains("Triangle"))
                    {
                        script.Shape = "Triangle";
                    }
                    else if (button.name.Contains("TripCircle"))
                    {
                        script.Shape = "Triangle";
                    }
                    else if (button.name.Contains("X"))
                    {
                        script.Shape = "X";
                    }
                }
            }

            Navigation newNav = new Navigation();
            newNav.mode = Navigation.Mode.None;
            button.navigation = newNav;

            button.interactable = false;
        }

    }
}
