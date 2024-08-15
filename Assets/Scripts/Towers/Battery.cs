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
        dmg = f.damages * dist * f.damagesDistMult;
        reload = f.reload * dist * f.reloadDistMult;
        nextShot = Time.time + 1;
    }
}

public class Battery : Tower
{
    public static List<Battery> allBatteries = new List<Battery> ();

    [Header ( "Battery" )]
    public float damagesDistMult;
    public float reloadDistMult;

    // battery stuff
    public List<BatteryLink> linkedBatteries = new List<BatteryLink> ();

    void connectBatteries ()
    {
        linkedBatteries.Clear ();
        foreach ( Battery b in allBatteries )
            b.linkedBatteries.Clear ();

        allBatteries.Add ( this );
        if ( allBatteries.Count < 2 ) return;
        if ( allBatteries.Count == 2 )
        {
            BatteryLink bl = new BatteryLink ( allBatteries[ 0 ] , allBatteries[ 1 ] );
            // Add the link
            bl.first.linkedBatteries.Add ( bl );
            bl.second.linkedBatteries.Add ( bl );
            return;
        }

        Quick_Sort ( allBatteries , 0 , allBatteries.Count - 1 );

        for ( int i = 0 ; i < allBatteries.Count - 1 ; i++ )
        {
            BatteryLink bl = new BatteryLink ( allBatteries[ i ] , allBatteries[ i + 1 ] );

            // Add the link
            bl.first.linkedBatteries.Add ( bl );
            bl.second.linkedBatteries.Add ( bl );
        }
    }

    private void Start ()
    {
        connectBatteries ();

        /*
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
        }*/

    }

    private void OnDestroy ()
    {
        connectBatteries ();
    }

    public void removeLink ( BatteryLink link )
    {
        linkedBatteries.Remove ( link );
    }

    protected override void Update ()
    {
        foreach ( BatteryLink bl in linkedBatteries )
        {
            if ( Time.time < bl.nextShot ) continue;

            GameObject b = Instantiate ( bullet , new Vector3 ( 0 , 4 , 0 ) + ( bl.first.transform.position + bl.second.transform.position ) / 2 , transform.rotation );
            b.GetComponent<BatteryLightning> ().set ( bl.dmg , bl.first , bl.second , bl.dist );
            FMODUnity.RuntimeManager.PlayOneShot("event:/BatteryLightning", GetComponent<Transform>().position);
            if ( flash )
                Instantiate ( flash , transform.position , transform.rotation );
            bl.nextShot = Time.time + bl.reload;
        }
    }

    private static void Quick_Sort ( List<Battery> arr , int left , int right )
    {
        // Check if there are elements to sort
        if ( left < right )
        {
            // Find the pivot index
            int pivot = Partition ( arr , left , right );

            // Recursively sort elements on the left and right of the pivot
            if ( pivot > 1 )
            {
                Quick_Sort ( arr , left , pivot - 1 );
            }
            if ( pivot + 1 < right )
            {
                Quick_Sort ( arr , pivot + 1 , right );
            }
        }
    }

    // Method to partition the array
    private static int Partition ( List<Battery> arr , int left , int right )
    {
        // Select the pivot element
        Battery pivot = arr[ left ];

        // Continue until left and right pointers meet
        while ( true )
        {
            // Move left pointer until a value greater than or equal to pivot is found
            while ( arr[ left ].transform.position.x <= pivot.transform.position.x )
            {
                left++;
            }

            // Move right pointer until a value less than or equal to pivot is found
            while ( arr[ right ].transform.position.x > pivot.transform.position.x )
            {
                right--;
            }

            // If left pointer is still smaller than right pointer, swap elements
            if ( left < right )
            {
                if ( arr[ left ] == arr[ right ] ) return right;

                Battery temp = arr[ left ];
                arr[ left ] = arr[ right ];
                arr[ right ] = temp;
            }
            else
            {
                // Return the right pointer indicating the partitioning position
                return right;
            }
        }
    }
}
