using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int lives = 10;
    public List<Transform> path = new List<Transform> ();
    [SerializeField] LineRenderer line;

    private void Start ()
    {
        line.positionCount = path.Count;
        for ( int i = 0 ; i < path.Count ; i++ )
            line.SetPosition ( i , path[ i ].position );
    }

    private void FixedUpdate ()
    {
        Monster monster;
        if ( Monster.allMonsters.Count == 0 ) return;
        for ( LinkedListNode<Monster> m = Monster.allMonsters.First ; m != null ; m = m.Next )
        {
            if ( m.Value == null )
            {
                Monster.allMonsters.Remove ( m );
                continue;
            }

            monster = m.Value;
            // Move monster
            monster.transform.position = Vector3.MoveTowards ( monster.transform.position , path[ monster.pathindex ].transform.position , monster.speed * Time.fixedDeltaTime );

            // Check if waypoint has been reached
            if ( Vector3.Distance ( monster.transform.position , path[ monster.pathindex ].transform.position ) < 0.1f )
                monster.pathindex++;

            // If last waypoint, lose a life
            if ( monster.pathindex > path.Count - 1 )
            {
                Destroy ( monster.gameObject );
                lives--;
            }
        }
        /*
        foreach ( Monster monster in Monster.allMonsters )
        {
            // Move monster
            monster.transform.position = Vector3.MoveTowards ( monster.transform.position , path[ monster.pathindex ].transform.position , monster.speed * Time.fixedDeltaTime );

            // Check if waypoint has been reached
            if ( Vector3.Distance ( monster.transform.position , path[ monster.pathindex ].transform.position ) < 0.1f )
                monster.pathindex++;

            // If last waypoint, lose a life
            if ( monster.pathindex++ > path.Count )
            {
                Destroy ( monster.gameObject );
                lives--;
            }
        }
        */
    }
}
