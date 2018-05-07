using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Controll : MonoBehaviour
{
    float speed = 10;
    public bool right;
    public bool left;
    public TextMesh text;
    public GameObject camera;
    data Data;
    public float step;
    public bool move = false;
    float timer = 3;
    Vector3 pw;
    float distance;
    // Use this for initialization
    void Start()
    {
        Data = GameObject.FindGameObjectWithTag("data").GetComponent<data>();
    }
    void OnMouseDown()
    {
        if (move != true)
        {
            if (left == true)
            {
                Data.currwaypoint -= 1;
                Debug.Log(Data.currwaypoint + " " + Data.Waypoints.Count);
                if ((Data.currwaypoint) >= 0)
                {
                   
                    //camera.transform.position =;
                    move = true;
                    Data.currwaypoint += 1;
                    distance = Vector3.Distance(camera.transform.position, Data.Waypoints[(Data.currwaypoint - 1)].transform.position);
                    
                    pw = Data.Waypoints[(Data.currwaypoint - 1)].transform.position;
                    Data.currwaypoint -= 1;

                }
                else
                    Data.currwaypoint += 1;
            }
            if (right == true)
            {
                Data.currwaypoint += 1;
                Debug.Log(Data.currwaypoint + " " + Data.Waypoints.Count);
                if (Data.currwaypoint + 1 <= (Data.Waypoints.Count))
                {
                    
                    move = true;
                    Data.currwaypoint -= 1;
                    distance = Vector3.Distance(camera.transform.position, Data.Waypoints[(Data.currwaypoint + 1)].transform.position);
                    pw = Data.Waypoints[(Data.currwaypoint + 1)].transform.position;
                    Data.currwaypoint += 1;
                }
                else
                    Data.currwaypoint -= 1;

            }
        }
        //Debug.Log("Previous:" + (currwaypoint - 1));
        //Debug.Log("curr: " + currwaypoint);
        //Debug.Log("Next:" + (currwaypoint + 1));
    }
    void OnMouseEnter()
    {
        text.color = new Color(0.5F, 1, 0.1F);
    }
    void OnMouseExit()
    {
        text.color = new Color(1, 1, 1);
    }
    void Update()
    {
        step = speed *Time.deltaTime;
        
        if (move == true)
        {
            timer -= Time.deltaTime;
            if (right)
            {
                
                camera.transform.position = Vector3.MoveTowards(camera.transform.position, pw, step);
                
            }

            if (left)
            {

               
                camera.transform.position = Vector3.MoveTowards(camera.transform.position, pw, step);
                
            }
            if (timer <= 0)
            {
                timer = distance / speed;
                move = false;
            }

        }
    }

}
