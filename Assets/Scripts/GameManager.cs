using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;

    [Header("References")]

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private PlayerModel _player;

    [SerializeField]
    private GameController _gameController;

    public PlayerModel Player
    {
        get { return _player; }
        set { _player = value; }
    }

    public UIManager UIManager
    {
        get { return _uiManager; }
        set { _uiManager = value; }
    }

    public GameController GameController
    {
        get { return _gameController; }
    }

    public static GameManager Get()
    {
        return singleton;
    }

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        GameInitialization();
    }

    private void GameInitialization()
    {
        Camera.main.aspect = 16f / 9f;

        UIManager.ScoreController.UpdateScore(Player.Points);

        Player.Reset();
    }
}
