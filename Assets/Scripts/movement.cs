using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class movement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float curr_speed = 0f;
    private float acceleration = .3f;

    public float jump_power = 16f;
    public int jump_count = 1;

    private bool isRight = true;
    private bool is_pressed = false;
    private bool in_air = false;
    private float decelerate = .5f;

    public int armor;
    public string spawn_file;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        float x, y;
        StreamReader sr = new StreamReader("Assets/Scripts/Data/" + spawn_file);
        sr.BaseStream.Seek(0, SeekOrigin.Begin);

        string line = sr.ReadLine();
        x = float.Parse(line);
        line = sr.ReadLine();
        y = float.Parse(line);

        transform.position = new Vector2(x, y);

        sr.Close();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") == false || rb.velocity.y < 0)
        {
            rb.gravityScale = 5;
            is_pressed = false;
        }
        else
            rb.gravityScale = 2.5f;
        if(rb.velocity.y < 2f && rb.velocity.y > 0 && !isGrounded())
        {
            rb.gravityScale = 2f;
        }
        if (Input.GetButton("Jump") && is_pressed == false && jump_count != 0)
        {
            is_pressed = true;
            in_air = true;
            rb.velocity = new Vector2(rb.velocity.x*.9f, jump_power);
            jump_count--;
            
        }
        if (isGrounded() && in_air)
        {
            in_air = false;
            is_pressed = false;
            
            jump_count++;
        }



        Flip();
    }

    private void FixedUpdate()
    {
        
        
        if(curr_speed > speed)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        if(in_air && curr_speed < speed)
        {
            curr_speed += acceleration;
            rb.velocity = new Vector2(rb.velocity.x + (acceleration * horizontal)*.75f, rb.velocity.y);
        }
        if(rb.velocity.x > 13f)
        {
            rb.velocity = new Vector2(rb.velocity.x - decelerate, rb.velocity.y);
        }
        else if (rb.velocity.x < -13f)
        {
            rb.velocity = new Vector2(rb.velocity.x + decelerate, rb.velocity.y);
        }
        else if (horizontal == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * .8f, rb.velocity.y);
            curr_speed = 0; 
            acceleration = .3f;
        }
        else if (curr_speed < speed)
        {
            curr_speed += acceleration;
            rb.velocity = new Vector2(rb.velocity.x+acceleration*horizontal, rb.velocity.y);
            acceleration += .1f;
        }
        else if (curr_speed > speed)
        {
            rb.velocity = new Vector2(curr_speed*horizontal, rb.velocity.y);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if(isRight && horizontal < 0f || !isRight && horizontal > 0f)
        {
            isRight = !isRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
