using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public enum GameState
    {
        Ongoing,
        GameOver,
        GameWon
    }

    public Player player;
    public TileManager tileManager;
    public Spawner spawner;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameState state;

    void Start()
    {
        Restart();
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case GameState.GameWon:
            case GameState.GameOver:
                if (Input.GetKey(KeyCode.Space))
                {
                    Restart();
                }
                break;
            case GameState.Ongoing:
                tileManager.RegenerateVisited();
                if (player.IsDead() ||  TimerController.instance.OutOfTime())
                {
                    LoseScreen.SetActive(true);
                    TimerController.instance.EndTimer();
                    state = GameState.GameOver;
                }
                else if (player.HasWon())
                {
                    WinScreen.SetActive(true);
                    TimerController.instance.EndTimer();
                    state = GameState.GameWon;
                }
                break;
        }
    }

    public void Restart()
    {
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        state = GameState.Ongoing;
        tileManager.GenerateMap();
        spawner.Reset();
        player.ResetPlayer(GetStartPos());
        StartTimer();
    }

    public void StartTimer()
    {
        TimerController.instance.BeginTimer();
    }

    public Vector2 GetStartPos()
    {
        return tileManager.GetStartPlayerPosition();
    }
}
