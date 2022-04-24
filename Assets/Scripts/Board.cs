using System;
using UnityEngine;
using TMPro;


public class Board : MonoBehaviour
{
    [SerializeField] private GameObject _boardTile;
    [SerializeField] private GameManager GameManager;
    [SerializeField] private TMP_Text pointText;
    [SerializeField] private TMP_Text endScore;
    public static Board Instance { get; private set; }

    public event Action BoardCleaning;
    public int filledTiles;
    private bool _fulfilledBoard;
    public int BoardPoints = 16;
    private bool _gameStarted;
    private int _points;
    //private Component _pointsText;
    public int BoardCounter
    {
        get { return filledTiles; }
        set
        {
            filledTiles += value;

        }
    }
    private void Awake()
    {
        GameManager.GameStarted += BoardStart;
        GameManager.GameReset += BoardReset;
        GameManager.GameOver += GameOverBoard;
    }

    private void OnDestroy()
    {
        GameManager.GameStarted -= BoardStart;
        GameManager.GameReset -= BoardReset;
        GameManager.GameOver -= GameOverBoard;
    }

    void Start()
    {

        Instance = this;
        _gameStarted = false;
        BoardCreating();
        
    }


    void FixedUpdate()
    {
        if (!_gameStarted)
        {
            return;
        }
        if (filledTiles == BoardPoints && !_fulfilledBoard)
        {
            Clock.Instance.CarbonState = true;
            _fulfilledBoard = true;
            _points += 100;
            pointText.text = "Points: " + _points.ToString();
            BoardCleaning?.Invoke();
            filledTiles = 0;

        }

        if (filledTiles < BoardPoints)
            _fulfilledBoard = false;


    }

    void BoardCreating()
    {
        _points = 0;
        filledTiles = 0;
        pointText.text = "Points: " + _points.ToString();
        for (var i = 0; i < (BoardPoints / 4); i++)
        {
            for (var j = 0; j < 4; j++)
            {
                GameObject rowObj = Instantiate(_boardTile, new Vector3(0, 0, 1), Quaternion.identity, gameObject.transform);
                rowObj.transform.localPosition = new Vector3(-1f + j * 0.65f, -0.9f + i * 0.65f, 0);
            }
        }
    }

    public void BoardStart()
    {
        _gameStarted = true;
    }

    public void BoardReset()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("BoardTile"))
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        BoardCreating();
    }

    public void GameOverBoard()
    {
        endScore.text = _points.ToString();
    }


}
