using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Nailgun : Tower
{
    public float damages = 1;
    public GameObject tip;
    public GameObject bullet;
    public GameObject flash;
    public float reload = 1;
    private float nextShot = 1;

    private void Update ()
    {
        if ( Time.time < nextShot ) return;

        GameObject b = Instantiate ( bullet , tip.transform.position , transform.rotation );
        b.GetComponent<Projectile> ().damages = damages;
        if ( flash )
            Instantiate ( flash , transform.position , transform.rotation );
        nextShot = Time.time + reload;
    }
}
