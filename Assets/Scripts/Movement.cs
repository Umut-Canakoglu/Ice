using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    private float speedMultiplier = 1.2f;
    private float walkSpeed = 6f;
    public float jumpForce = 30f;
    public int jumpAmount;
    private float horizontal;
    public Animator animator;
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
        speed = walkSpeed;
        animator = GetComponent<Animator>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                jumpAmount = 2;
            }
            if (jumpAmount > 0)
            {
                jumpAmount -= 1;
                velocityY = jumpForce;
            }
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y>0)
        {
            velocityY = rb.velocity.y * 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("isSprint", true);
            speed = walkSpeed*speedMultiplier;
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isSprint", false);
            speed = walkSpeed;
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
        animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
        if (transform.position.y <= -14f)
        {
            Destroy(gameObject);
        }
    }
    private bool isGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.23f, groundLayer);
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green; 
        int segments = 100; 
        float angleStep = 360f / segments;
        Vector3 prevPoint = groundCheck.position + new Vector3(0.23f, 0, 0);
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = groundCheck.position + new Vector3(Mathf.Cos(angle) * 0.23f, Mathf.Sin(angle) * 0.23f, 0);
            Gizmos.DrawLine(prevPoint, newPoint); 
            prevPoint = newPoint; 
        }
    }
}
