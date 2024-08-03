using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BatteryLink
{
    public Battery first;
    public Battery second;
    public float dist;
    public float dmg;
    public float reload;
    public float nextShot;

    public BatteryLink ( Battery f , Battery s )
    {
        second = f;
        first = s;
        dist = Vector3.Distance ( f.transform.position , s.transform.position );
        dmg = f.damages * f.damagesDistMult;
        reload = f.reload * f.reloadDistMult;
        nextShot = Time.time + 1;
    }
}

public class Battery : Tower
{
    public static List<Battery> allBatteries = new List<Battery> ();

    public float damages = 1;
    public float damagesDistMult;
    public GameObject bullet;
    public GameObject flash;
    public float reload = 1;
    public float reloadDistMult;

    // battery stuff
    public List<BatteryLink> linkedBatteries = new List<BatteryLink> ();

    private void Awake ()
    {
        for ( int i = 0 ; i < allBatteries.Count ; i++ )
        {
            // if ( allBatteries[ i ] == this ) continue;
            // Check if it is aligned with another battery
            if ( transform.position.x == allBatteries[ i ].transform.position.x || transform.position.y == allBatteries[ i ].transform.position.y )
            { // Make a link and init
                BatteryLink bl = new BatteryLink ( allBatteries[ i ] , this );

                // Add the link
                bl.first.linkedBatteries.Add ( bl );
                bl.second.linkedBatteries.Add ( bl );
            }
        }

        allBatteries.Add ( this );
    }

    private void OnDestroy ()
    {
        foreach ( BatteryLink bl in linkedBatteries )
        {
            // Disconnect batteries
            if ( bl.first == this || bl.second == this )
            {
                bl.first.removeLink ( bl );
                bl.second.removeLink ( bl );
            }
        }
    }

    public void removeLink ( BatteryLink link )
    {
        linkedBatteries.Remove ( link );
    }

    private void Update ()
    {
        foreach ( BatteryLink bl in linkedBatteries )
        {
            if ( Time.time < bl.nextShot ) continue;

            Debug.Log ( bl.nextShot );

            GameObject b = Instantiate ( bullet , new Vector3 ( 0 , 4 , 0 ) + ( bl.first.transform.position + bl.second.transform.position ) / 2 , transform.rotation );
            b.GetComponent<BatteryLightning> ().set ( bl.dmg , bl.first , bl.second , bl.dist );
            if ( flash )
                Instantiate ( flash , transform.position , transform.rotation );
            bl.nextShot = Time.time + bl.reload;
        }
    }
}
