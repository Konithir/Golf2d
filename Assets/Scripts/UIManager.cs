using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text ScoreText { get; set; }
    private Text EndGameScoreText { get; set; }
    private Button AgainButton { get; set; }
    private GameObject EndGamePanel { get; set; }
    private GameObject UIObject { get; set; }
    private StringBuilder StringBuilder { get; set; }

    private const string ScoreString = "SCORE: ";
    private const string GameOverString = "GAME OVER!";
    private const string BestScoreString = " BEST: ";

    private void Awake()
    {
        StringBuilder = new StringBuilder();
    }

    public void InstantiateUI(GameObject ui)
    {
        UIObject = Instantiate(ui, Vector3.zero, Quaternion.identity);
        UIObject.GetComponent<Canvas>().worldCamera = Camera.main;
        UIObject.name = ui.name = "UICanvas";
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        EndGameScoreText = GameObject.Find("EndGameScoreText").GetComponent<Text>();
        AgainButton = GameObject.Find("AgainButton").GetComponent<Button>();
        EndGamePanel = GameObject.Find("EndGamePanel");
        AgainButton.onClick.AddListener(ResetGame);
        SetEndGamePanel(false);
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
        GameManager.Get().BallController.StartRound();
    }

}
