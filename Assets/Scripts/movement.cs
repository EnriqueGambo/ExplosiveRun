using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using System.IO;
using System.Diagnostics;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    private float decelerate = .25f;

    public int armor = 0;
    public string spawn_file;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform lefcheck;
    [SerializeField] private Transform rightcheck;
    [SerializeField] private Collider2D bumpcheck;
    [SerializeField] private UnityEngine.UI.Button[] directions = new UnityEngine.UI.Button[2];
    [SerializeField] private UnityEngine.UI.Button jump_button;

    private Stopwatch sw = new Stopwatch();
    private Stopwatch sw2 = new Stopwatch();
    private bool walled = false;
    public bool mobile;
    private bool jump = false;
    // Start is called before the first frame update

    // Update is called once per frame
    private Ray ray;
    private RaycastHit hit;
    void Start()
    {
        hit = GetComponent<RaycastHit>();
        ray = new Ray(transform.position, Vector2.down);
        //ray.direction = new Vector2(1, -1);]
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
        ray.origin = new Vector3(0, 0);
      
       // RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, .5f, groundLayer);
        if (Physics.Raycast(ray, out hit, 1f, groundLayer))
        {
            UnityEngine.Debug.Log("It hits");
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButton("Jump");

        if(jump_count < 0)
            jump_count = 0;
        if (mobile)
        {
            ButtonHold jumpButton = jump_button.GetComponent<ButtonHold>();
            jump = jumpButton.buttonPressed;

            if (directions[0].GetComponent<ButtonHold>().buttonPressed) {
                horizontal = -1;
            }
            else if (directions[1].GetComponent<ButtonHold>().buttonPressed)
            {
                horizontal = 1;
            }
        }
        
        if(isWalled() && !isGrounded())
        {
            walled = true;
            Wall_Jump();
        }
        else if(walled)
        {
            sw2.Start();
            if(sw2.ElapsedMilliseconds > 200)
            {
                sw2.Restart();
                walled = false;
            }
        }

        if (jump == false || rb.velocity.y < 0)
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
        if (jump && is_pressed == false && jump_count != 0)
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
        if(rb.velocity.y < 0 && !in_air)
        {
            sw.Start();
            if (sw.ElapsedMilliseconds > 1000)
            {
                in_air = true;
                jump_count--;
                sw.Restart();
            }
        }

        Flip();
    }

    private void FixedUpdate()
    {    
        if(in_air && curr_speed < speed)
        {
            curr_speed += acceleration;
            rb.velocity = new Vector2(rb.velocity.x + (acceleration * horizontal)*.9f, rb.velocity.y);
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
        else if (curr_speed < speed && !walled)
        {
            curr_speed += acceleration;
            rb.velocity = new Vector2(rb.velocity.x+acceleration*horizontal, rb.velocity.y);
            acceleration += .1f;
        }
        else if (curr_speed > speed)
        {
            if ((rb.velocity.x < 0) != (horizontal < 0))
            {
                rb.velocity = new Vector2(rb.velocity.x *.7f, rb.velocity.y);
                curr_speed *= .7f;
            }
            else
            {
                rb.velocity = new Vector2(curr_speed * horizontal, rb.velocity.y);
            }
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundLayer);
    }

    private bool isWalled()
    {
        LayerMask temp = groundLayer;
        if(Physics2D.OverlapCircle(lefcheck.position, 0.2f, temp) && horizontal < 0)
            return true;
        else if (Physics2D.OverlapCircle(rightcheck.position, 0.2f, groundLayer) && horizontal > 0)
            return true;
        return false;
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
    private void Wall_Jump()
    {
        if (rb.velocity.y != 0f)
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.gravityScale = 0f;

        if (Physics2D.OverlapCircle(rightcheck.position, 0.2f, groundLayer)  && jump && !is_pressed)
            rb.velocity = new Vector2(-speed * .75f, jump_power);
        if (Physics2D.OverlapCircle(lefcheck.position, 0.2f, groundLayer) && jump && !is_pressed)
            rb.velocity = new Vector2(speed * .75f, jump_power);
    }
}
