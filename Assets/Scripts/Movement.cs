using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 4f;
    public float jumpForce = 6f;
    public int jumpAmount = 0;

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

        if (Input.GetKeyDown(KeyCode.Space) && jumpAmount != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpAmount -= 1;
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y>0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D  collision){
        if(collision.gameObject.tag == "Ground"){
            jumpAmount = 2;
        }
    }
    private bool isGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }
}
