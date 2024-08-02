using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent ( typeof ( Rigidbody2D ) )]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 1;
    public float jumpForce = 50;
    public LayerMask groundLayer;
    private float checkDistance = 5; // Distance to check for ground

    private Rigidbody2D rb;
    private float moveInputX;
    private Vector2 lastPos;

    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer playerSprite;

    private void Awake ()
    {
        instance = this;
    }

    void Start ()
    {
        rb = this.GetComponent<Rigidbody2D> ();
        lastPos = transform.position;
    }


    void Update ()
    {
        moveInputX = Input.GetAxisRaw ( "Horizontal" );

        anim.SetBool ( "isWalking" , moveInputX != 0 );
        anim.SetBool ( "isJumping" , rb.velocity.y > 0.1f );// !IsGrounded () );
        playerSprite.flipX = moveInputX < 0;
        transform.rotation = Quaternion.identity;

        if ( Input.GetKeyDown ( KeyCode.Space ) && IsGrounded () )
        {
            Jump ();
        }

        lastPos = transform.position;
    }

    void FixedUpdate ()
    {
        rb.velocity = new Vector2 ( moveInputX * moveSpeed , rb.velocity.y );
        // rb.transform.position = new Vector3 ( rb.transform.position.x , rb.transform.position.y , 0 );
    }

    private bool IsGrounded ()
    {
        Debug.DrawRay ( transform.position , Vector3.down * checkDistance , Color.red , 100f , false );
        RaycastHit2D hit = Physics2D.Raycast ( transform.position , Vector2.down , checkDistance , groundLayer );
        if ( hit.collider != null )
        {
            return true;
        }
        if ( Mathf.Abs ( lastPos.y ) - Mathf.Abs ( transform.position.y ) < 0.05f && rb.velocity.y <= 0.1f )
            return true;
        return false;
    }

    private void Jump ()
    {
        rb.velocity = new Vector2 ( rb.velocity.x , jumpForce );
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);
    }*/
}