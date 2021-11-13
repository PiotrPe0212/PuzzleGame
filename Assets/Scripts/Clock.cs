using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Clock Instance { get; private set; }
    [SerializeField] private GameManager GameManager;
    public GameObject clockHand;
    public float _clockScale = 10;
    public float _minAngle = 60;
    public float _maxAngle = 60;
    public float _addCarbon = 5;
    private float _time;
    private float _actualAngle;
    private float _totalAngle;
    private float _timeToAngleProportion;
    private bool _gameStarted;
    private bool _resetValue;

    private bool carbonAdded;

    public bool CarbonState
    {
        get { return carbonAdded; }
        set
        {
            carbonAdded = value;
        }
    }

    private void Awake()
    {
        GameManager.GameStarted += StartClock;
        GameManager.GameReset += ResetClock;
    }

    private void OnDestroy()
    {
        GameManager.GameStarted -= StartClock;
        GameManager.GameReset -= ResetClock;
    }

    void Start()
    {
        _gameStarted = false;
        Instance = this;
       ResetClock();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_gameStarted)
        {
            return;
        }
        if (_time > 0)
        {
            ClockFunction(-Time.deltaTime);

            if (carbonAdded)
            {
                ClockFunction(_addCarbon);
                carbonAdded = false;
            }
        }
        else
        {
            GameManager.EndGame();
        }
        
    }

    void ClockFunction( float _addedTime)
    {
        _time += _addedTime;
        _actualAngle += _addedTime * _timeToAngleProportion;
        clockHand.transform.eulerAngles = new Vector3(0, 0, _actualAngle);

    }

    public void StartClock()
    {
        _gameStarted = true;
    }

    public void ResetClock()
    {
        clockHand.transform.eulerAngles = new Vector3(0, 0, _maxAngle);
        _totalAngle = Mathf.Abs(_minAngle) + Mathf.Abs(_maxAngle);
        _timeToAngleProportion = _totalAngle / _clockScale;
        _time = _clockScale;
        _actualAngle = _maxAngle;
    }
}
