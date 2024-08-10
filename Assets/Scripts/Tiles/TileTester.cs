using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTester : MonoBehaviour
{
    void Update ()
    {
        transform.position = PlayerController.instance.transform.position.toGridPos () * 8;
    }
}
