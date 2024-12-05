using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public Vector3 scoreTextOffset;
    public Camera playerCamera;
    public int CurrentScore {get; set;}


    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            UpdateScoreTextPosition();
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

     private void UpdateScoreTextPosition()
    {
        if (playerCamera != null && scoreText != null)
        {
            Vector3 screenPosition = playerCamera.WorldToScreenPoint(transform.position + scoreTextOffset);
            scoreText.transform.position = screenPosition;
        }
    }
}
