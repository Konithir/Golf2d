using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    private StringBuilder StringBuilder = new StringBuilder();

    public void UpdateScore(int score)
    {
        StringBuilder.Clear();
        StringBuilder.Append(score);
        _scoreText.text = StringBuilder.ToString();
    }

}
