using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Camera cam;
    private int startingWidth;
    private int statingHeight;

    void Start()
    {
        startingWidth = Screen.width;
        statingHeight = Screen.height;
        if(Screen.width > 400)
        {
            cam.transform.position = new Vector3(transform.position.x, 140, transform.position.z);
            cam.orthographicSize = 240f;
        }
        else
        {
            cam.orthographicSize = 100f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Follow();
        if(Screen.width != startingWidth)
        {
            if (Screen.width > 400)
            {
                if(startingWidth < 400)
                {
                    
                    cam.transform.position = new Vector3(transform.position.x, transform.position.y + 140, transform.position.z);
                }
                cam.orthographicSize = 240f;
            }
            else
            {
                if(startingWidth > 400)
                {
                    cam.transform.position = new Vector3(transform.position.x, transform.position.y - 140, transform.position.z);
                }
                cam.orthographicSize = 100f;
            }
            startingWidth = Screen.width;
            List<Generator> generators = new List<Generator>();

            foreach (Generator go in FindObjectsOfType<Generator>() as Generator[])
            {
                if (go.transform.position.x < 0)
                {
                    go.transform.position = new Vector3(-400, go.transform.position.y, go.transform.position.z);
                }
                else
                {
                    go.transform.position = new Vector3(400, go.transform.position.y, go.transform.position.z);
                }
            }
            
        }
        
    }

    void Follow()
    {
        if(target.position.y >= transform.position.y) { 
            Vector3 targetPosition = target.position + offset;
            targetPosition.x = 0;
            targetPosition.z = transform.position.z;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);
            transform.position = smoothPosition;
        }
    }

}
