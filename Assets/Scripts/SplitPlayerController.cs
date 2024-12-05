using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SplitPlayerController : MonoBehaviour
{
//starting
    private bool gameStarted = false;
    private int PlayerNumber;

//jumping
    private Vector3 jumpForce = new Vector3(0, 5, 0);
    private bool isJumping = false;

//movement
    private float moveSpeed = 10;
    private Vector3 movementDirection;
    public InputActionReference movementP1;
    public InputActionReference movementP2;
    public InputActionAsset player2M;

//looking
    private float rotationSpeed = 50;
    private Vector3 lookDirection;
    public InputActionReference lookP1;
    public InputActionReference lookP2;

//shooting
    public GameObject bulletPrefab;
    public float shootCoolDown = 0.5f;
    public InputActionReference shootP1;
    public InputActionReference shootP2;
    public Transform bulletSpawn;

    public void OnLook()
    {
        switch (PlayerNumber)
        {
            case 0:
                lookDirection = lookP1.action.ReadValue<Vector2>();
                break;
            case 1:
                lookDirection = lookP2.action.ReadValue<Vector2>();
                break;
        }
    }

    public void OnJump()
    {
        if (!gameStarted)
        {
            PlacePlayer();
        }
        else
        {
            isJumping = true;
            this.gameObject.GetComponent<Rigidbody>().AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    public void OnMove()
    {
        switch (PlayerNumber)
        {
            case 0:
                movementDirection = movementP1.action.ReadValue<Vector2>();
                break;
            case 1:
                movementDirection = movementP2.action.ReadValue<Vector2>();
                break;
        }
    }

    public void OnShoot()
    {
        switch (PlayerNumber)
        {
            case 0:
                if (Time.time > shootCoolDown && shootP1.action.triggered)
                {
                    Shoot();
                }
                break;
            case 1:
                if (Time.time > shootCoolDown && shootP2.action.triggered)
                {
                    Shoot();
                }
                break;
        }
    }

    public void Shoot()
    {
        Debug.Log("Player that shot is: " + this.gameObject.tag);
        shootCoolDown = Time.time + 0.5f;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bullet.gameObject.tag = this.gameObject.tag;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * 50, ForceMode.Impulse);
        }
    }

    private void PlacePlayer()
    {
        var pm = GameObject.Find("PlayerManager");
        PlayerNumber = pm.GetComponent<PlayerManager>().CheckTotalPlayers();

        switch (PlayerNumber)
        {
            case 0:
                transform.position = new Vector3(-5, 1, 0);
                this.gameObject.name = "Player1";
                this.gameObject.tag = "playerOne";
                pm.GetComponent<PlayerManager>().SpawnPlayer1();
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                //GameObject.Find("Body").GetComponent<RobotStateController>().SetPlayer(this.gameObject);
                break;
            case 1:
                transform.position = new Vector3(5, 1, 0);
                this.gameObject.name = "Player2";
                this.gameObject.tag = "playerTwo";
                pm.GetComponent<PlayerManager>().SpawnPlayer2();
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                this.gameObject.GetComponent<PlayerInput>().actions = player2M;
                break;
        }

        pm.GetComponent<PlayerManager>().AddPlayer();
        gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Vector3.forward * movementDirection.y * moveSpeed * Time.deltaTime);
        this.gameObject.transform.Translate(Vector3.right * movementDirection.x * moveSpeed * Time.deltaTime);

        this.gameObject.transform.Rotate(Vector3.up * lookDirection.x * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isJumping)
        {
            if (collision.collider.name == "Floor")
            {
                isJumping = false;
            }
        }
    }
}
