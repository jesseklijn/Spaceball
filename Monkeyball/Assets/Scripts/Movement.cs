using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public static Movement movement;
    public GameObject player;
    public GameObject camera;
    Gamestats gamestats;
    public bool paused = true;
    public bool gamepause = false;
    GameObject gameguiobject;
    public GameGUI gameGUI;
    // Use this for initialization
    void Awake()
    {
        //if (movement == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //    movement = this;
        //}
        //else if (movement != this)
        //{
        //    Destroy(gameObject);
        //}
    }
    void Start()
    {
        gameguiobject = GameObject.FindGameObjectWithTag("GameGUI");
        if (gameguiobject != null)
        {
            gameGUI = gameguiobject.GetComponent<GameGUI>();

        }
      
            gamestats = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Gamestats>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameGUI.nextlevel == false)
        {
           

            if (paused == false)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    gamestats = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Gamestats>();
                }
                if (gamestats != null)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        player.GetComponent<Rigidbody>().AddForce(camera.transform.forward * gamestats.speed);
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        player.GetComponent<Rigidbody>().AddForce(camera.transform.right * -gamestats.speed);
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        player.GetComponent<Rigidbody>().AddForce(camera.transform.forward * -gamestats.speed);
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        player.GetComponent<Rigidbody>().AddForce(camera.transform.right * gamestats.speed);
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        player.GetComponent<Rigidbody>().AddForce(camera.transform.forward * 40);
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        player.GetComponent<Rigidbody>().AddForce(camera.transform.right * -40);
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        player.GetComponent<Rigidbody>().AddForce(camera.transform.forward * -40);
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        player.GetComponent<Rigidbody>().AddForce(camera.transform.right * 40);
                    }

                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gamepause = !gamepause;
                if (gamepause == true) { Time.timeScale = 0; }
                else { Time.timeScale = 1; }
            }
        }
        else
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
