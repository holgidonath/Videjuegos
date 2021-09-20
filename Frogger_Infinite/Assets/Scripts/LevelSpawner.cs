using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject level;
    Vector3 nextSpawnPoint;


    void Start()
    {
        nextSpawnPoint = new Vector3(0, -100, 0);
        SpawnLevel();
        SpawnLevel();
        SpawnLevel();
        SpawnLevel();
    }

    public void SpawnLevel()
    {
        GameObject temp = Instantiate(level, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(0).transform.position;
        GameObject.FindWithTag("Frog").GetComponent<FrogMove>().addLevel();
    }
    
}
