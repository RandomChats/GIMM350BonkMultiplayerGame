using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  Rigidbody rb;
  [SerializeField] float speed = 10f;

  [SerializeField] float Jump = 5f;

  // Start is called before the first frame update
  void Start() {
    rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update() {
    float hor = Input.GetAxis("Horizontal") * speed;
    float vert = Input.GetAxis("Vertical") * speed;

    rb.velocity = new Vector3(hor, rb.velocity.y, vert);

    if (Input.GetButtonDown("Jump") && Mathf.Approximately(rb.velocity.y, 0))
      rb.velocity = new Vector3(rb.velocity.x, Jump, rb.velocity.z);

    //turns the camera with the player but works weird dont know if we want to keep
    transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
  }
}