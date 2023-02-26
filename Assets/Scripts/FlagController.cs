using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    [SerializeField]
    private GameController _gameController;

    private ScoreCalculator _scoreCalculator;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _scoreCalculator = new ScoreCalculator();
    }

    private void HandleScore()
    {
        //Add Points
        _scoreCalculator.Score();

        //Update UI
        GameManager.Get().UIManager.ScoreController.UpdateScore(GameManager.Get().Player.Points);

        //Start New Round
        _gameController.StartRound();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;

        HandleScore();
    }
}
