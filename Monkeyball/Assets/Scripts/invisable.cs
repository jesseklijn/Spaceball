using UnityEngine;
using System.Collections;

public class invisable : MonoBehaviour
{
    public Material material;
    bool steppedon;
    bool binvisable;
    float timer = 1;
    float itimer = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (steppedon)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.2F);


        }
        else
        {

            StartCoroutine(MyMethod());


        }
    }
    IEnumerator MyMethod()
    {
        if (gameObject.GetComponent<Renderer>().material.color.a >= 0)
        {
            for (int i = 0; i < itimer; i++)
            {
                yield return new WaitForSeconds(0.1F);
                if (gameObject.GetComponent<Renderer>().material.color.a - 0.05F >= 0)
                    gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, gameObject.GetComponent<Renderer>().material.color.a - 0.1F);
            }
        }






    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            steppedon = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            steppedon = false;
        }
    }


}
