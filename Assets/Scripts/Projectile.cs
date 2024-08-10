using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damages;
    public GameObject poof;
    public bool destroyOnHit = true;
    public Vector2 power;
    public float lifeTime = 999;

    void FixedUpdate ()
    {
        lifeTime -= Time.fixedDeltaTime;
        transform.Translate ( Vector3.right * Time.fixedDeltaTime * speed * Mathf.Sign ( transform.localScale.x ) , Space.World );
        if ( lifeTime < 0 ) Destroy ( gameObject );
    }

    protected void OnTriggerEnter2D ( Collider2D collision )
    {
        try
        {
            collision.GetComponent<Monster> ().takeDamages ( damages );
            if ( power != Vector2.zero )
            {
                collision.attachedRigidbody.AddForce ( power , ForceMode2D.Impulse );
                collision.GetComponent<Monster> ().stunned = false;
            }
        }
        catch { }
        if ( poof )
            Instantiate ( poof , transform.position , transform.rotation );
        if ( destroyOnHit )
            Destroy ( gameObject );
    }
}
