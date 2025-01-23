using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 50f;

    private float horizontal;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private float maxIceSpeed = 8f;
    private bool onIce;
    private float iceMoveMulitplier;
    private float iceBaseSpeed = 2f;
    private float velocityY;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        onIce = false;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Slippy")
        {
            onIce = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Slippy")
        {
            onIce = false;
        }
    }
   
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        velocityY = rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            velocityY = jumpForce;
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y>0)
        {
            velocityY = rb.velocity.y * 0.5f;
        }
        if (onIce == true)
        {
            if (horizontal != 0)
            {
                iceMoveMulitplier += (Time.deltaTime * horizontal*1.5f);
            } else
            {
                if (iceMoveMulitplier < 0)
                {
                    iceMoveMulitplier += Time.deltaTime;
                    if (iceMoveMulitplier > 0)
                    {
                        iceMoveMulitplier = 0;
                    }
                } else if (iceMoveMulitplier > 0)
                {
                    iceMoveMulitplier -= Time.deltaTime;
                    if (iceMoveMulitplier < 0)
                    {
                        iceMoveMulitplier = 0;
                    }
                }
            }
            if (iceMoveMulitplier > maxIceSpeed)
            {
                iceMoveMulitplier = maxIceSpeed;
            } else if (iceMoveMulitplier < -maxIceSpeed){
                iceMoveMulitplier = -maxIceSpeed;
            }
        } else {
            iceMoveMulitplier = iceBaseSpeed*horizontal;
        }
        if (onIce == true)
        {
            rb.velocity = new Vector2((speed*iceMoveMulitplier), velocityY);
        } else {
            rb.velocity = new Vector2((speed*horizontal), velocityY);
        }
        Flip();
    }
    private bool isGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }
    private void Flip()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
