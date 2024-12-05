using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotStateController : MonoBehaviour
{
    //player objects
    public GameObject player;
    public GameObject playerPrefab;
    public GameObject playerPrefab2;
    public SplitPlayerController playerControl;
    private PlayerScore playerScore;
    public string TargetPlayerName { get; set; }

    //misc robot objects
    public GameObject scanner;
    public RobotState currentState;
    public List<Transform> destinations = new List<Transform>();
    public NavMeshAgent robot;
    public Transform currentDestination;

    //materials
    public Material red;
    public Material blue;
    public Material orange;
    public Material yellow;
    public Material green;

    //robot stats
    public float health = 20f;
    public float fleeHealth = 10f;
    public float detectionRange = 5f;
    public  float attackRange = 2f;
    public float pauseDuration = 5f;
    public float pauseTimer;
    public float attackDamage = 5f;

    

    // Start is called before the first frame update
    void Start()
    {
        robot = GetComponent<NavMeshAgent>();
        currentDestination = RandomDestination();
        SetState(new RobotWorking(this));

        if (playerPrefab != null)
        {
            player = Instantiate(playerPrefab);
        }
        if (playerPrefab2 != null)
        {
            player = Instantiate(playerPrefab2);
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    public void SetPlayerScore(PlayerScore score)
    {
        playerScore = score;
    }

    public Transform RandomDestination()
    {
        if (destinations.Count > 0)
        {
            int rd = Random.Range(0, destinations.Count);
            return destinations[rd];
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.CheckTransitions();
        currentState.Act();
    }

    public void SetState(RobotState rs)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = rs;

        if(currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void TakeDamage(float damage, GameObject bullet)
    {
        health -= damage;
        Debug.Log("Robot Health: " + health);
        if (health <= fleeHealth)
        {
            SetState(new RobotFlee(this));
        }

        if (health <= 0)
        {
            if (bullet.CompareTag("playerOne")) {
                Debug.Log("Player One Scored");
                GameObject playerOne = GameObject.FindGameObjectWithTag("playerOne");
                PlayerScore playerOneScore = playerOne.GetComponent<PlayerScore>();
                if (playerOneScore != null) {
                    playerOneScore.GetComponent<PlayerScore>().AddScore(10);
                }
            } else if (bullet.CompareTag("playerTwo")) {
                Debug.Log("Player Two Scored");
                GameObject playerTwo = GameObject.FindGameObjectWithTag("playerTwo");
                PlayerScore playerTwoScore = playerTwo.GetComponent<PlayerScore>();
                if (playerTwoScore != null) {
                    playerTwoScore.GetComponent<PlayerScore>().AddScore(10);
                }
            }
            Destroy(gameObject);
        }
    }
}
