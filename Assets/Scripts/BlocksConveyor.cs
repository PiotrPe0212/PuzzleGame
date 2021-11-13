using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlocksConveyor : MonoBehaviour
{
    [SerializeField] private GameManager GameManager;
    public GameObject[] blocks;
    public GameObject conveyor;
    public float _timeToWait=1.5f;
    private GameObject _block;
    private float _timer;
    private int _random =100;
    private int _blockNumber;

    private bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        _blockNumber = 0;
        gameStarted = false;
    }

    private void Awake()
    {
        GameManager.GameStarted += StartConveyor;
        GameManager.GameReset += ResetConveyor;
        GameManager.GameOver += StopConveyor;
    }

    private void OnDestroy()
    {
        GameManager.GameStarted -= StartConveyor;
        GameManager.GameReset -= ResetConveyor;
        GameManager.GameOver -= StopConveyor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!gameStarted)
        {
            return;
        }
        if (_random == 100)
        _random = Random.Range(0, blocks.Length);
        _timer += Time.deltaTime;
        if(_timer>=_timeToWait)
        {
            _block = Instantiate(blocks[_random], new Vector3(0, 0, 1), Quaternion.identity, gameObject.transform);
            _block.transform.localPosition = new Vector3(-8, -6.5f, 0);
            _block.transform.name += _blockNumber;
            _random = 100;
            _timer = 0;
            _blockNumber++;
        }
        
    }

    public void StartConveyor()
    {
        gameStarted = true;
    }

    public void StopConveyor()
    {
        gameStarted = false;
    }

    public void ResetConveyor()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name != "Conveyor")
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        gameStarted = true;
    }
    
}
