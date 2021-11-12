using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksConveyor : MonoBehaviour
{
    public GameObject[] blocks;
    public GameObject conveyor;
    public float _timeToWait=1f;
    private GameObject _block;
    private float _timer;
    private int _random =100;
    private int _blockNumber;
    // Start is called before the first frame update
    void Start()
    {
        _blockNumber = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_random == 100)
        _random = Random.Range(0, blocks.Length);
        _timer += Time.deltaTime;
        if(_timer>=_timeToWait)
        {
            _block = Instantiate(blocks[_random], new Vector3(0, 0, 1), Quaternion.identity, gameObject.transform);
            _block.transform.localPosition = new Vector3(-4, -6.5f, 0);
            _block.transform.name += _blockNumber;
            _random = 100;
            _timer = 0;
            _blockNumber++;
        }
        
    }
}
