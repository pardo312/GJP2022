using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Jiufen.Audio;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Singleton;
    public EnemyZonesController enemyZonesController;
    public event Action<LevelStage> OnGameStateChanged;
    public int zone = 0;

    public LevelStage LevelStage = LevelStage.setup;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Singleton != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StartGame();
        AudioManager.PlayAudio("OST_WALK");
    }

    public void StartGame()
    {
        SetLevelState(LevelStage.gameMode);
        enemyZonesController.SpawnEnemies();
    }

    private void SetLevelState(LevelStage state)
    {
        LevelStage = state;
        OnGameStateChanged?.Invoke(LevelStage);
    }
}

public enum LevelStage
{
    setup = 0,
    inbetween = 1,
    gameMode = 2,
    victory = 3
}