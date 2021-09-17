using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Type type;
    public float minTime, maxTime;
    public GameObject car;
    public GameObject [] logs;

    bool active;

    public enum Type 
    {
        cars,
        logs
    }


    void Generate()
    {
        if (type == Type.cars)
        {
            GameObject temporaryObject = Instantiate (car, transform.position, Quaternion.identity);
        }
        else if ((type == Type.logs))
        {
             GameObject temporaryObject = Instantiate (logs [Random.Range(0,logs.Length)], transform.position, Quaternion.identity);
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
