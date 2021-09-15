using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    public Type type;
    public Position carPosition;
    public float minTime, maxTime;
    public GameObject car;
    public GameObject [] logs;

    bool active;

    public enum Type 
    {
        cars,
        logs
    }

    public enum Position
    {
        left,
        right
    }

    void Generate()
    {
        if (type == Type.cars)
        {
            GameObject temporaryObject = Instantiate (car, transform.position, Quaternion.identity);
        }
        
        Invoke ("Generate", Random.Range (minTime,maxTime));
    }
    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
