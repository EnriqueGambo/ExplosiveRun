using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jump_power = 16f;
    public int jump_count = 1;
    public int jcounter = 1;
    private bool isRight = true;
    private bool is_pressed = false;
    private bool in_air = false;
    public int armor;
    public Vector2 Spawn;

    private float acceleration = .01f;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        transform.position = Spawn;
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") == false)
        {
            is_pressed = false;
        }
        if(Input.GetButton("Jump") && is_pressed == false && jump_count != 0)
        {
            is_pressed = true;
            in_air = true;
            rb.velocity = new Vector2(rb.velocity.x, jump_power);
            jump_count--;
            
        }
        if (isGrounded() && in_air)
        {
            in_air = false;
            is_pressed = false;
            if (jump_count == 0 && jcounter != 1)
            {
                jump_count += jcounter;
                jcounter--;
            }
            else
                jump_count++;
        }



        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
