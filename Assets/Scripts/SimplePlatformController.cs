using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour {

    //Fields hidden from inspector
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;

    //Public fields
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 750f;
    public Transform groundChecker1;
    public Transform groundChecker2;
    public Transform wallChecker1;
    public Transform wallChecker2;
    //Private fields
    private bool grounded = false;
    private bool blocked = false;
    private Animator animat;
    private Rigidbody2D rb2d;


	void Awake () {
        animat = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        grounded = Physics2D.OverlapArea(groundChecker1.position, groundChecker2.position, 1 << LayerMask.NameToLayer("Ground"));
        blocked = Physics2D.OverlapArea(wallChecker1.position, wallChecker2.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
	}

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput*rb2d.velocity.x < maxSpeed && !(!grounded && blocked))
        {
            rb2d.AddForce(Vector2.right * horizontalInput * moveForce);
        }

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x)*maxSpeed, rb2d.velocity.y);
        }

        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        if (jump)
        {
            animat.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        animat.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x)/maxSpeed);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1;
        transform.localScale = tempLocalScale;
    }
}
