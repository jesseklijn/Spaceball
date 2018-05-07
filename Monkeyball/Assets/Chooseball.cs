using UnityEngine;
using System.Collections;

public class Chooseball : MonoBehaviour
{
    float time = 3;
    public TextMesh text;
    public TextMesh msg;
    public int reqlevel = 0;
    public int cost = 0;
    public bool bought = false;
    public GameObject ball;
    public string ballname;
    public int chosenball;
    bool balli;
    Vector3 balls = new Vector3(2.5f, 2.5f, 2.5f);
    Gamestats gamestats;
    SaveLoad saveload;
    // Use this for initialization
    void Start()
    {
        saveload = GameObject.FindGameObjectWithTag("gamestats").GetComponent<SaveLoad>();
        gamestats = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Gamestats>();
        msg = GameObject.FindGameObjectWithTag("msg").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (msg.text != "")
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                
                msg.text = "";
                time = 3;
            }

        }
        if (balli == true)
        {
            if ((ball.transform.localScale.x < 2.5f))
            {
                ball.transform.localScale += new Vector3(0.5F * Time.deltaTime, 0.5F * Time.deltaTime, 0.5F * Time.deltaTime);
            }
        }
        else
        {
            if ((ball.transform.localScale.x > 1f))
            {
                ball.transform.localScale -= new Vector3(0.5F * Time.deltaTime,0.5F * Time.deltaTime, 0.5F * Time.deltaTime);
            }

        }
    }
    void OnMouseDown()
    {
        if (reqlevel >= gamestats.money)//TODO add levels
        {
            if (cost <= gamestats.moneypoints)
            {
                gamestats.moneypoints -= cost;
                gamestats.chosenball = chosenball;
                saveload.Save(gamestats.profile_name);
            }
            else
                msg.text = "Not enough money :(.";
        }
        else
            msg.text = "Not enough stars :(."; 
    }
    void OnMouseEnter()
    {
        balli = true;
        text.text = ballname + " Cost: " + cost;
        text.color = new Color(1, 1, 1);
    }
    void OnMouseExit()
    {
        balli = false;
        text.text = "";
    }
}
