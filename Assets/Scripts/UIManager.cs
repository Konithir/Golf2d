using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Text ScoreText;

    [SerializeField]
    private Text EndGameScoreText;

    [SerializeField]
    private Button AgainButton;

    [SerializeField]
    private GameObject EndGamePanel;

    [SerializeField]
    private GameObject UIObject;

    [SerializeField]
    private StringBuilder StringBuilder;

    private const string ScoreString = "SCORE: ";
    private const string GameOverString = "GAME OVER!";
    private const string BestScoreString = " BEST: ";

    private void Awake()
    {
        StringBuilder = new StringBuilder();
    }

    public void UpdateScore(int score)
    {
        StringBuilder.Clear();
        StringBuilder.Append(score);
        ScoreText.text = StringBuilder.ToString();
    }

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

        EndGameScoreText.text = StringBuilder.ToString();
        SetEndGamePanel(true);
    }

    public void SetEndGamePanel(bool visibility)
    {
        EndGamePanel.SetActive(visibility);
    }

    public void ResetGame()
    {
        SetEndGamePanel(false);
        GameManager.Get().StartRound();
    }

}
