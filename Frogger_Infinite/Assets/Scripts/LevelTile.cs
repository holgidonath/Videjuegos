using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    // Start is called before the first frame update
    LevelSpawner levelSpawner;
    void Start()
    {
        levelSpawner = GameObject.FindObjectOfType<LevelSpawner>();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Frog")
        {
            levelSpawner.SpawnLevel();
            Destroy(gameObject, 2);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
