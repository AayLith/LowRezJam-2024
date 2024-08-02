using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damages;
    public GameObject poof;

    void FixedUpdate ()
    {
        transform.Translate ( Vector3.right * Time.fixedDeltaTime * speed * Mathf.Sign ( transform.localScale.x ) , Space.World );
    }

    private void OnTriggerEnter2D ( Collider2D collision )
    {
        try { collision.GetComponent<Monster> ().takeDamages ( damages ); }
        catch { }
        Instantiate ( poof , transform.position , transform.rotation );
        Destroy ( gameObject );
    }
}
