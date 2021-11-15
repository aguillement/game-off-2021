using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusShield     : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private GameObject player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -1) * speed;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Spaceship>().ResetShieldDurationTmp();
            col.gameObject.GetComponent<Spaceship>().isShieldActivated = true;
            Destroy(gameObject);
        }
    }
}