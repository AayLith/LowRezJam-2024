using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    public float speed;

    private void FixedUpdate ()
    {
        foreach ( KeyValuePair<Collider2D , int> c in Conveyor.allCollisions )
        {
            if ( c.Value > 0 )
                c.Key.transform.Translate ( -speed * Time.fixedDeltaTime , 0 , 0 );
        }
    }
}
