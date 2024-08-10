using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public static LinkedList<Monster> allMonsters = new LinkedList<Monster> ();

    public GameObject sprite;
    public int loot = 1;
    public float speed { get { return Mathf.Max ( _speed * 0.1f , _speed * speedmod ); } }
    public float _speed = 10;
    public float speedmod = 1;
    public float raycastOffset = -5;
    [HideInInspector] public float cur_health;
    public float max_health;
    public bool stunned = false;

    private Rigidbody2D rb;
    private float gravity;

    private void Awake ()
    {
        cur_health = max_health;
        allMonsters.AddFirst ( this );
        rb = GetComponent<Rigidbody2D> ();
        gravity = rb.gravityScale;
    }

    private void Update ()
    {
        transform.rotation = Quaternion.identity;
    }

    private void FixedUpdate ()
    {
        if ( stunned ) return;

        Debug.DrawRay ( transform.position + new Vector3 ( 0 , raycastOffset , 0 ) , Vector2.right * 4 , Color.red );
        RaycastHit2D hit = Physics2D.Raycast ( transform.position + new Vector3 ( 0 , raycastOffset , 0 ) , Vector2.right , 4 , LayerMask.GetMask ( "Ground" ) );
        if ( hit.collider != null )
        {
            rb.gravityScale = 0;
            transform.position += Vector3.up * Time.fixedDeltaTime * speed * 2;
        }
        else
        {
            transform.Translate ( Vector3.right * Time.fixedDeltaTime * speed , Space.World );
            rb.gravityScale = gravity;
        }
    }

    public void takeDamages ( float amount )
    {
        cur_health -= amount;
        if ( cur_health <= 0 )
        { // Do ondeath stuff
            PlayerController.instance.changeMoney ( loot );
            Destroy ( gameObject );
        }
    }
}
