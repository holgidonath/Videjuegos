using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMove : MonoBehaviour
{
    public float speed = 150f;
    private Vector2 _movement;
    private Animator _anmCtrl;
    private bool running;
    private int lookingHorizontal;

    private Vector3 destination, startPos;
    float startTime, time;
    private float move_tiles_y = Screen.height / 13;
    private float move_tiles_x = Screen.width / 13;


    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public Resolution resolution;

    private Camera cam;


   
    // Start is called before the first frame update
    void Start()
    {
        resolution = Screen.currentResolution;
        _anmCtrl = GetComponent<Animator>();
        transform.position = new Vector3(0, -(Screen.height / 2), 0);
        Debug.Log(Screen.width + " x " + Screen.height);
        movePoint.parent = null;
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        /*UpdateInput();*/
        /*UpdateMovement();*/

        /*if (running)
        {
            time = (Time.time - startTime) * speed;
            transform.position = Vector3.Lerp(startPos, destination, time);
            if(transform.position == destination)
            {
                running = false;
                _anmCtrl.SetBool("run", false);
            }
        }

        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && !running)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _anmCtrl.SetInteger("lookDir", 3);
                destination = transform.position + new Vector3(0, move_tiles_y, 0);
                lookingHorizontal = 0;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _anmCtrl.SetInteger("lookDir", 2);
                destination = transform.position + new Vector3(0, -move_tiles_y, 0);
                lookingHorizontal = 0;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _anmCtrl.SetInteger("lookDir", 1);
                destination = transform.position + new Vector3(-move_tiles_x, 0, 0);
                lookingHorizontal = 1;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _anmCtrl.SetInteger("lookDir", 0);
                destination = transform.position + new Vector3(move_tiles_x, 0, 0);
                lookingHorizontal = 1;
            }
            _anmCtrl.SetBool("run", true);
            startTime = Time.time;
            startPos = transform.position;
            running = true;
        }*/

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        if (running)
        {
            if (transform.position == movePoint.position)
            {
                running = false;
                _anmCtrl.SetBool("run", false);
            }
        }

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.5f && !running)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                
                    var lastMovePointPositionX = movePoint.position;
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal")*19f, 0f, 0f);
                    if(Mathf.Abs(movePoint.position.x) <= Screen.width / 2) { 
                        _anmCtrl.SetBool("run", true);
                        running = true;
                        lookingHorizontal = 1;
                        if (Input.GetAxisRaw("Horizontal") == 1f)
                        {
                            _anmCtrl.SetInteger("lookDir", 0);
                        
                        } else
                        {
                            _anmCtrl.SetInteger("lookDir", 1);
                        }
                    }
                    else
                    {
                        movePoint.position = lastMovePointPositionX;
                    }

                
                
            }
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
               
                    var lastMovePointPositionY = movePoint.position;
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical")*20f, 0f);
                    
                    if (movePoint.position.y >= (cam.transform.position.y - Screen.height/2))
                    {
                        _anmCtrl.SetBool("run", true);
                        running = true;
                        lookingHorizontal = 0;
                        if (Input.GetAxisRaw("Vertical") == 1f)
                        {
                            _anmCtrl.SetInteger("lookDir", 3);
                        }
                        else
                        {
                            _anmCtrl.SetInteger("lookDir", 2);
                        }
                    }
                    else
                    {
                        movePoint.position = lastMovePointPositionY;
                    }
                
            }
        }


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }

    /*  void UpdateInput()
      {
          if (Input.GetKey(KeyCode.RightArrow))
          {
              _anmCtrl.SetInteger("lookDir", 0);
              _movement.x = speed;
              running = true;
              lookingHorizontal = 1;
              _anmCtrl.SetBool("run", true);

          }
          else if (Input.GetKey(KeyCode.LeftArrow))
          {
              _anmCtrl.SetInteger("lookDir", 1);
              _movement.x = -speed;
              running = true;
              lookingHorizontal = 1;
              _anmCtrl.SetBool("run", true);

          }
          else if (Input.GetKey(KeyCode.UpArrow))
          {
              _anmCtrl.SetInteger("lookDir", 3);
              _movement.y = speed;
              running = true;
              lookingHorizontal = 0;
              _anmCtrl.SetBool("run", true);

          }
          else if (Input.GetKey(KeyCode.DownArrow))
          {
              _anmCtrl.SetInteger("lookDir", 2);
              _movement.y = -speed;
              running = true;
              lookingHorizontal = 0;
              _anmCtrl.SetBool("run", true);


          }
          else
          {
              _movement.x = 0f;
              _movement.y = 0f;
              running = false;
              _anmCtrl.SetBool("run", false);
          }




      }
  */
    /*   void UpdateMovement()
       {
           var dt = Time.deltaTime;
           var curPos = transform.position;
           if (running)
           {
               if(lookingHorizontal == 1)
               {
                   float newposx = (curPos.x + (_movement.x * dt));
                   transform.position = new Vector3(newposx,curPos.y,curPos.z);
               }
               else if (lookingHorizontal == 0)
               {
                   float newposy = (curPos.y + (_movement.y * dt));
                   transform.position = new Vector3(curPos.x,newposy,curPos.z);
               }
           }
       }*/

    public void Respawn()
    {
        transform.position = new Vector3(0,0, 0);
    }


}
