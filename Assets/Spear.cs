using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private GameObject player;
    private float speed;
    public Rigidbody2D rb;
    private Vector2 direction;
    void Start()
    {
        speed = 3f;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = new Vector2(player.transform.localScale.x, 0f).normalized;
        rb.velocity = direction * speed;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            rb.velocity = new Vector2(0f,0f);
        }
    }
}
