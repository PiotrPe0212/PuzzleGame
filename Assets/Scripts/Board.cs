using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject _boardTile;
    public static Board Instance { get; private set; }

    public event Action BoardCleaning;
    public int filledTiles;
    private bool _fulfilledBoard;
    public int BoardPoints = 16;
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
        BoardCreating();

    }
    void Start()
    {

        Instance = this;

    }


    void FixedUpdate()
    {

        if (filledTiles == BoardPoints && !_fulfilledBoard)
        {
            Clock.Instance.CarbonState = true;
            _fulfilledBoard = true;
            BoardCleaning?.Invoke();
            filledTiles = 0;

        }

        if (filledTiles < BoardPoints)
            _fulfilledBoard = false;
    }

    void BoardCreating()
    {
        for (int i = 0; i < (BoardPoints / 4); i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject rowObj;
                rowObj = Instantiate(_boardTile, new Vector3(0, 0, 1), Quaternion.identity, gameObject.transform);
                rowObj.transform.localPosition = new Vector3(-1f +j*0.65f, -0.9f+ i*0.65f, 0);
            }
        }
    }
}
