using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private List<GameObject> _dotsList;

    [SerializeField]
    private GameObject _dotObj;

    private const int DOT_NUMBER = 20;  
    private const int MAX_FORCE = 200;
    private const float FORCE_ADDITION = 100f;

    private float _currentForce;

    private bool _shootStarted = false;
    private bool _gameReady = false;

    public bool GameReady
    {
        get { return _gameReady; }
        set { _gameReady = value; }
    }


    #region Private Methods

    private void Update()
    {
        BallUpdate();
    }

    private void Shoot()
    {
        _shootStarted = false;
        _rigidbody2D.AddForce(this.transform.up * _currentForce);
        foreach (GameObject dot in _dotsList)
        {
            dot.SetActive(false);
        }
        _gameReady = false;
        Invoke(nameof(Lose), 3f);
    }

    private void Lose()
    {
        StopBall();
        GameManager.Get().UIManager.EndGamePanel.GameOver(GameManager.Get().Player.Points);
    }

    private List<Vector2> CalculateArc()
    {
        var listOfPoints = new List<Vector2>();

        float maxDuration = 2f;
        float timeStepInterval = 0.1f;
        int maxSteps = (int)(maxDuration / timeStepInterval);
        Vector2 direction = transform.up;
        Vector2 startingPosition = transform.position;

        var velocity = _currentForce / _rigidbody2D.mass * Time.fixedDeltaTime;

        for (int i = 0; i < maxSteps; i++)
        {
            Vector2 calculatedPositon = startingPosition + direction * velocity * i * timeStepInterval;
            calculatedPositon.y += Physics2D.gravity.y / 2 * Mathf.Pow(i * timeStepInterval, 2);
            if (calculatedPositon.y < this.transform.position.y)
            {
                return listOfPoints;
            }
            listOfPoints.Add(calculatedPositon);
        }
        return listOfPoints;
    }

    #endregion
    #region Public Methods

  

    public void StopBall()
    {
        if(_rigidbody2D)
        {
            _rigidbody2D.velocity = Vector3.zero;
            _rigidbody2D.angularVelocity = 0;
        }  
    }

    public void StopLoseGameInvoke()
    {
        CancelInvoke();
    }

    public void BallUpdate()
    {
        if (_gameReady)
        {
            if (Input.anyKey && !_shootStarted)
            {
                _currentForce = 0;
                _shootStarted = true;
            }
            else if (Input.anyKey)
            {
                _currentForce += (FORCE_ADDITION + 3 * GameManager.Get().Player.Points) * Time.deltaTime;
                transform.Rotate(new Vector3(0, 0, 15 + GameManager.Get().Player.Points) * Time.deltaTime);

                var points = CalculateArc();

                for (int i = 0; i < points.Count; i++)
                {
                    _dotsList[i].transform.position = points[i];
                    _dotsList[i].SetActive(true);
                }
                if (_currentForce >= MAX_FORCE)
                {
                    Shoot();
                }
            }
            else if (!Input.anyKey && _shootStarted)
            {
                Shoot();
            }
        }
    }

    #endregion
}
