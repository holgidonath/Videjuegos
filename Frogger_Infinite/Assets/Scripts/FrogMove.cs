using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMove : MonoBehaviour
{
    public float speed = 1f;
    private Vector2 _movement;
    private Animator _anmCtrl;
   
    // Start is called before the first frame update
    void Start()
    {
        _anmCtrl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdateMovement();
    }

    void UpdateInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _movement.x = speed;
            _anmCtrl.SetBool("run",true);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _movement.x = -speed;
            _anmCtrl.SetBool("run",true);
        }
        else if  (Input.GetKey(KeyCode.UpArrow))
        {
            _movement.y = speed;
            _anmCtrl.SetBool("run",true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _movement.y = -speed;
            _anmCtrl.SetBool("run",true);
        }
        else
        {
            _movement.x = 0f;
            _movement.y = 0f;
        }

        _anmCtrl.SetBool("run", Mathf.Abs(_movement.x) > 0.001f ||  Mathf.Abs(_movement.y) > 0.001f);

    }

    void UpdateMovement()
    {
        var dt = Time.deltaTime;
        var curPos = transform.position;
        
        float newposx = curPos.x + _movement.x * dt;
        float newposy = curPos.y + _movement.y * dt;

        transform.position = new Vector3(newposx,newposy,curPos.z);
    }


}
