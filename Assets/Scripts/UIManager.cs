using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private EndGamePanel _endGamePanel;

    [SerializeField]
    private ScoreController _scoreController;

    public EndGamePanel EndGamePanel
    {
        get { return _endGamePanel; }
    }

    public ScoreController ScoreController
    {
        get { return _scoreController; }
    }
  
}
