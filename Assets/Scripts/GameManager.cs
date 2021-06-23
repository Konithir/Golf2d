using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    public ResourcesManager ResourcesManager { get; set; }
    public UIManager UIManager { get; set; }
    public BallController BallController { get; set; }
    public PlayerModel Player { get; set; }

    public static GameManager Get()
    {
        return singleton;
    }

    private void Awake()
    {
        singleton = this;
        ResourcesManager = gameObject.AddComponent<ResourcesManager>();
        UIManager = gameObject.AddComponent<UIManager>();
        Player = new PlayerModel();
    }

    private void Start()
    {
        Camera.main.aspect = 16f / 9f;
        UIManager.InstantiateUI(ResourcesManager.GetPrefab(PrefabEnum.UICanvas));
        UIManager.UpdateScore(Player.Points);
        BallController = Instantiate(ResourcesManager.GetPrefab(PrefabEnum.Ball),Vector3.zero,Quaternion.identity).AddComponent<BallController>();
        BallController.gameObject.name = "Ball";
        BallController.StartRound();
    }

    private void Update()
    {
        BallController.BallUpdate();
    }
}
