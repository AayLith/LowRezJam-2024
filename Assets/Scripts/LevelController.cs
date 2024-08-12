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

public class LevelController : MonoBehaviour
{
    public GameObject portal;
    public List<MonsterWave> waves = new List<MonsterWave> ();
    public float waveDelay = 3;

    void Start ()
    {
        StartCoroutine ( start () );
    }

    IEnumerator start ()
    {
        yield return new WaitForSeconds ( 1 );
        StartCoroutine ( waveSpawn ( 0 ) );
    }

    IEnumerator waveSpawn ( int index )
    {
        yield return new WaitForSeconds ( waveDelay );

        if ( index >= waves.Count )
        {
            // Victory
            FMODUnity.RuntimeManager.PlayOneShot("event:/Victory");
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

        StartCoroutine ( waveSpawn ( index++ ) );
    }
}
