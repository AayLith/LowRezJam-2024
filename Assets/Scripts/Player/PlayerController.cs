using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    
    public float jumpForce = 5;
    public LayerMask groundLayer;
    private float checkDistance = 5f; // Distance to check for ground

    private Rigidbody2D rb;
    private float moveInputX;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInputX * moveSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);
    }*/
}
