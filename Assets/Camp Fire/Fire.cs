using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fire : MonoBehaviour
{
    public bool open;
    public Transform transform;
    private Vector2 size;
    public Animator animator;
    public Light2D light;
    public ParticleSystem particleSystem;
    void Start()
    {
        light = GetComponentInChildren<Light2D>();
        light.intensity = 0f;
        size = new Vector2(1.5f, 1.5f);
        open = false;
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Pause(); 
    }

    void Update()
    {
        Vector2 overlapBoxSize = new Vector2(size.x*transform.lossyScale.x, size.y*transform.lossyScale.y);
        Collider2D[] hitInfo = Physics2D.OverlapBoxAll(transform.position, overlapBoxSize, transform.eulerAngles.z);
        foreach (Collider2D hit in hitInfo)
        {
            if (hit.gameObject.tag == "Player")
            {
                if (open == false)
                {
                    particleSystem.Play();
                    light.intensity = 3f;
                    animator.SetBool("isOn", true);
                    transform.position = transform.position + new Vector3(0f, 0.4f, 0f);
                }
                open = true;
            }
        }
    }
}
