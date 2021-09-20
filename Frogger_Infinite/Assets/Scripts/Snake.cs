using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private bool firstTurn = true;
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
        start = new Vector3((logSprite.bounds.size.x / 2)*(-1), 0, 0);
        end = new Vector3((logSprite.bounds.size.x / 2), 0, 0);
        target.localPosition = end;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target.localPosition, fixedSpeed);
        }
        if(transform.localPosition == target.localPosition)
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
}
