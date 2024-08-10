using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static Dictionary<Vector3Int , Tile> tiles = new Dictionary<Vector3Int , Tile> ();

    public enum types { none, ground, ceiling }
    public types type;
    public GameObject content;
    public Vector3 gridPos;
    public Vector3 position { get { return transform.position; } }

    private void Start ()
    {
        addTile ( this );
    }

    public static Tile getTile ( Vector3 pos )
    {
        try { return tiles[ pos.toGridPos () ]; }
        catch { }
        return null;
    }

    public static Tile getTileAbove ( Tile t )
    {
        return getTile ( t.position + new Vector3 ( 0 , 8 , 0 ) );
    }

    public static Tile getTileBelow ( Tile t )
    {
        return getTile ( t.position + new Vector3 ( 0 , -8 , 0 ) );
    }

    public static void addTile ( Tile t )
    {
        tiles.Add ( t.transform.position.toGridPos () , t );
        t.gridPos = t.transform.position.toGridPos ();
    }

    public static void removeTile ( Tile t )
    {
        tiles.Remove ( t.transform.position.toGridPos () );
    }
}
