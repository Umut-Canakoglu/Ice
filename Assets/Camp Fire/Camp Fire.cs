using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CampFire : MonoBehaviour
{
    private bool playerIn;
    public BoxCollider2D boxCollider;
    public Transform transform;
    public float heatTime;
    public float freezeTime;
    public Sprite[] sprites; 
    public Image image;
    private int currentIndex;
    void Start()
    {
        currentIndex = 3;
        transform = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
        image.sprite = sprites[currentIndex];;
    }
    void Update() 
    {
        playerIn = false;
        Vector2 overlapBoxCenter = (Vector2)transform.position + boxCollider.offset;
        Vector2 overlapBoxSize = boxCollider.size;
        Collider2D[] hitInfo = Physics2D.OverlapBoxAll(overlapBoxCenter, overlapBoxSize, transform.eulerAngles.z);
        foreach (Collider2D hit in hitInfo)
        {
            if (hit.gameObject.tag == "CampFire")
            {
                playerIn = true;
            }
        }
        if (playerIn == true)
        {
            freezeTime = 0;
            heatTime += Time.deltaTime;
        } else if (playerIn == false)
        {
            heatTime = 0;
            freezeTime += Time.deltaTime;
        }
        if (freezeTime >= 25f)
        {
            if (currentIndex == 0)
            {
                Destroy(gameObject);
            } else {
                image.sprite = sprites[currentIndex-1];
            }
            freezeTime = 0;
        }
        if (heatTime >= 25f)
        {
            if (currentIndex == 3){} else {
                image.sprite = sprites[currentIndex+1];
            }
            heatTime = 0;
        }
    }
}
