using UnityEngine;
using System.Collections;

public class Speed : MonoBehaviour {
    Gamestats gamestats;
    public float speed;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.tag == "Player")
        {

           gamestats = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Gamestats>();
           gamestats.speed += speed;
           Debug.Log("Speed given");
        }

    }
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit");
        if (collision.gameObject.tag == "Player")
        {
            gamestats = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Gamestats>();
            gamestats.speed -= speed;
            Debug.Log("Speed taken");
        }


    }
}
