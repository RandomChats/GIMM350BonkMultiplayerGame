using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 5f;

    void Start()
    {
        StartCoroutine(DestroyBullet());
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<RobotStateController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
