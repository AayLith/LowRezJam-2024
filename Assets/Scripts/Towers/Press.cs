using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : Tower
{
    public Transform press;
    public LineRenderer line;
    public float pressSpeed;
    float minHeight;
    float maxHeight;
    public float dropStun = 0.25f;
    Vector3 pressPos;


    void Start ()
    {
        maxHeight = transform.position.y;
        RaycastHit2D hit = Physics2D.Raycast ( transform.position , Vector2.down , 1000 , LayerMask.GetMask ( "Ground" ) );
        minHeight = hit.point.y + 4;
        pressPos = press.position;
        line.positionCount = 2;
        line.SetPosition ( 0 , transform.position );
        line.SetPosition ( 1 , press.position );
        StartCoroutine ( gopress () );
    }

    protected override void Update ()
    {
        line.SetPosition ( 0 , transform.position );
        line.SetPosition ( 1 , press.position );
    }

    IEnumerator gopress ()
    {
        // move up
        while ( press.position.y < maxHeight )
        {
            yield return new WaitForFixedUpdate ();
            press.position += Vector3.up * pressSpeed * Time.fixedDeltaTime;
            line.SetPosition ( 0 , transform.position );
            line.SetPosition ( 1 , press.position );
        }
        press.position = new Vector3 ( press.position.x , Mathf.Clamp ( press.position.y , minHeight , maxHeight ) , press.position.z );
        yield return new WaitForSeconds (1);

        // move down
        while ( press.position.y > minHeight )
        {
            yield return new WaitForFixedUpdate ();
            press.position -= Vector3.up * pressSpeed * Time.fixedDeltaTime * 10;
            line.SetPosition ( 0 , transform.position );
            line.SetPosition ( 1 , press.position );
        }
        press.position = new Vector3 ( press.position.x , Mathf.Clamp ( press.position.y , minHeight , maxHeight ) , press.position.z );

        //strike
        GameObject b = Instantiate ( bullet , press.transform.position , press.transform.rotation );
        b.GetComponent<Projectile> ().damages = damages;
        b.GetComponent<Projectile> ().power = power;
        if ( flash )
            Instantiate ( flash , press.transform.position , press.transform.rotation );

        yield return new WaitForSeconds ( 1 );

        StartCoroutine ( gopress () );
    }
}
