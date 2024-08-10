using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Conveyor : Tower
{
    public static Dictionary<Collider2D , int> allCollisions = new Dictionary<Collider2D , int> ();

    public RuleTile tile;
    public string grid;

    private void Start ()
    {
        Vector3Int gridpos = transform.position.toGridPos ();// new ( ( int ) ( ( transform.position.x - 4 ) / 8 ) , ( int ) ( ( transform.position.y - 4 ) / 8 ) , 0 );
        Tilemap g = GameObject.Find ( grid ).GetComponent<Tilemap> ();
        g.SetTile ( gridpos , tile );
    }

    protected override void Update ()
    {
        
    }

    private void OnDestroy ()
    {
        Vector3Int gridpos = transform.position.toGridPos ();// new ( ( int ) ( ( transform.position.x - 4 ) / 8 ) , ( int ) ( ( transform.position.y - 4 ) / 8 ) , 0 );
        Tilemap g = GameObject.Find ( grid ).GetComponent<Tilemap> ();
        g.SetTile ( gridpos , null );
    }

    private void OnTriggerEnter2D ( Collider2D collision )
    {
        if ( !allCollisions.ContainsKey ( collision ) )
            allCollisions.Add ( collision , 1 );
        else
            allCollisions[ collision ]++;
    }

    private void OnTriggerExit2D ( Collider2D collision )
    {
        if ( allCollisions.ContainsKey ( collision ) )
            allCollisions[ collision ]--;
    }
}
