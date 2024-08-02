using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public static LinkedList<Monster> allMonsters = new LinkedList<Monster> ();

    public float speed { get { return Mathf.Max ( _speed * 0.1f , _speed * speedmod ); } }
    public float _speed = 10;
    public float speedmod = 1;
    public int pathindex = 0;
    [HideInInspector] public float cur_health;
    public float max_health;

    public bool stunned = false;

    private void Awake ()
    {
        pathindex = 0;
        cur_health = max_health;
        allMonsters.AddFirst ( this );
    }

    private void FixedUpdate ()
    {
        if ( !stunned )
            transform.Translate ( Vector3.right * Time.fixedDeltaTime * speed , Space.World );
    }

    public void takeDamages ( float amount )
    {
        cur_health -= amount;
        if ( cur_health <= 0 )
        { // Do ondeath stuff
            Destroy ( gameObject );
        }
    }
}
