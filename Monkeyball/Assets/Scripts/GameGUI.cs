using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour
{
    public bool nextlevel = false;
    public bool menu = false;
    public bool record = false;
    public float recordi;
    Movement movement;
    GameObject player;
    float button_width = 200;
    float button_height = 80;
    Scenemaster scenemaster;
    // Use this for initialization
    void Start()
    {
        scenemaster = GameObject.FindGameObjectWithTag("Scenemaster").GetComponent<Scenemaster>();
        player = GameObject.FindGameObjectWithTag("Game");
        if (player != null) { movement = player.GetComponent<Movement>(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.gamepause == true) menu = true;
        else { menu = false;  }
    }
    void OnGUI()
    {
        if (menu)
        {
            //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            GUI.Box(new Rect(10, 10, 210, ((button_height * 4) + (6 * 15))), "");
            if (GUI.Button(new Rect(15, 15, button_width, button_height), "Resume")) { movement.gamepause = false; }
            if (GUI.Button(new Rect(15, (button_height + 15), button_width, button_height), "Options")) { }//TODO CREATE OPTIONS
            if (GUI.Button(new Rect(15, ((button_height * 2) + 15), button_width, button_height), "Retry level")) { Time.timeScale = 1; Application.LoadLevel(Application.loadedLevel); }
            if (GUI.Button(new Rect(15, ((button_height * 3) + 15), button_width, button_height), "Exit to mainmenu")) { Time.timeScale = 1; Application.LoadLevel(0); }
            if (GUI.Button(new Rect(15, ((button_height * 4) + 15), button_width, button_height), "Exit game")) {  Application.LoadLevel(0); }
        }
        if (nextlevel)
        {
            if (record == true)
            {
                GUI.Box(new Rect(220, 15, 210, ((button_height * 4) + (6 * 15))), "New record!: " + scenemaster.record);


            }

            if (GUI.Button(new Rect(15, 15, button_width, button_height), "Options")) { } //TODO CREATE OPTIONS
            if (GUI.Button(new Rect(15, ((button_height * 1) + 15), button_width, button_height), "Previous level")) { Application.LoadLevel(Application.loadedLevel - 1); }
            if (GUI.Button(new Rect(15, ((button_height * 2) + 15), button_width, button_height), "Retry level")) { Application.LoadLevel(Application.loadedLevel); }
            if (GUI.Button(new Rect(15, ((button_height * 3) + 15), button_width, button_height), "Next level")) { Application.LoadLevel(Application.loadedLevel + 1); }
            if (GUI.Button(new Rect(15, ((button_height * 4) + 15), button_width, button_height), "Exit to mainmenu")) { Application.LoadLevel(0); }

        }

    }
}
