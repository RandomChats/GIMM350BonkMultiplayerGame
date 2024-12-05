using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootCoolDown = 0.5f;
private float lastShootTime;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastShootTime + shootCoolDown && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Debug.Log("Player Shoot: ");
        lastShootTime = Time.time;

        GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(gun.transform.right * 10f, ForceMode.Impulse);
        }
    }

    
}
