using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour {
    public int damage = 2;


    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    private void OnTriggerEnter(Collider c) {
        if (c.gameObject.tag == "playerOne") {
            Debug.Log("Player One Hit");
            c.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        else if (c.gameObject.tag == "playerTwo") {
            Debug.Log("Player Two Hit");
            c.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}