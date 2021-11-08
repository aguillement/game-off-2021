using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    int direction = 1;
    public int bulletSpeed = 10;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ChangeDirection()
    {
        direction *= -1;
        
    }

    void Update()
    {
        rb.velocity = new Vector2(0, bulletSpeed * direction);
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);

        if (direction == 1)
        {
            // If bullet touch wall
            if (col.gameObject.tag == "bounds")
            {
                Destroy(gameObject);
            }

            if (col.gameObject.tag == "Enemy")
            {
                col.gameObject.GetComponent<Enemy>().Damage();
                Destroy(gameObject);
            }

            if (col.gameObject.tag == "EnemyBullet")
            {
                col.gameObject.GetComponent<Bullet>().Damage();
                Destroy(gameObject);
            }
        } else
        {
            // If bullet touch wall
            if (col.gameObject.tag == "boundsbottom")
            {
                Destroy(gameObject);
            }
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<Spaceship>().Damage();
                Destroy(gameObject);
            }
        }

    }

    public void Damage()
    {
        Destroy(gameObject);
    }
}