using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour {
    public float health;
    public float maxHealth = 100f;
    public TextMeshProUGUI healthText;
    public Vector3 healthTextOffset;
    public Camera playerCamera;
    public bool isDead = false;
    public GameObject gun;

    public TextMeshProUGUI youDiedText;
    public Vector3 deathTextOffset;

    private SplitPlayerController playerController;
    private GameObject NovaEventManager;

    // Start is called before the first frame update
    void Start() {
        ResetHealth();
        playerController = GetComponent<SplitPlayerController>();
        NovaEventManager = GameObject.Find("NovaEventManager");
    }

    void OnEnable() {
        ResetHealth();
    }

    void Update() {
        if (healthText != null) {
            healthText.text = "Health: " + health;
            UpdateHealthTextPosition();
            UpdateDeathTextPosition();
        }
    }

    public void TakeDamage(float amount) {
        Debug.Log("Player took damage. Player health = " + health);
        health -= amount;
        if (health <= 0) {
            
            Die();
        }
    }

    public void ResetHealth() {
        health = maxHealth;
    }

    private void Die() {
        NovaEventManager.GetComponent<PlayerWinChecker>().PlayersAlive -= 2;
        if (playerController != null) {
            playerController.enabled = false;
        }

        if (gun != null) {
            gun.SetActive(false);
        }

        if (youDiedText != null) {
            youDiedText.text = "You Died!";
            youDiedText.gameObject.SetActive(true);
        }

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        healthText.text = "Health: 0 (Dead)";
        isDead = true;
    }

    private void UpdateHealthTextPosition() {
        if (playerCamera != null && healthText != null) {
            Vector3 screenPosition = playerCamera.WorldToScreenPoint(transform.position + healthTextOffset);
            healthText.transform.position = screenPosition;
        }
    }

    private void UpdateDeathTextPosition() {
        if (playerCamera != null && youDiedText != null) {
            Vector3 screenPosition = playerCamera.WorldToScreenPoint(transform.position + deathTextOffset);
            youDiedText.transform.position = screenPosition;
        }
    }
}