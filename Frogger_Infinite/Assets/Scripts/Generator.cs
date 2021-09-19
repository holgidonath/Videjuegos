using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Type type;
    public CarType carType;
    public float minTime, maxTime;
    public GameObject [] cars;
    public GameObject [] logs;


    bool active;

    public enum Type 
    {
        cars,
        logs
    }

    public enum CarType
    {
        mower,
        pinkCar,
        truck,
        whiteCar,
        yellowCar
    }


    void Generate()
    {
        if (type == Type.cars)
        {
            GameObject temporaryObject = Instantiate (cars[(int)carType], transform.position, Quaternion.identity);
            var carScript = temporaryObject.GetComponent<Movement>();
            if(transform.position.x > 0)
            {
                carScript.speed = -50f;
                if (carType == CarType.mower || carType == CarType.whiteCar)
                {
                    temporaryObject.GetComponent<SpriteRenderer>().flipX = true;
                    if (carType == CarType.whiteCar)
                    {
                        minTime = 0.5f;
                        maxTime = 2f;
                        carScript.speed = -150f;
                    }
                }
                
            } else
            {
                carScript.speed = 50f;
                if (carType == CarType.pinkCar || carType == CarType.truck || carType == CarType.yellowCar)
                {
                    temporaryObject.GetComponent<SpriteRenderer>().flipX = true;
                }
                if (carType == CarType.whiteCar)
                {
                    minTime = 0.5f;
                    maxTime = 2f;
                    carScript.speed = 150f;
                }

            }
        }
        else if ((type == Type.logs))
        {
            GameObject temporaryObject = Instantiate (logs [Random.Range(0,logs.Length)], transform.position, Quaternion.identity);
            var logScript = temporaryObject.GetComponent<Movement>();
            maxTime = 5f;
            minTime = 3f;
            if (transform.position.x > 0)
            {
                logScript.speed = -50f;
            }
            else
            {
                logScript.speed = 50f;
            }
        }
        
        Invoke ("Generate", Random.Range (minTime,maxTime));
    }
    // Start is called before the first frame update
    void Start()
    {
        type = (Type)transform.parent.GetComponent<LevelTile>().type;
        if(type == Type.cars)
        {
            carType = (CarType)Random.Range(0, 5);
            if(carType == CarType.whiteCar)
            {
                minTime = 0.3f;
                maxTime = 1f;
            }
        }
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
