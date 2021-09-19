using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject level;
    Vector3 nextSpawnPoint;
    public Text counterText;
    public float counter_score = 0;
    private float counter = 0;

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
        counter += 1;
        if (counter%10 == 0)
        {
            counter_score += counter_score;
        }
        else
        {
            counter_score += 1;
        }
        counter += 1;
        counterText.text = counter_score.ToString();
    }
    
}
