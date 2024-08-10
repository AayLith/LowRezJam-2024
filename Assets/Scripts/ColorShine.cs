using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShine : MonoBehaviour
{
    public float offset;
    public float amplitude = 1;
    public float speed = 1;
    public SpriteRenderer r;
    Color c;

    private void Awake ()
    {
        c = r.color;
    }

    void Update ()
    {
        c.a = amplitude * Mathf.Sin ( Time.time * speed ) + offset;
        r.color = c;
    }
}
