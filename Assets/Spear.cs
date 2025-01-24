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
        speed = 4f;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        direction = new Vector2(player.transform.localScale.x, 0f).normalized;
        rb.velocity = direction * speed;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Spike")
        {
            GameObject[] spears = GameObject.FindGameObjectsWithTag("Spear");
            foreach (GameObject obj in spears)
            {
                obj.name = "Spear1";
            }
            Destroy(gameObject);
        }
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Spear")
        {
            rb.velocity = new Vector2(0f,0f);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
