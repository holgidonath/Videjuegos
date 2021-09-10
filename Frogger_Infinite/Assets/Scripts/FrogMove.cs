using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMove : MonoBehaviour
{
    public float speed = 1f;
    private Vector2 _movement;
    private Animator _anmCtrl;
    private bool running;
    private int lookingHorizontal;
   
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
            running = true;
            lookingHorizontal = 1;
            _anmCtrl.SetInteger ("lookDir", 0);
            _anmCtrl.SetBool ("run", true);
        
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _movement.x = -speed;
            running = true;
            lookingHorizontal = 1;
            _anmCtrl.SetInteger ("lookDir", 1);
            _anmCtrl.SetBool ("run", true);
           
        }
        else if  (Input.GetKey(KeyCode.UpArrow))
        {
            _movement.y = speed;
            running = true;
            lookingHorizontal = 0;
            _anmCtrl.SetInteger ("lookDir", 3);
            _anmCtrl.SetBool ("run", true);
            
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _movement.y = -speed;
            running = true;
            lookingHorizontal = 0;
            _anmCtrl.SetInteger ("lookDir", 2);
            _anmCtrl.SetBool ("run", true);
    
            
        }
        else
        {
            _movement.x = 0f;
            _movement.y = 0f;
            running = false;
            _anmCtrl.SetBool ("run", false);
        }

       

    }

    void UpdateMovement()
    {
        var dt = Time.deltaTime;
        var curPos = transform.position;
        if (running)
        {
            if(lookingHorizontal == 1)
            {
                float newposx = (curPos.x + (_movement.x * dt)/10f);
                transform.position = new Vector3(newposx,curPos.y,curPos.z);
            }
            else if (lookingHorizontal == 0)
            {
            float newposy = (curPos.y + (_movement.y * dt)/10f);
            transform.position = new Vector3(curPos.x,newposy,curPos.z);
            }
        }
    }


}
