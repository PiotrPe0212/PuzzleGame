using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Clock Instance { get; private set; }
    public GameObject clockHand;
    public float _clockScale = 10;
    public float _minAngle = 60;
    public float _maxAngle = 60;
    public float _addCarbon = 5;
    private float _time;
    private float _actualAngle;
    private float _totalAngle;
    private float _timeToAngleProportion;
   

    private bool carbonAdded;

    public bool CarbonState
    {
        get { return carbonAdded; }
        set
        {
            carbonAdded = value;


        }
    }
    void Start()
    {
        Instance = this;
        clockHand.transform.eulerAngles = new Vector3(0, 0, _maxAngle);
        _totalAngle = Mathf.Abs(_minAngle) + Mathf.Abs(_maxAngle);
        _timeToAngleProportion = _totalAngle / _clockScale;
        _time = _clockScale;
        _actualAngle = _maxAngle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_time > 0)
        {
            ClockFunction(-Time.deltaTime);

            if (carbonAdded)
            {
                ClockFunction(_addCarbon);
                carbonAdded = false;
            }
        }
    }

    void ClockFunction( float _addedTime)
    {
        _time += _addedTime;
        _actualAngle += _addedTime * _timeToAngleProportion;
        clockHand.transform.eulerAngles = new Vector3(0, 0, _actualAngle);

    }
}
