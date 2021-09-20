using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FrogMove : MonoBehaviour
{
    public AudioSource[] SoundFx;
    public float counter_check = 0;
    public Text counterText1;
    public Text counterText;
    public Text bonus;
    public float counter = -1;
    public GameObject lossMenu;
    public float speed = 150f;
    private Vector2 _movement;
    private Animator _anmCtrl;
    private bool running;
    private int lookingHorizontal;
    public float levelCount = 0f;

    private Vector3 destination, startPos;
    float startTime, time;
    private float move_tiles_y = Screen.height / 13;
    private float move_tiles_x = Screen.width / 13;


    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public Resolution resolution;

    private Camera cam;

    private float tilesx = 20f;
    private float tilesy = 20f;

    private bool xAxisInUse = false;
    private bool yAxisInUse = false;

    private bool dead = false;

    private List<GameObject> levelsToDestroy;

    private int logsTouched = 0;


    // Start is called before the first frame update
    void Start()
    {
        resolution = Screen.currentResolution;
        _anmCtrl = GetComponent<Animator>();
        
        /*Debug.Log(Screen.width + " x " + Screen.height);*/
        movePoint.parent = null;
        cam = Camera.main;
        levelsToDestroy = new List<GameObject>();
        lookingHorizontal = 1;
        transform.position = new Vector3(0, -98f, 0);
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        dead = true;
        yield return new WaitForSeconds(2f);
        dead = false;
    }

    public void addLevel()
    {
        levelCount++;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        if (running && !dead)
        {
            if (transform.position == movePoint.position)
            {
                running = false;
                _anmCtrl.SetBool("run", false);
            }
        }

        if ((Vector3.Distance(transform.position, movePoint.position) <= 0.5f && !running) && !dead)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (xAxisInUse == false && yAxisInUse == false) 
                { 
                    var lastMovePointPositionX = movePoint.position;
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal")*tilesx, 0f, 0f);
                    
                    if (Mathf.Abs(movePoint.position.x) <= Screen.width / 2) { 
                        if(lookingHorizontal == 0)
                        {
                            var collider = GetComponent<BoxCollider2D>();
                            collider.size = new Vector2(collider.size.y, collider.size.x);
                            collider.offset = new Vector2(collider.offset.x, 7.8f);
                        }
                        _anmCtrl.SetBool("run", true);
                        running = true;
                        lookingHorizontal = 1;
                        SoundFx[0].Play();
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
                    xAxisInUse = true;
                }

            } 
            if(Input.GetAxisRaw("Horizontal") == 0)
            {
                xAxisInUse = false;
            }
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (yAxisInUse == false && xAxisInUse == false) 
                { 
                    var lastMovePointPositionY = movePoint.position;
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical")*tilesy, 0f);
                    
                    if (movePoint.position.y >= (cam.transform.position.y - Screen.height/2))
                    {
                        if(lookingHorizontal == 1) { 
                            var collider = GetComponent<BoxCollider2D>();
                            collider.size = new Vector2(collider.size.y, collider.size.x);
                            collider.offset = new Vector2(collider.offset.x, 6f);
                        }
                        _anmCtrl.SetBool("run", true);
                        running = true;
                        lookingHorizontal = 0;
                        SoundFx[0].Play();
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
                    yAxisInUse = true;
                }
            }
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                yAxisInUse = false;
            }
        }


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && !dead)
        {
            Respawn();
        }
        else if (col.gameObject.tag == "Platform" || col.gameObject.tag == "CheckPoint")
        {
            logsTouched++;
            if (col.gameObject.tag == "CheckPoint")
            {
                AddPoint();
                
            }
        }

        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Platform" && !dead)
        {
            if (Mathf.Abs(movePoint.position.x) <= ((Screen.width / 2)-8)) // el 8 es para corregir el offset del sprite
            {
                movePoint.parent = col.transform;
                transform.parent = col.transform;
            }
            else
            {
                if((Mathf.Abs(movePoint.position.x) <= ((Screen.width / 2))) && Mathf.Sign(movePoint.position.x) != Mathf.Sign(col.transform.gameObject.GetComponent<Movement>().speed))
                {
                    movePoint.parent = col.transform;
                    transform.parent = col.transform;
                }else { 
                    transform.parent = null;
                    movePoint.parent = null;
                }
            }

        }
        else if (logsTouched == 0 && col.gameObject.tag == "Water" && !dead && !running)
        {
            Respawn();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Platform" || col.gameObject.tag == "CheckPoint" ) && !dead)
        {
            transform.parent = null;
            movePoint.parent = null;
            
            logsTouched--;
            
            if (col.gameObject.tag == "CheckPoint")
            {
                bonus.enabled = false;
                Destroy(col);
            }
        }
        else if (col.gameObject.tag == "Level" && !dead)
        {
            if (!levelsToDestroy.Contains(col.gameObject))
            {
                levelsToDestroy.Add(col.gameObject);
            }
            
            if(levelsToDestroy.Count >= 5)
            {
                GameObject temp = levelsToDestroy[0];
                levelsToDestroy.RemoveAt(0);
                Destroy(temp);
            }
        }

    }

   

    public void Respawn()
    {
       
        if (!dead) 
        { 
            SoundFx[1].Play();
            dead = true;
            GetComponent<SpriteRenderer>().enabled = false;
            lossMenu.SetActive(true);
        }

    }

    public void AddPoint()
    {
        if(counter_check%5 == 0 && counter_check >= 5)
        {
            counter += 2*counter/3;
            bonus.enabled = true;
            SoundFx[2].Play();
        }
        else
        {
            counter += 1;
        }
        counter_check += 1;
        counterText.text = Mathf.Round(counter).ToString();
        counterText1.text = Mathf.Round(counter).ToString();
    }
    


}
