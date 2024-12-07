using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWinChecker : MonoBehaviour {
  private int count;
  private GameObject playerOneScore;
  private GameObject playerTwoScore;

  void Start() {
    RobotStateController[] enemyCount = FindObjectsOfType<RobotStateController>();

    foreach (RobotStateController enemy in enemyCount) {
      count++;
    }

    playerOneScore = GameObject.FindGameObjectWithTag("playerOne");
    playerTwoScore = GameObject.FindGameObjectWithTag("playerTwo");
  }


 /* void update() {
    if (count == 0) {
      if(playerOneScore.GetComponent<PlayerScore>().score > playerTwoScore.GetComponent<PlayerScore>().score) {
        SceneManager.LoadScene(playerOneWin);
      } else if(playerOneScore.GetComponent<PlayerScore>().score < playerTwoScore.GetComponent<PlayerScore>().score) {
        SceneManager.LoadScene(playerOneWin);
      }
      
    }
  }*/
}