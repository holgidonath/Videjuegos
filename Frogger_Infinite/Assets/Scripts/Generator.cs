using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Type type;
    public CarType carType;
    public WaterType waterType;
    public float minTime, maxTime;
    public GameObject [] cars;
    public GameObject [] logs;
    private int turtleIndex = 3;
    private int minDTurtleIndex = 3;
    private int maxDTurtleIndex = 5;
    private int minTTurtleIndex = 5;
    private int maxTTurtleIndex = 7;
    private int regularTurtleCounter = 0;
    private float snakeProb = 0.1f;
    private float crocodileProb = 0.1f;
    private bool crocodileSpawned = false;
    public GameObject snake;


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

    public enum WaterType
    {
        bigLog,
        mediumLog,
        smallLog,
        msDobuleT,
        mDoubleT,
        msTripleT,
        mTripleT,
        crocodile
    }


    void Generate()
    {
        GameObject temporaryObject;
        if (type == Type.cars)
        {
            temporaryObject = Instantiate (cars[(int)carType], transform.position, Quaternion.identity);
            var carScript = temporaryObject.GetComponent<Movement>();
            if(transform.position.x > 0)
            {
                carScript.speed = -50f;
                if (carType == CarType.mower || carType == CarType.whiteCar)
                {
                    temporaryObject.GetComponent<SpriteRenderer>().flipX = true;
                    if (carType == CarType.whiteCar)
                    {
                        
                        carScript.speed = -150f;
                    }
                }
                if (carType == CarType.truck)
                {
                    carScript.speed = -35f;
                }

            } else
            {
                carScript.speed = 50f;
                if (carType == CarType.pinkCar || carType == CarType.truck || carType == CarType.yellowCar)
                {
                    temporaryObject.GetComponent<SpriteRenderer>().flipX = true;
                    if(carType == CarType.truck)
                    {
                        carScript.speed = 35f;
                    }
                }
                if (carType == CarType.whiteCar)
                {
                    
                    carScript.speed = 150f;
                }

            }
        }
        else if ((type == Type.logs))
        {
            if (waterType == WaterType.msDobuleT || waterType == WaterType.msTripleT) //turtle state
            {
                temporaryObject = Instantiate(logs[turtleIndex], transform.position, Quaternion.identity);
                if (turtleIndex == (int)WaterType.mDoubleT || turtleIndex == (int)WaterType.mTripleT)
                {
                    if(regularTurtleCounter == 2)
                    {
                        regularTurtleCounter = 0;
                        turtleIndex++;
                    }
                    regularTurtleCounter++;
                }
                else
                {
                    turtleIndex++;
                }
                if (waterType == WaterType.msDobuleT)
                {
                    if (turtleIndex == maxDTurtleIndex)
                    {
                        turtleIndex = minDTurtleIndex;
                    }

                } else
                {
                    if (turtleIndex == maxTTurtleIndex)
                    {
                        turtleIndex = minTTurtleIndex;
                    }
                }

            }
            else
            {
                if (Random.Range(0f, 1f) <= crocodileProb)
                {
                    crocodileSpawned = true;
                    temporaryObject = Instantiate(logs[(int)WaterType.crocodile], transform.position, Quaternion.identity);
                }
                else
                {
                    temporaryObject = Instantiate(logs[(int)waterType], transform.position, Quaternion.identity);
                }
                
                if(Random.Range(0f,1f) <= snakeProb && !crocodileSpawned)
                {
                    
                    snake = Instantiate(snake, temporaryObject.transform.position, Quaternion.identity);
                    var snakeScript = snake.GetComponent<Movement>();
                    snake.transform.SetParent(temporaryObject.transform);

                }

                


            }
            var logScript = temporaryObject.GetComponent<Movement>();
            if (transform.position.x > 0)
            {
                logScript.speed = -50f;
                if (crocodileSpawned)
                {
                    temporaryObject.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else
            {
                logScript.speed = 50f;

                if (!crocodileSpawned)
                {
                    SpriteRenderer[] sprites = temporaryObject.GetComponentsInChildren<SpriteRenderer>();

                    for (int i = 0; i < sprites.Length; i++)
                    {
                        sprites[i].flipX = true;
                    }
                }


            }
            if (crocodileSpawned)
            {
                crocodileSpawned = false;
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
                minTime = 0.5f;
                maxTime = 2f;
            } else if (carType == CarType.truck) {
                minTime = 2f;
                maxTime = 5f;
            }
        } else
        {
            waterType = (WaterType)Random.Range(0, 4);
            if(waterType == WaterType.bigLog)
            {
                minTime = 5f;
                maxTime = 8f;
            }
            else if (waterType == WaterType.mediumLog)
            {
                minTime = 4f;
                maxTime = 7f;
            }
            else if (waterType == WaterType.smallLog)
            {
                minTime = 3f;
                maxTime = 6f;
            }
            else
            {
                waterType = (WaterType)Random.Range(0, 2) == 0 ? WaterType.msDobuleT : WaterType.msTripleT;
                turtleIndex = waterType == WaterType.msDobuleT ? minDTurtleIndex : minTTurtleIndex;
                minTime = 1.5f;
                maxTime = 3f;
            }
        }
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
