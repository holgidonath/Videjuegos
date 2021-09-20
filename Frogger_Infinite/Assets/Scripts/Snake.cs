using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private GameObject log;
    private bool flip = false;
    public Transform target;
    private Vector3 start, end;
    private float speed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
        log = transform.parent.gameObject;
        var logSprite = log.GetComponent<SpriteRenderer>();
        flip = GetComponent<SpriteRenderer>().flipX;
        if (target != null)
        {
            target.SetParent(log.transform);
        }
        if(log.transform.position.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            flip = true;
        }
        if(log.GetComponent<Movement>().speed > 0)
        {
            end = new Vector3(1000, 0, 0);
            start = new Vector3(1000 * (-1), 0, 0);
        }
        else
        {
            start = new Vector3(1000, 0, 0);
            end = new Vector3(1000 * (-1), 0, 0);
        }
        
        target.localPosition = end;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "LimitPoints")
        {
            if (flip)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                flip = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                flip = true;
            }
            target.localPosition = (target.localPosition == start) ? end : start;


        }
    }
    void Update()
    {
        if (target != null)
        {
            var fixedSpeed = speed * Time.deltaTime;
            if (flip)
            {
                transform.localPosition += new Vector3(fixedSpeed, 0, 0);
            }
            else
            {
                transform.localPosition += new Vector3(-fixedSpeed, 0, 0);
            }
            

        }
    }
}
