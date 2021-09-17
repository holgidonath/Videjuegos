using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    // Update is called once per frame
    void Update()
    {
        Follow();
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
