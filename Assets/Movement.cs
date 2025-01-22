using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 4f;
    public float jumpForce = 6f;

    private float horizontal;

    public Transform groundCheck;
    public LayerMask groundLayer;
    

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);

         if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y>0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool isGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }
}
