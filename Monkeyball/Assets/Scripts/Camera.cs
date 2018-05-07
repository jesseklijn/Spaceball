using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    public GameObject player;
    GameObject gameguiobject;
    public GameGUI gameGUI;
    float timer = 0;
    bool s = false;
    // Use this for initialization
    void Start()
    {
        gameguiobject = GameObject.FindGameObjectWithTag("GameGUI");
        if (gameguiobject != null)
        {
            gameGUI = gameguiobject.GetComponent<GameGUI>();

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameGUI.nextlevel == false)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                gameObject.transform.Rotate(0, -(100 * Time.deltaTime), 0);
            }
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.transform.Rotate(0, (100 * Time.deltaTime), 0);
            }
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 4, player.transform.position.z);
        }
        else
        {
            if (timer <= 0)
            {

                timer = 5;
                s = !s;

            }
            if (s) { gameObject.transform.Translate((Vector3.up * Time.deltaTime), Space.World); }
            else
            {
                gameObject.transform.Translate((Vector3.down * Time.deltaTime), Space.World);
            }
            gameObject.transform.Rotate(0, -(20 * Time.deltaTime), 0);
            timer -= Time.deltaTime;
          
        }
    }



}
