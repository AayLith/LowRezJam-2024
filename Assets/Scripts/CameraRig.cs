using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public float speed = 5;

    private void FixedUpdate ()
    {
        //transform.position = PlayerController.instance.transform.position;
        transform.position = Vector3.Lerp ( transform.position , PlayerController.instance.transform.position , speed * Time.deltaTime );
        //transform.position = new Vector3 ( Mathf.Ceil ( transform.position.x ) , Mathf.Ceil ( transform.position.y ) , transform.position.z );
    }
}
