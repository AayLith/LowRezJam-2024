using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter2D ( Collider2D collision )
    {
        PlayerController.instance.changeLives ( -1 );
        Destroy ( collision.gameObject );
    }
}
