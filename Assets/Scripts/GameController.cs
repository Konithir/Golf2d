using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private BallController _ballController;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private GameObject _flag;

    private const float BALL_STARTING_POSITION_MIN = -8f;
    private const float BALL_STARTING_POSITION_MAX = -6f;
    private const float FLAG_STARTING_POSITION_MIN = 3f;
    private const float FLAG__STARTING_POSITION_MAX = 8f;

    private void Start()
    {
        StartRound();
    }

    public void StartRound()
    {
        _uiManager.ScoreController.UpdateScore(GameManager.Get().Player.Points);

        _ballController.StopBall();
        _ballController.StopLoseGameInvoke();

        _flag.transform.position = new Vector3(Random.Range(FLAG_STARTING_POSITION_MIN, FLAG__STARTING_POSITION_MAX), -4.08f, 0);

        _ballController.transform.position = new Vector3(Random.Range(BALL_STARTING_POSITION_MIN, BALL_STARTING_POSITION_MAX), -3.3f, 0);
        _ballController.transform.eulerAngles = new Vector3(0, 0, -90);

        _ballController.GameReady = true;
    }
}
