using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    [HideInInspector]
    public ResourcesManager ResourcesManager;
    [HideInInspector]
    public UIManager UIManager;
    [HideInInspector]
    public BallController BallController;
    [HideInInspector]
    public PlayerModel Player = new PlayerModel();

    public static GameManager Get()
    {
        return singleton;
    }

    private void Awake()
    {
        singleton = this;
        ResourcesManager = gameObject.AddComponent<ResourcesManager>();
        UIManager = gameObject.AddComponent<UIManager>();
    }

    private void Start()
    {
        UIManager.InstantiateUI(ResourcesManager.GetPrefab(PrefabEnum.UICanvas));
        UIManager.UpdateScore(Player.Points);
        BallController = Instantiate(ResourcesManager.GetPrefab(PrefabEnum.Ball),Vector3.zero,Quaternion.identity).AddComponent<BallController>();
        BallController.gameObject.name = "Ball";
        BallController.StartRound();
    }
}
