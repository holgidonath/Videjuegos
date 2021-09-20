using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private Vector3 movePoint;
    // Start is called before the first frame update
    void Start()
    {
        movePoint = new Vector3(1000, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;
        
        transform.position = Vector3.MoveTowards (transform.position, movePoint ,speed * dt);
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
        Destroy (gameObject);
    }
}
