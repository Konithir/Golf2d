using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    private Button _againButton;

    private StringBuilder StringBuilder = new StringBuilder();

    private const string ScoreString = "SCORE: ";
    private const string GameOverString = "GAME OVER!";
    private const string BestScoreString = " BEST: ";

    public void GameOver(int score)
    {
        var best = PlayerPrefs.GetInt("best");

        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("best", best);
        }

        StringBuilder.Clear();
        StringBuilder.AppendLine(GameOverString);
        StringBuilder.Append(ScoreString);
        StringBuilder.Append(score);
        StringBuilder.Append(BestScoreString);
        StringBuilder.Append(best);

        _text.text = StringBuilder.ToString();
        SetEndGamePanel(true);
    }

    public void SetEndGamePanel(bool visibility)
    {
        gameObject.SetActive(visibility);
    }

    public void ResetGame()
    {
        SetEndGamePanel(false);
        GameManager.Get().Player.Reset();
        GameManager.Get().GameController.StartRound();
    }
}
