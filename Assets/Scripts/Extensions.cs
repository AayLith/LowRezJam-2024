using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class Extensions
{
    public static Vector2Int vectToInt ( this Vector2 v )
    {
        return new Vector2Int ( ( int ) v.x , ( int ) v.y );
    }

    public static Vector3Int vectToInt ( this Vector3 v )
    {
        return new Vector3Int ( ( int ) v.x , ( int ) v.y , ( int ) v.z );
    }

    public static Vector3Int toGridPos ( this Vector3 v )
    {
        return new Vector3Int ( ( int ) ( ( v.x - 4 ) / 8 ) , ( int ) ( ( v.y - 4 ) / 8 ) , 0 );
    }
}
