using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : Tower
{
    public Transform hook;
    public LineRenderer line;
    public float hookSpeed;
    float minHeight;
    float maxHeight;
    public float dropStun = 0.25f;
    Transform caught;
    List<Collider2D> collisions = new List<Collider2D>();
    Vector3 hookPos;


    private void Start()
    {
        maxHeight = transform.position.y;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1000, LayerMask.GetMask("Ground"));
        minHeight = hit.point.y + 4;
        hookPos = hook.position;
        StartCoroutine(crane());
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
    }

    protected override void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
    }

    IEnumerator crane()
    {
        while (true)
        {
            if (caught != null)
            {
                if (caught.GetComponent<Monster>().stunned == false || hookPos.y >= maxHeight)
                {
                    caught.GetComponent<Monster>().stunned = false;
                    caught = null;
                    anim.SetTrigger ( "coil" );
                    yield return new WaitForSeconds(dropStun);
                }
                else
                {

                    hook.position += Vector3.up * hookSpeed * Time.fixedDeltaTime;
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Crane");

                    caught.position = hook.position - Vector3.up * 8;

                }
            }
            else
            {
                hook.position -= Vector3.up * hookSpeed * Time.fixedDeltaTime;

                if (collisions.Count > 0)
                {
                    caught = collisions[0].transform;
                    caught.position = hook.position - Vector3.up * 8;
                    caught.GetComponent<Monster>().stunned = true;
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Crane");
                    anim.SetTrigger ( "attack" );
                }
            }

            hook.position = new Vector3(hook.position.x, Mathf.Clamp(hook.position.y, minHeight, maxHeight), hook.position.z);


            hookPos = hook.position;

            yield return new WaitForFixedUpdate();
        }
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisions.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisions.Remove(collision);
    }
}