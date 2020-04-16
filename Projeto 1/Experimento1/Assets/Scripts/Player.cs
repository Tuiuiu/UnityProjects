using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontalSpeed = 3.0f;
    public float jumpForce = 5.0f;
    private bool onGround = true;

    public Rigidbody2D rb;
    private JumpSensor sensor;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sensor = transform.Find("Sensor").GetComponent<JumpSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onGround && sensor.State())
        {
            onGround = true;
        }

        float inputX = Input.GetAxisRaw("Horizontal");

        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }


        if (Input.GetButton("Jump") && onGround){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            onGround = false;
            sensor.Disable(0.2f);
        }
        else if (inputX != 0)
        {
            anim.SetInteger("AnimState", 2);
        }
        else
        {
            anim.SetInteger("AnimState", 0);
        }
        rb.velocity = new Vector2(inputX * horizontalSpeed, rb.velocity.y);
              
    }
}
