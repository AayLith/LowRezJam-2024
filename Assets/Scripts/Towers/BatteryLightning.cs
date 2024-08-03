using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BatteryLightning : Projectile
{
    public LineRenderer line;
    public float randomness;
    public float lifetime = 0.5f;
    public Color color;
    public BoxCollider2D col;
    public int stepSkip = 4;

    private void Reset ()
    {
        speed = 0;
    }

    private void Update ()
    {
        lifetime -= Time.deltaTime;
        color.a = lifetime * 2;
        line.SetColors ( color , color );

        if ( lifetime < 0 )
            Destroy ( gameObject );
    }

    public void set ( float dmg , Battery first , Battery second , float dist )
    {
        // float dist = Vector3.Distance ( first.transform.position , second.transform.position );
        Vector3 diff = new Vector3 ( Mathf.Abs ( second.transform.position.x - first.transform.position.x ) ,
                                        Mathf.Abs ( second.transform.position.y - first.transform.position.y ) , 0 );
        Vector3 diff2 = second.transform.position - first.transform.position;

        damages = dmg;

        line.positionCount = ( int ) ( dist / stepSkip ) + 2;
        col.size = new Vector2 ( Mathf.Max ( 4 , diff.x ) , Mathf.Max ( 4 , diff.y ) );
        Vector3 step = diff2 / line.positionCount;

        line.SetPosition ( 0 , first.transform.position + new Vector3 ( 0 , 7 , 0 ) );
        for ( int i = 1 ; i < line.positionCount - 1 ; i++ )
        {
            line.SetPosition ( i , first.transform.position + step * i + ( ( Vector3 ) Random.insideUnitCircle * randomness ) + new Vector3 ( 0 , 7 , 0 ) );
        }
        line.SetPosition ( line.positionCount - 1 , second.transform.position + new Vector3 ( 0 , 7 , 0 ) );
    }
}
