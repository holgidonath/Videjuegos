using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 50f;
    private Vector2 _movement;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;
        var curPos = transform.position;
        _movement.x = speed;
        float newposx = (curPos.x + (_movement.x * dt));
        transform.position = new Vector3(newposx, curPos.y, curPos.z);
    }
}
