using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpJump : MonoBehaviour
{
    Rigidbody2D rb;
    public float minPow = 25;
    public float maxPow = 50;
    public float minDelay = 3;
    public float maxDelay = 10;

    private void Awake ()
    {
        rb = GetComponent<Rigidbody2D> ();
        StartCoroutine ( jump () );
    }

    IEnumerator jump ()
    {
        yield return new WaitForSeconds ( Random.Range ( minDelay , maxDelay ) );

        if ( rb.velocity.y < 0.1f )
            rb.velocity = new Vector2 ( rb.velocity.x , Random.Range ( minPow , maxPow ) );

        StartCoroutine ( jump () );
    }
}
