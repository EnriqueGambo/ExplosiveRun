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

    private float time;
    public Vector2 pos;
    public int armor = 0;
    public string spawn_file;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D wallcheck;
    [SerializeField] private UnityEngine.UI.Button[] directions = new UnityEngine.UI.Button[2];
    [SerializeField] private UnityEngine.UI.Button jump_button;

    private Stopwatch sw = new Stopwatch();
    private Stopwatch sw2 = new Stopwatch();
    private bool walled = false;
    public bool mobile;
    private bool jump = false;
    // Start is called before the first frame update

    // Update is called once per frame
    private bool already_jumped;
    public bool started;
    public int up;
    void Update()
    { 
        if (started)
        {
            started = false;
            transform.position = new Vector2(pos.x, pos.y + up);
            UnityEngine.Debug.Log("Hello");
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButton("Jump");

        if(jump_count < 0)
            jump_count = 0;
        if(armor <  0)
            armor = 0;
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
        if (stop)
        {
            stopfall(time);
            return;
        }
            

        if (isWalled() && !isGrounded())
        {
            walled = true;
            Wall_Jump();
        }
        else if(walled)
        {
            sw2.Start();
            if(sw2.ElapsedMilliseconds > 200)
            {
                sw2.Reset();
                sw2.Stop();
                walled = false;
            }
        }

        if (is_bounce)
            return;
        if(!jump)
            is_pressed = false;
        if (jump == false || rb.velocity.y < 0)
        {
            rb.gravityScale = 5;
        }
        else
            rb.gravityScale = 2.5f;
        if(rb.velocity.y < 2f && rb.velocity.y > 0 && !isGrounded())
        {
            rb.gravityScale = 2f;
        }
        if(is_wall_jump && jump) { }
        else 
                is_wall_jump = false;

    
        if (jump && is_pressed == false && jump_count != 0 && !is_bounce && !is_wall_jump)
        {
            is_pressed = true;
            in_air = true;
            rb.velocity = new Vector2(rb.velocity.x*.9f, jump_power);
            jump_count--;
            already_jumped = true;
        }
        if (isGrounded())
            already_jumped = false;
        if (isGrounded() && in_air)
        {
            in_air = false;
            is_pressed = false;
            
            jump_count++;
        }

        if (!isGrounded() && !already_jumped)
        {
            sw.Start();
            if (sw.ElapsedMilliseconds > 900)
            {
                in_air = true;
                jump_count--;
                sw.Reset();
                sw.Stop();
            }
        }

        Flip();
    }

    Stopwatch Stopwatch = new Stopwatch();
    private bool is_bounce = false;
    private void FixedUpdate()
    {
        if (is_bounce)
        {
            Stopwatch.Start(); 
            acceleration = 0;
        }
        if(Stopwatch.ElapsedMilliseconds > 200)
        {
            Stopwatch.Reset();
            Stopwatch.Stop();
            is_bounce = false;
            acceleration = .3f;
        }
        if (stop)
        {
            return;
        }

        if(in_air && curr_speed < speed && horizontal != 0)
        {
            curr_speed += acceleration;
            rb.velocity = new Vector2(rb.velocity.x + (acceleration * horizontal)*.9f, rb.velocity.y);
        }
        if(rb.velocity.x > speed * 2f)
        {
            rb.velocity = new Vector2(rb.velocity.x - decelerate, rb.velocity.y);
        }
        else if (rb.velocity.x < -speed * 2f)
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
        
        return Physics2D.OverlapCircle(groundcheck.position, .2f, groundLayer);
    }

    private bool isWalled()
    { 
        if (Physics2D.IsTouchingLayers(wallcheck, groundLayer) && horizontal != 0)
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

    private bool is_wall_jump = false;
    private void Wall_Jump()
    {
        rb.velocity = new Vector2(0f, .0f);
        rb.gravityScale = 0f;

        if (Physics2D.IsTouchingLayers(wallcheck, groundLayer) && jump && !is_pressed && horizontal > 0)
        {
            rb.velocity = new Vector2(-8f, jump_power*.75f);
            is_bounce = true;
            is_wall_jump = true;
        }
        else if (Physics2D.IsTouchingLayers(wallcheck, groundLayer) && jump && !is_pressed && horizontal < 0)
        {
            rb.velocity = new Vector2(8f, jump_power * .75f);
            is_bounce = true;
            is_wall_jump = true;
        }
    }
    Stopwatch sj = new Stopwatch();
    public void stopfall(float seconds)
    {
        sj.Start();
        UnityEngine.Debug.Log("Is active");
        rb.gravityScale = -.1f;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
       
        
        if(sj.ElapsedMilliseconds > time * 1000)
        {
            stop = false;
            sj.Stop();
            sj.Reset();
        }
        
    }
    private bool stop = false;
    public void wait(float seconds)
    {
        time = seconds;
        stop = true;
    }
}
