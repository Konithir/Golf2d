using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;

    [Header("References")]

    [SerializeField]
    private ResourcesManager _resourcesManager;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private PlayerModel _player;

    private BallController BallController;

    public PlayerModel Player
    {
        get { return _player; }
        set { _player = value; }
    }

    public ResourcesManager ResourcesManager
    {
        get { return _resourcesManager; }
        set { _resourcesManager = value; }
    }

    public UIManager UIManager
    {
        get { return _uiManager; }
        set { _uiManager = value; }
    }

    public static GameManager Get()
    {
        return singleton;
    }

    private void Awake()
    {
        singleton = this;
        Player = new PlayerModel();
    }

    private void Start()
    {
        GameInitialization();
    }

    private void GameInitialization()
    {
        Camera.main.aspect = 16f / 9f;

        UIManager.UpdateScore(Player.Points);

        BallController = Instantiate(ResourcesManager.GetPrefab(PrefabEnum.Ball), Vector3.zero, Quaternion.identity).AddComponent<BallController>();
        BallController.gameObject.name = "Ball";

        Player.Reset();

        StartRound();
    }

    public void StartRound()
    {
        BallController.StartRound();
    }
}
