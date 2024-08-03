using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damages;
    public GameObject poof;
    public bool destroyOnHit = true;

    void FixedUpdate ()
    {
        transform.Translate ( Vector3.right * Time.fixedDeltaTime * speed * Mathf.Sign ( transform.localScale.x ) , Space.World );
    }

    protected void OnTriggerEnter2D ( Collider2D collision )
    {
        try { collision.GetComponent<Monster> ().takeDamages ( damages ); }
        catch { }
        if ( poof )
            Instantiate ( poof , transform.position , transform.rotation );
        if ( destroyOnHit )
            Destroy ( gameObject );
    }
}
