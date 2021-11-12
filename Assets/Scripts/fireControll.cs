using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireControll : MonoBehaviour
{
    [SerializeField] private Board _board;
    private Animator _animator;
    public bool bigFireOn;
    public bool smallFireOn;
    public bool bigFireEnds;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _board.BoardCleaning += CleaningBoard;
        bigFireEnds = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (bigFireEnds)
        {
            bigFireOn = false;
            _animator.SetBool("SmaLfire", true);
            _animator.SetBool("BiGfire", false);
        }

    }

    void EndOfBigFire()
    {
        bigFireEnds = true;
    }
    void CleaningBoard()
    {
        bigFireEnds = false;

        
        
            //smallFireOn = false;
            _animator.SetBool("BiGfire", true);
            _animator.SetBool("SmaLfire", false);
    }
}