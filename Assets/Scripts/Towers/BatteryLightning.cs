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
        Vector3 diff = second.transform.position - first.transform.position;

        damages = dmg;

        line.positionCount = ( int ) ( dist / stepSkip ) + 2;
        col.size = new Vector2 ( Mathf.Max ( 1 , diff.x ) , Mathf.Max ( 1 , diff.y ) );
        Vector3 step = diff / line.positionCount;

        line.SetPosition ( 0 , first.transform.position + new Vector3 ( 0 , 4 , 0 ) );
        for ( int i = 1 ; i < line.positionCount - 1 ; i++ )
        {
            line.SetPosition ( i , first.transform.position + step * i + ( ( Vector3 ) Random.insideUnitCircle * randomness ) + new Vector3 ( 0 , 4 , 0 ) );
        }
        line.SetPosition ( line.positionCount - 1 , second.transform.position + new Vector3 ( 0 , 4 , 0 ) );
    }
}
