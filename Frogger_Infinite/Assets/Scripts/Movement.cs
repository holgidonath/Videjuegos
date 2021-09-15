using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;
        transform.position += new Vector3 (speed * dt,0,0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Destructor")
        {
            Destruir();
        }
    }

    void Destruir()
    {
    
    }
}
