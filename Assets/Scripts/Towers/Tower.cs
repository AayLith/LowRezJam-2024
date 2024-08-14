using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class Tower : MonoBehaviour
{
    public enum placements { aboveGround, ground, ceiling, air }
    public placements placement;

    [Header ( "Stats" )]
    public int price = 5;
    public float damages = 1;
    public float reload = 1;
    protected float nextShot = 1;
    public Vector2 power = Vector2.zero;
    public bool waitForTrigger = false;

    [Header ( "GameObjects" )]
    public Sprite menuSprite;
    public ColliderRecord trigger;
    public GameObject tip;
    public GameObject bullet;
    public GameObject flash;
    public Animator anim;

    virtual protected void Update ()
    {
        if ( Time.time < nextShot ) return;
        if ( trigger != null && waitForTrigger && trigger.collisions.Count == 0 ) return;

        if ( anim )
        {
            anim.SetTrigger ( "attack" );
        }
        GameObject b = Instantiate ( bullet , tip.transform.position , transform.rotation );
        b.GetComponent<Projectile> ().damages = damages;
        b.GetComponent<Projectile> ().power = power;
        if ( flash )
            Instantiate ( flash , transform.position , transform.rotation );
        nextShot = Time.time + reload;
    }
}
