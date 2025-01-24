using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CamperFire : MonoBehaviour
{
    private bool playerIn;
    public GameObject fireNews;
    private float waitTimeFire;
    public GameObject freezeNews;
    public GameObject tutorial;
    private float waitTimeFreeze;
    public BoxCollider2D boxCollider;
    public Transform transform;
    public float heatTime;
    public float freezeTime;
    public Sprite[] sprites; 
    public Image image;
    private int currentIndex;
    public Animator animator;
    private float waitTimeTutorial;
    void Start()
    {
        waitTimeTutorial = 2f;
        tutorial.SetActive(true);
        waitTimeFire = 0f;
        waitTimeFreeze = 0f;
        animator = GetComponent<Animator>();
        currentIndex = 4;
        transform = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
        image.sprite = sprites[currentIndex];;
    }
    void Update() 
    {
        if (waitTimeTutorial > 0)
        {
            waitTimeTutorial -= Time.deltaTime;
            if (waitTimeTutorial <= 0)
            {
                tutorial.SetActive(false);
            }
        }
        playerIn = false;
        Vector2 overlapBoxCenter = (Vector2)transform.position + boxCollider.offset;
        Vector2 overlapBoxSize = new Vector2(boxCollider.size.x*transform.lossyScale.x, boxCollider.size.y*transform.lossyScale.y);
        Collider2D[] hitInfo = Physics2D.OverlapBoxAll(overlapBoxCenter, overlapBoxSize, transform.eulerAngles.z);
        foreach (Collider2D hit in hitInfo)
        {
            if (hit.gameObject.tag == "CampFire" && hit.gameObject.GetComponent<Fire>().open == false)
            {
                fireNews.SetActive(true);
                waitTimeFire = 2f;
            } else if (hit.gameObject.tag == "CampFire" && hit.gameObject.GetComponent<Fire>().open == true)
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
        if (freezeTime >= 10f)
        {
            if (currentIndex == 0)
            {
                Destroy(gameObject);
            } else {
                image.sprite = sprites[currentIndex-1];
                currentIndex -= 1;
            }
            freezeTime = 0;
        }
        if (heatTime >= 10f)
        {
            if (currentIndex < 4)
            {
                image.sprite = sprites[currentIndex+1];
                currentIndex += 1;
            }
            heatTime = 0;
        }
        if (currentIndex == 1 && fireNews.activeSelf == false && playerIn == false)
        {
            freezeNews.SetActive(true);
        }
        if (waitTimeFire > 0)
        {
            waitTimeFire -= Time.deltaTime;
            if (waitTimeFire <= 0)
            {
                fireNews.SetActive(false);
            }
        }
        if (waitTimeFreeze > 0)
        {
            waitTimeFreeze -= Time.deltaTime;
            if (waitTimeFreeze <= 0)
            {
                freezeNews.SetActive(false);
            }
        }
    }
}