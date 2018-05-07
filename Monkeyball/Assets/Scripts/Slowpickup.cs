using UnityEngine;
using System.Collections;

public class Slowpickup : MonoBehaviour
{

    public bool pickedup = false;
    public float timer = 1;
    //public GameObject parent;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (pickedup == true)
        {
            Debug.Log("swag");


            Time.timeScale = 0.2F;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            timer -= Time.deltaTime;
        }
        
        if (timer <= 0)
        {
            Debug.Log("swag");
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            Destroy(gameObject.gameObject);
        }

    }
}



