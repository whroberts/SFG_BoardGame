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

        private Sprite[] _gamePieceSprites;
        private Sprite _gridTile;

        private GameObject[] _allPieces;
        List<GameObject> _allPiecesList = new List<GameObject>();

        private List<GameObject> _enemyPieceList = new List<GameObject>();
        private GameObject[,] _enemyPiecesOnGrid;
        private Vector2[,] _enemyPiecesGridPositions;
        private Color[,] _enemyPiecesColor;
        private String[,] _enemyPiecesShape;

        private List<GameObject> _playerPieceList = new List<GameObject>();

        private GameObject[,] _playerPiecesOnGrid;
        private Vector2[,] _playerPiecesGridPositions;
        private Color[,] _playerPiecesColor;
        private String[,] _playerPiecesShape;

        public Vector2[,] GridID => _gridID;
        public Vector2[,] GridPosition => _gridPosition;
        public GameObject[,] PlayerPiecesOnGrid => _playerPiecesOnGrid;

        public GameObject[] AllPieces => _allPieces;
        public List<GameObject> AllPiecesList => _allPiecesList;

        public List<GameObject> PlayerPiecesList => _playerPieceList;
        public Vector2[,] PlayerPiecesGridPosition => _playerPiecesGridPositions;
        public Color[,] PlayerPiecesColor => _playerPiecesColor;
        public String[,] PlayerPiecesShape => _playerPiecesShape;

        public List<GameObject> EnemyPiecesList => _enemyPieceList;
        public GameObject[,] EnemyPiecesOnGrid => _enemyPiecesOnGrid;
        public Vector2[,] EnemyPiecesGridPositions => _enemyPiecesGridPositions;
        public Color[,] EnemyPiecesColor => _enemyPiecesColor;
        public String[,] EnemyPiecesShape => _enemyPiecesShape;


        float _waitTime = 0.01f;

        private Sprite[] _gridTiles;

        public override void Enter()
        {
            //Debug.Log("Creating game board...");

            SetupBoard = GetComponent<SetupBoardGameBaseState>();

            _gamePieceSprites = (Sprite[])Resources.LoadAll<Sprite>("GamePieces_v3");
            //_gridTile = (Sprite)Resources.Load<Sprite>("GridSprite");

            _gridTiles = (Sprite[])Resources.LoadAll<Sprite>("GridSprites");

            SetupBoard._createdBoard = false;

            GenerateGrid(SetupBoard.BoardSizeX, SetupBoard.BoardSizeY);
            StartCoroutine(GenerateGamePieces(SetupBoard.Shapes, SetupBoard.Colors));
        }

        public override void Exit()
        {
            BoardData?.Invoke();
            //Debug.Log("Finished creating game board");
        }

        public void GenerateGrid(int cols, int rows)
        {
            bool dark = true;

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

                    if (dark)
                    {
                        img.sprite = _gridTiles[0];
                        dark = false;
                    }
                    else if (!dark)
                    {
                        img.sprite = _gridTiles[1];
                        dark = true;
                    }

                    grid.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                    _gridPosition[col, row] = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                }
            }
        }

        public IEnumerator GenerateGamePieces(int shapes, int colors)
        {
            int i = _gamePieceSprites.Length - 1;
            int k = 0;
            _allPieces = new GameObject[_gamePieceSprites.Length * 2];

            _playerPiecesOnGrid = new GameObject[(int)_gridSize.x + 1, (int)_gridSize.y + 1];
            _playerPiecesColor = new Color[shapes, (int)_gridSize.y + 1];
            _playerPiecesShape = new String[shapes, (int)_gridSize.y + 1];
            _playerPiecesGridPositions = new Vector2[shapes, (int)_gridSize.y+1];

            _enemyPiecesOnGrid = new GameObject[(int)_gridSize.x + 1, (int)_gridSize.y + 1];
            _enemyPiecesColor = new Color[shapes, (int)_gridSize.y + 1];
            _enemyPiecesShape = new String[shapes, (int)_gridSize.y + 1];
            _enemyPiecesGridPositions = new Vector2[shapes, (int)_gridSize.y + 1];

            // Player Pieces creation

            for (int color = (int)_gridSize.x - colors + 1; color < (int)_gridSize.x + 1; color++)
            {
                for (int shape = shapes-1; shape >= 0; shape--)
                {
                    //yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, 0.5f));

                    _playerPiecesGridPositions[shape, color] = new Vector2(shape, color);

                    float posX = shape * _tileSize - _gamePiecesPanel.rect.x;
                    float posY = color * -_tileSize - _gamePiecesPanel.rect.y;

                    GameObject gamePiece = new GameObject("Player: " + _gamePieceSprites[i].name, typeof(Image), typeof(Button), typeof(GamePiece), typeof(AudioSource));
                    Image img = gamePiece.GetComponent<Image>();
                    Button button = gamePiece.GetComponent<Button>();
                    GamePiece script = gamePiece.GetComponent<GamePiece>();
                    AudioSource audioSource = gamePiece.GetComponent<AudioSource>();

                    script.GridID = _playerPiecesGridPositions[shape, color];
                    script.BoardManager = StateMachine.BoardManager;
                    script.isPlayerPiece = true;
                    ButtonSetup(button);

                    _playerPiecesOnGrid[shape, color] = gamePiece;
                    _playerPiecesColor[shape, color] = script.Color;
                    _playerPiecesShape[shape, color] = script.Shape;

                    gamePiece.gameObject.transform.SetParent(_gamePiecesPanel);
                    img.sprite = _gamePieceSprites[i];

                    audioSource.clip = script.PlacementSound;
                    audioSource.volume = 0.5f;
                    audioSource.Play();
                    audioSource.loop = false;

                    gamePiece.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                    _playerPieceList.Add(gamePiece);
                    _allPieces[k] = gamePiece;
                    _allPiecesList.Add(gamePiece);
                    k++;
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
                    //yield return new WaitForSeconds(UnityEngine.Random.Range(0.15f, 0.35f));

                    _enemyPiecesGridPositions[shape, color] = new Vector2(shape, color);

                    float posX = shape * _tileSize - _gamePiecesPanel.rect.x;
                    float posY = color * -_tileSize - _gamePiecesPanel.rect.y;

                    GameObject gamePiece = new GameObject("Enemy: "+_gamePieceSprites[i].name, typeof(Image), typeof(Button), typeof(GamePiece), typeof(AudioSource));
                    Image img = gamePiece.GetComponent<Image>();
                    Button button = gamePiece.GetComponent<Button>();
                    GamePiece script = gamePiece.GetComponent<GamePiece>();
                    AudioSource audioSource = gamePiece.GetComponent<AudioSource>();

                    script.GridID = _enemyPiecesGridPositions[shape, color];
                    script.BoardManager = StateMachine.BoardManager;
                    ButtonSetup(button);

                    _enemyPiecesOnGrid[shape, color] = gamePiece;
                    _enemyPiecesColor[shape, color] = script.Color;
                    _enemyPiecesShape[shape, color] = script.Shape;

                    gamePiece.gameObject.transform.SetParent(_gamePiecesPanel);
                    img.sprite = _gamePieceSprites[i];

                    audioSource.clip = script.PlacementSound;
                    audioSource.volume = 0.5f;
                    audioSource.Play();
                    audioSource.loop = false;

                    gamePiece.transform.position = new Vector2(posX - (_boardSize.x / 2), posY + (_boardSize.y / 2));
                    _enemyPieceList.Add(gamePiece);
                    _allPieces[k] = gamePiece;
                    _allPiecesList.Add(gamePiece);
                    k++;
                    i++;
                }
            }

            yield return new WaitForSeconds(_waitTime * 5);
            SetupBoard._createdBoard = true;

            StateMachine.ChangeState<SetupBoardGameBaseState>();
        }

        private void ButtonSetup(Button button)
        {
            GamePiece script = button.GetComponent<GamePiece>();

            if (script != null && button.name.Contains("Player"))
            {
                //old color block
                /*
                ColorBlock cb = button.colors;
                cb.normalColor = new Color(175f / 255f, 1f, 175f / 255f, 1f);
                cb.selectedColor = new Color(0f, 1f, 0f, 1f);
                cb.highlightedColor = cb.selectedColor;
                cb.pressedColor = cb.selectedColor;
                cb.disabledColor = new Color(200f / 255f, 1f, 200f / 255f, 1f);
                */

                //new color block
                ColorBlock cb = button.colors;
                cb.disabledColor = new Color(200f / 255f, 200f / 255f, 200f / 255f, 1f);

                if (button.name.Contains("blue"))
                {
                    cb.normalColor = Color.blue;
                    cb.highlightedColor = new Color(0f, 0f, 80f/255f, 1f);
                    cb.selectedColor = Color.blue;
                    cb.pressedColor = new Color(0f, 0f, 160f / 255f, 1f);
                    script.Color = Color.blue;
                }
                else if (button.name.Contains("green"))
                {
                    cb.normalColor = Color.green;
                    cb.highlightedColor = new Color(0f, 80f / 255f, 0f, 1f);
                    cb.selectedColor = Color.green;
                    cb.pressedColor = new Color(0f, 160f / 255f, 0f, 1f);
                    script.Color = Color.green;
                }
                else if (button.name.Contains("red"))
                {
                    cb.normalColor = Color.red;
                    cb.highlightedColor = new Color(80f / 255f, 0f, 0f, 1f);
                    cb.selectedColor = Color.red;
                    cb.pressedColor = new Color(160f / 255f, 0f, 0f, 1f);
                    script.Color = Color.red;
                }
                button.colors = cb;
            }
            else if (script != null && button.name.Contains("Enemy"))
            {
                //old color block
                /*
                ColorBlock cb = button.colors;
                cb.disabledColor = new Color(1f, 200f / 255f, 200f / 255f, 1);
                cb.normalColor = new Color(1f, 175f / 255f, 175f / 255f, 1);
                cb.highlightedColor = cb.normalColor;
                cb.pressedColor = cb.normalColor;
                */

                //new color block
                ColorBlock cb = button.colors;
                cb.disabledColor = new Color(50f / 255f, 50f / 255f, 50f / 255f, 1);

                if (button.name.Contains("blue"))
                {
                    cb.normalColor = new Color(175f / 255f, 175f / 255f, 1f, 1);
                    cb.selectedColor = Color.blue;
                    script.Color = Color.blue;
                }
                else if (button.name.Contains("green"))
                {
                    cb.normalColor = new Color(175f / 255f, 1f, 175f / 255f, 1);
                    cb.selectedColor = Color.green;
                    script.Color = Color.green;
                }
                else if (button.name.Contains("red"))
                {
                    cb.normalColor = new Color(1f, 175f / 255f, 175f / 255f, 1);
                    cb.selectedColor = Color.red;
                    script.Color = Color.red;
                }
                cb.highlightedColor = cb.normalColor;
                cb.pressedColor = cb.normalColor;
                button.colors = cb;
            }

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
            else if (button.name.Contains("TripCirc"))
            {
                script.Shape = "TripCircle";
            }
            else if (button.name.Contains("X"))
            {
                script.Shape = "X";
            }

            Navigation newNav = new Navigation();
            newNav.mode = Navigation.Mode.None;
            button.navigation = newNav;

            button.interactable = false;
        }

    }
}
