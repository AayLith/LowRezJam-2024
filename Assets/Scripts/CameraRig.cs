using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public float speed = 5;
    public Vector3 offset;
    public Vector3 min;
    public Vector3 max;

    private void FixedUpdate ()
    {
        //transform.position = PlayerController.instance.transform.position;
        Vector3 pos = new Vector3 (
            Mathf.Clamp ( PlayerController.instance.transform.position.x , min.x , max.x ) ,
            Mathf.Clamp ( PlayerController.instance.transform.position.y , min.y , max.y ) ,
            Mathf.Clamp ( PlayerController.instance.transform.position.z , min.z , max.z ) );
        transform.position = Vector3.Lerp ( transform.position , pos + offset , speed * Time.deltaTime );
        transform.position = pos + offset;
        //transform.position = new Vector3 ( Mathf.Ceil ( transform.position.x ) , Mathf.Ceil ( transform.position.y ) , transform.position.z );
    }
}
