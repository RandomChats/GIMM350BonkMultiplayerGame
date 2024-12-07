using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
  private int NumberOfPlayers = 0;

  public PlayerHealth player1Health;
  public PlayerHealth player2Health;
  public PlayerScore player1Score;
  public PlayerScore player2Score;

  public TextMeshProUGUI player1HealthText;
  public TextMeshProUGUI player1DeathText;
  public TextMeshProUGUI player2HealthText;
  public TextMeshProUGUI player2DeathText;
  public Camera player1Camera;
  public Camera player2Camera;
  public TextMeshProUGUI player1ScoreText;
  public TextMeshProUGUI player2ScoreText;
  public GameObject NovaEventSystem;

  public GameObject playerTwoCanvasThing;
  public int CheckTotalPlayers() {
    return NumberOfPlayers;
  }

  public void AddPlayer() {
    NumberOfPlayers++;
  }

  public void SpawnPlayer1() {
    GameObject player1Object = new GameObject("Player1");
    player1Health = player1Object.AddComponent<PlayerHealth>();
    PlayerScore player1Score = player1Object.AddComponent<PlayerScore>();
    player1Health.healthText = player1HealthText;
    player1Health.youDiedText = player1DeathText;
    player1Score.scoreText = player1ScoreText;
    player1Score.playerCamera = player1Camera;
    player1Health.playerCamera = player1Camera;
    player1Health.healthTextOffset = new Vector3(0, 2, 0);
    player1Score.scoreTextOffset = new Vector3(0, 1, 0);
    player1Health.deathTextOffset = new Vector3(0, 0, 0);
    NovaEventSystem.GetComponent<PlayerWinChecker>().PlayersAlive++;

    SetPlayerScoreForEnemy(player1Score, "Player1");
    Debug.Log("Player 1 Score and Camera assigned.");
  }

  public void SpawnPlayer2() {
    GameObject player2Object = new GameObject("Player2");
    player2Health = player2Object.AddComponent<PlayerHealth>();
    PlayerScore player2Score = player2Object.AddComponent<PlayerScore>();
    player2Health.healthText = player2HealthText;
    player2Health.youDiedText = player2DeathText;
    player2Score.scoreText = player2ScoreText;
    player2Score.playerCamera = player2Camera;
    player2Health.playerCamera = player2Camera;
    player2Health.healthTextOffset = new Vector3(0, 2, 0);
    player2Score.scoreTextOffset = new Vector3(0, 1, 0);
    player2Health.deathTextOffset = new Vector3(0, 0, 0);
    NovaEventSystem.GetComponent<PlayerWinChecker>().PlayersAlive++;
    playerTwoCanvasThing.SetActive(false);

    SetPlayerScoreForEnemy(player2Score, "Player2");
    Debug.Log("Player 2 Score and Camera assigned.");
  }

  private void SetPlayerScoreForEnemy(PlayerScore playerScore, string playerName) {
    RobotStateController[] enemies = FindObjectsOfType<RobotStateController>();
    foreach (RobotStateController enemy in enemies) {
      if (enemy.TargetPlayerName == playerName) {
        enemy.SetPlayerScore(playerScore);
      }
    }
  }
}