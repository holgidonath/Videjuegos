using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rows : MonoBehaviour
{
    private static float SCREEN_WIDTH = 320;
    private float startPos; 
    
    
    private GameObject first;
    private GameObject second;

    public GameObject[] rowObjects;
    private Vector2 _movement;
    private float speed = 50f;


    // Start is called before the first frame update
    void Start()
    {
        startPos = rowObjects[0].transform.position.x;
        first = rowObjects[0];
        second = rowObjects[1];
    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;
        _movement.x = -speed;

        foreach (var car in rowObjects)
        {
            var curPos = transform.position;
            float newposx = (curPos.x + (_movement.x * dt));
            car.transform.position += new Vector3(_movement.x*dt,0);
        }
        if(second.transform.position.x < -(startPos + SCREEN_WIDTH))
        {
            first.transform.position = second.transform.position;
            second.transform.position += new Vector3 (SCREEN_WIDTH,0);
        }
    
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Frog")
        {
            col.SendMessage("Respawn");
        }
    }
}

