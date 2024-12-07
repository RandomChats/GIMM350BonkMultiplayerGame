using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWinChecker : MonoBehaviour {
    public int count;
    [SerializeField] private Scene playerOneWin;
    [SerializeField] private Scene playerTwoWin;
    [SerializeField] private Scene playerLose;

    public int PlayersAlive;
    public int player1Score;
    public int player2Score;


    void Start() {
        PlayersAlive = 2;
        RobotStateController[] enemyCount = FindObjectsOfType<RobotStateController>();

        foreach (RobotStateController enemy in enemyCount) {
            count++;
        }
    }


    void Update() {
       if (count == 0) {
         if(player1Score > player2Score) {
           SceneManager.LoadScene(1);
         } else if(player1Score < player2Score) {
           SceneManager.LoadScene(2);
         }
       }

       if (PlayersAlive == 0) {
           SceneManager.LoadScene(3);
       }
     }
}