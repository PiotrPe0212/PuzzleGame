using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas _StartCanvas;
    [SerializeField] private Canvas _GameOverCanvas;
    [SerializeField] private Canvas _GameCanvas;

    public event Action GameStarted;
    public event Action GameReset;
    public event Action GameOver;

    private void Start()
    {

        _StartCanvas.enabled = true;
        _GameOverCanvas.enabled = false;
        _GameCanvas.enabled = false;
    }

    public void StartGame()
    {
        _GameCanvas.enabled = true;
        _StartCanvas.enabled = false;
        GameStarted?.Invoke();
    }

    public void EndGame()
    {
        _GameOverCanvas.enabled = true;
        GameOver?.Invoke();

    }

    public void ResetGame()
    {
        _GameOverCanvas.enabled = false;
GameReset?.Invoke();
    }
    
}
