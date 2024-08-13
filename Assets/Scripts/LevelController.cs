using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterSpawns
{
    public Monster monster;
    [Range ( 1 , 10 )]
    public int amount = 1;
    [Range ( 0.1f , 10 )]
    public float delay = 1;
}

[System.Serializable]
public class MonsterWave
{
    public List<MonsterSpawns> spawns = new List<MonsterSpawns> ();
}

[System.Serializable]
public class Spawn
{
    public Monster monster;
    public int weight = 1;
}

public class LevelController : MonoBehaviour
{
    public GameObject portal;
    //public List<MonsterWave> waves = new List<MonsterWave> ();
    //public float waveDelay = 3;

    public Spawn[] spawns;
    List<Monster> monsters = new List<Monster>();
    public int minWave = 5;
    public int maxWave = 10;
    public float minDelay = 5;
    public float maxDelay = 10;

    void Start ()
    {
        foreach ( Spawn s in spawns )
            for ( int i = 0 ; i < s.weight ; i++ )
                monsters.Add ( s.monster );
        StartCoroutine ( start () );
    }

    IEnumerator start ()
    {
        yield return new WaitForSeconds ( 10 );
        StartCoroutine ( waveSpawn ( 0 ) );
    }

    IEnumerator waveSpawn ( int index )
    {
        int i = 0;
        while ( i < 4096 )
        {
            for ( int j = 0 ; j < UnityEngine.Random.Range ( minWave , maxWave ) ; j++ )
            {
                int r = UnityEngine.Random.Range ( 0 , monsters.Count );
                Instantiate ( monsters[ r ] , portal.transform.position , Quaternion.identity , null );
                i++;
                yield return new WaitForSeconds ( 0.2f );
            }
            yield return new WaitForSeconds ( UnityEngine.Random.Range ( minDelay , maxDelay ) );
        }

        while ( Monster.allMonsters.Count > 0 )
            yield return null;
        // Victory
        FMODUnity.RuntimeManager.PlayOneShot ( "event:/Victory" );

        /*
        if ( index >= waves.Count )
        {
            // Victory
            FMODUnity.RuntimeManager.PlayOneShot ( "event:/Victory" );
            yield break;
        }
        foreach ( MonsterWave wave in waves )
            for ( int i = 0 ; i < wave.spawns.Count ; i++ )
            {
                for ( int j = 0 ; j < wave.spawns[ i ].amount ; j++ )
                {
                    Instantiate ( wave.spawns[ i ].monster , portal.transform.position , Quaternion.identity , null );
                    yield return new WaitForSeconds ( 0.2f );
                }
                yield return new WaitForSeconds ( wave.spawns[ i ].delay );
            }

        StartCoroutine ( waveSpawn ( index++ ) );*/
    }
}
