using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BoardGame;

public class GamePiece : MonoBehaviour, IMoveable
{
    [SerializeField] public StateMachine StateMachine = null;
    [SerializeField] public BoardManager BoardManager = null;

    [Header("Audio")]
    [SerializeField] public AudioClip PlacementSound = null;
    [SerializeField] public AudioClip MovementSound1 = null;
    [SerializeField] public AudioClip MovementSound2 = null;
    [SerializeField] public AudioClip MovementSound3 = null;

    [SerializeField] public Vector2 GridID = new Vector2(0, 0);
    [SerializeField] public Color Color = new Color(0, 0, 0, 0);
    [SerializeField] public string Shape = "";
    [SerializeField] public bool isPlayerPiece = false;
    [SerializeField] public int numPiecesInContact = 0;

    public bool _moved = false;
    public bool _cantMove = false;
    public bool _taken = false;
    public Vector2 _savedGridID = new Vector2(0, 0);
    public Vector2 _newGridID = new Vector2(0, 0);

    AudioManager _audioManager;
    public AudioSource _audioSource;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        PlacementSound = _audioManager.PlacementSound;
        MovementSound1 = _audioManager.MovementSound1;
        MovementSound2 = _audioManager.MovementSound2;
        MovementSound3 = _audioManager.MovementSound3;
    }

    private void Start()
    {
        //set up audio source
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _audioSource.volume = 0.25f;

    }

    void MovePiece(int newLocX, int newLocY)
    {
        if (!_moved)
        {
            //_newGridID = new Vector2(GridID.x + newLocX, GridID.y + newLocY);
            Vector2 _gridMovement = new Vector2(newLocX, newLocY);
            _savedGridID = GridID;
            StartCoroutine(BoardManager.MovePiece(this, _gridMovement, _savedGridID));

            int movementSoundChoice = Random.Range(0, 3);

            if (movementSoundChoice == 0)
            {
                _audioSource.volume = 0.5f;
                _audioSource.clip = MovementSound1;
            }
            else if (movementSoundChoice == 1)
            {
                _audioSource.volume = 0.5f;
                _audioSource.clip = MovementSound2;
            }
            else if (movementSoundChoice == 2)
            {
                _audioSource.volume = 0.5f;
                _audioSource.clip = MovementSound3;
            }
        }
    }

    // Player Movement
    public void MoveUp()
    {
        MovePiece(0, -1);
        _audioSource.Play();
    }

    public void MoveDiagonalUpLeft()
    {
        MovePiece(-1, -1);
        _audioSource.Play();
    }

    public void MoveDiagonalUpRight()
    {
        MovePiece(1, -1);
        _audioSource.Play();
    }

    
    public void MoveJumpDiagonalUpLeft()
    {
        if (BoardManager.JumpCheck(this, new Vector2(GridID.x - 1, GridID.y - 1)))
        {
            MovePiece(-2, -2);
        }
    }

    public void MoveJumpDiagonalUpRight()
    {
        if (BoardManager.JumpCheck(this, new Vector2(GridID.x + 1, GridID.y - 1)))
        {
            MovePiece(2, -2);
        }
    }

    public void MoveJumpUp()
    {
        if (BoardManager.JumpCheck(this, new Vector2(GridID.x, GridID.y-1)))
        {
            MovePiece(0, -2);
        }
    }
    

    // Enemy Movement
    public void MoveDown()
    {
        MovePiece(0, 1);
    }

    public void MoveDiagonalDownLeft()
    {
        MovePiece(-1, 1);
    }

    public void MoveDiagonalDownRight()
    {
        MovePiece(1, 1);
    }

    
    public void MoveJumpDown()
    {
        MovePiece(0, 2);
    }
    

    // not needed
    public void MoveLeft()
    {
        MovePiece(-1, 0);
    }

    public void MoveRight()
    {
        MovePiece(1, 0);
    }

    public void UndoMove()
    {
        if (_moved)
        {
            BoardManager.MovePieceBack(this, _newGridID, _savedGridID);
            _moved = false;
        }
    }
}
