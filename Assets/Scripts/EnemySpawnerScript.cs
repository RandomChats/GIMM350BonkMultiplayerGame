using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour {
  // Start is called before the first frame update
  [SerializeField] private GameObject enemy;
  [SerializeField] private Transform enemySpawnPoint;

  [SerializeField] private GameObject player;
  [SerializeField] private GameObject playerPrefab;
  [SerializeField] private GameObject playerPrefab2;

  void Start() {
    if (playerPrefab != null) {
      player = Instantiate(playerPrefab);
    }

    if (playerPrefab2 != null) {
      player = Instantiate(playerPrefab2);
    }

    enemySpawnPoint = this.gameObject.transform;
    StartCoroutine(BeginSpawning());
  }

  private void SpawnEnemy() {
    Instantiate(enemy, enemySpawnPoint);
  }

  IEnumerator BeginSpawning() {
    yield return new WaitForSeconds(3);
    SpawnEnemy();
  }
}