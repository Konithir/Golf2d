using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private const float BallStartingPositonMin = -8f;
    private const float BallStartingPositonMax = -6f;
    private const float FlagStartingPositionMin = 3f;
    private const float FlagStartingPositionMax = 8f;
    private const int DotNumber = 20;  
    private const int MaxForce = 200;
    private const float ForceAddition = 100f;
    [SerializeField]
    private float CurrentForce;

    private bool ShootStarted = false;
    private bool GameReady = false;

    private List<GameObject> DotsList = new List<GameObject>();
    private GameObject Flag;
    private Rigidbody2D Rigidbody2D;


    #region Private Methods

    private void Awake()
    {
        InitializeDots();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void InitializeDots()
    {
        var DotParent = new GameObject();
        DotParent.name = nameof(DotParent);
        for (int i = 0; i < DotNumber; i++)
        {
            var dot = Instantiate(GameManager.Get().ResourcesManager.GetPrefab(PrefabEnum.Dot), Vector3.zero, Quaternion.identity);
            dot.name = nameof(dot);
            dot.transform.SetParent(DotParent.transform);
            DotsList.Add(dot);
        }
    }

    private void Shoot()
    {
        ShootStarted = false;
        Rigidbody2D.AddForce(this.transform.up * CurrentForce);
        foreach (GameObject dot in DotsList)
        {
            dot.SetActive(false);
        }
        GameReady = false;
        Invoke(nameof(Lose), 3f);
    }

    private void Lose()
    {
        StopBall();
        GameManager.Get().UIManager.GameOver(GameManager.Get().Player.Points);
    }

    private List<Vector2> CalculateArc()
    {
        var listOfPoints = new List<Vector2>();

        float maxDuration = 2f;
        float timeStepInterval = 0.1f;
        int maxSteps = (int)(maxDuration / timeStepInterval);
        Vector2 direction = transform.up;
        Vector2 startingPosition = transform.position;

        var velocity = CurrentForce / Rigidbody2D.mass * Time.fixedDeltaTime;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Score();
        StartRound();
    }

    #endregion
    #region Public Methods

    public void StartRound()
    {
        if (!Flag)
        {
            Flag = Instantiate(GameManager.Get().ResourcesManager.GetPrefab(PrefabEnum.Flag), Vector3.zero, Quaternion.identity);
            Flag.name = nameof(Flag);
        }
        Flag.transform.position = new Vector3(Random.Range(FlagStartingPositionMin,FlagStartingPositionMax), -4.08f, 0);
        this.transform.position = new Vector3(Random.Range(BallStartingPositonMin,BallStartingPositonMax), -3.3f, 0);
        StopBall();
        this.transform.eulerAngles = new Vector3(0, 0, -90);
        this.gameObject.SetActive(true);
        GameReady = true;
    }

    public void StopBall()
    {
        if(Rigidbody2D)
        {
            Rigidbody2D.velocity = Vector3.zero;
            Rigidbody2D.angularVelocity = 0;
        }  
    }

    public void Score()
    {
        GameManager.Get().Player.Points++;
        GameManager.Get().UIManager.UpdateScore(GameManager.Get().Player.Points);
        CancelInvoke();
    }


    public void BallUpdate()
    {
        if (GameReady)
        {
            if (Input.anyKey && !ShootStarted)
            {
                CurrentForce = 0;
                ShootStarted = true;
            }
            else if (Input.anyKey)
            {
                CurrentForce += (ForceAddition + 3 * GameManager.Get().Player.Points) * Time.deltaTime;
                this.transform.Rotate(new Vector3(0, 0, 15 + GameManager.Get().Player.Points) * Time.deltaTime);
                var points = CalculateArc();
                for (int i = 0; i < points.Count; i++)
                {
                    DotsList[i].transform.position = points[i];
                    DotsList[i].SetActive(true);
                }
                if (CurrentForce >= MaxForce)
                {
                    Shoot();
                }
            }
            else if (!Input.anyKey && ShootStarted)
            {
                Shoot();
            }
        }
    }

    #endregion
}
