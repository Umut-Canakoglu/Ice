using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Transform transform;
    private Vector2 direction;
    public float ymin;
    private bool start;
    void Start() {
        transform = GetComponent<Transform>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.drag = 0f;
        start = false;
    }
    void Update()
    {
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(transform.position, 3f);  
        foreach (Collider2D hit in hitInfo)
        {
            if (hit.gameObject.tag == "Player" && start == false)
            {
                start = true;
                direction = hit.gameObject.transform.position - transform.position;
                rb2D.velocity = new Vector2(Mathf.Sign(direction.x)*5f, 0f);
            }
        }
        if (transform.position.y <= ymin)
        {
            Destroy(gameObject);
        }
    }
}
