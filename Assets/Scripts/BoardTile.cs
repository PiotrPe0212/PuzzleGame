using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    Vector3 _tilePosition;
    public LayerMask _detectLAyer;
    Ray _ray;
    public bool _puzzleDetected;
    public RaycastHit2D _puzzleRay;
    public GameObject _puzzle;

    public int _puzzlePoint;
    private GameObject _boardObject;
    private Board _boardScript;
    private bool _reset = true;

    void Start()
    {
        _tilePosition = gameObject.transform.position;
        _puzzleDetected = false;
        _ray = new Ray(_tilePosition, transform.forward);


    }

    private void Awake()
    {
        _boardObject = GameObject.Find("Board");
        _boardScript = (Board)_boardObject.GetComponent(typeof(Board));
        _boardScript.BoardCleaning += AllTilesFilled;

    }
    private void OnDestroy()
    {
        _boardScript.BoardCleaning -= AllTilesFilled;
    }


    void FixedUpdate()
    {

        if (Physics2D.GetRayIntersection(_ray, 10))
        {
            _puzzleRay = Physics2D.GetRayIntersection(_ray, 10);
            _puzzle = GameObject.Find(_puzzleRay.transform.name);
            if (!_puzzleDetected && _puzzle)
            {
                if (_puzzle.transform.tag == "PuzzleDrop")
                {
                    if (_puzzle.GetComponent<puzzle>())
                        _puzzle.GetComponent<puzzle>().goodPlacePoint = 1;

                    _puzzleDetected = true;
                    _reset = false;
                    Invoke("AddBoardPoint", 0.5f);

                }
            }

        }
        else
        {

            if (!_reset)
            {
                _puzzleDetected = false;
                if (_puzzle)
                    _puzzle.GetComponent<puzzle>().goodPlacePoint = -1;
                Board.Instance.BoardCounter = -1;
                _reset = true;
            }
        }
    }


    private void AllTilesFilled()
    {
        Invoke("Destr", 1f);

    }

    void Destr()
    {
        Destroy(_puzzle);
        _puzzle = null;
        _puzzleDetected = false;
        _reset = true;
    }

    void AddBoardPoint()
    {
        Board.Instance.BoardCounter = 1;
    }
}
