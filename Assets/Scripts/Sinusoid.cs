using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinusoid : MonoBehaviour
{
    public Vector3 wave;
    public float speed;

    float startTime;
    Vector3 startPos;

    private void Awake ()
    {
        startPos = transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.localPosition += wave * Mathf.Sin ( ( Time.time - startTime ) * speed );
    }
}
