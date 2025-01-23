using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private float normalDrag;
    private float iceDrag = 0f;
    private float maxIceSpeed = 20f;
    private bool onIce;
    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        normalDrag = GetComponent<Rigidbody2D>().drag;
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
        if (onIce == true)
        {
            if (rb2D.velocity.magnitude < maxIceSpeed)
            {
                rb2D.AddForce(rb2D.velocity.normalized * 5f, ForceMode2D.Force);
            }
        }
    }
}
