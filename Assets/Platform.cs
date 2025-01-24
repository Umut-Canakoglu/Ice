using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public PlatformEffector2D platformEffector;
    public Transform transform;
    public float waitTime;
    public bool playerHere;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        transform = GetComponent<Transform>();
        platformEffector = GetComponent<PlatformEffector2D>();
    }
    void Update() {
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            waitTime = 0.5f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector2 overlapBoxCenter = new Vector2(transform.position.x + boxCollider.offset.x, transform.position.y + boxCollider.offset.y);
            Vector2 overlapBoxSize = new Vector2(boxCollider.size.x*transform.lossyScale.x, boxCollider.size.y*transform.lossyScale.y);
            Collider2D[] hitInfo = Physics2D.OverlapBoxAll(overlapBoxCenter, overlapBoxSize, transform.eulerAngles.z);
            playerHere = false;
            foreach (Collider2D hit in hitInfo)
            {
                if (hit.gameObject.tag == "Player")
                {
                    playerHere = true;
                }
            }
            if (playerHere == true)
            {
                if (waitTime <= 0)
                {
                    platformEffector.rotationalOffset = 180f;
                    waitTime = 0.5f;
                } else {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            platformEffector.rotationalOffset = 0f;
        }
    }
    void OnDrawGizmos()
    {
        Vector2 overlapBoxCenter = new Vector2(transform.position.x, transform.position.y);
        Vector2 overlapBoxSize = new Vector2(boxCollider.size.x*transform.lossyScale.x, boxCollider.size.y*transform.lossyScale.y);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(overlapBoxCenter, overlapBoxSize);
    }
}
