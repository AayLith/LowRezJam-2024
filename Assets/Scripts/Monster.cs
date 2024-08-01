using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public static LinkedList<Monster> allMonsters = new LinkedList<Monster> ();

    public float speed { get { return Mathf.Max ( 0 , _speed * speedmod ); } }
    public float _speed = 10;
    public float speedmod = 1;
    public int pathindex = 0;
    [HideInInspector] public float cur_health;
    public float max_health;

    private void Awake ()
    {
        pathindex = 0;
        cur_health = max_health;
        allMonsters.AddFirst ( this );
    }
}
