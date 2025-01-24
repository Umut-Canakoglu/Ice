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
    public float radius = 5f;
    void Start() {
        transform = GetComponent<Transform>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.drag = 0f;
        start = false;
        radius = 10f;
    }
    void Update()
    {
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(transform.position, radius);  
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
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green; 
        int segments = 100; 
        float angleStep = 360f / segments;
        Vector3 prevPoint = transform.position + new Vector3(radius, 0, 0);
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Gizmos.DrawLine(prevPoint, newPoint); 
            prevPoint = newPoint; 
        }
    }
}
