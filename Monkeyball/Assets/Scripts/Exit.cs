using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour
{
    public GameObject firework;
    public GameObject[] fireworks;
    GameObject gameguiobject;
    public GameGUI gameGUI;
    Scenemaster scenemaster;
    Gamestats gamestats;
    SaveLoad saveload;
    int value;
    GameObject gamestatsg;
    // Use this for initialization
    void Start()
    {
        gamestatsg = GameObject.FindGameObjectWithTag("gamestats");
        if (gamestatsg != null)
        {
            saveload = gamestatsg.GetComponent<SaveLoad>();
            gamestats = gamestatsg.GetComponent<Gamestats>();
        }
        scenemaster = GameObject.FindGameObjectWithTag("Scenemaster").GetComponent<Scenemaster>();
        gameguiobject = GameObject.FindGameObjectWithTag("GameGUI");
        if (gameguiobject != null)
        {
            gameGUI = gameguiobject.GetComponent<GameGUI>();


        }
        if (gamestats != null)
        {
            
            saveload.LoadLevel(gamestats.profile_name, Application.loadedLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        if (gamestats != null)
        {
            gamestats.moneypoints += ((scenemaster.score + 1) * (100 / scenemaster.timer)) * 10;
            if (scenemaster.record == 0)
            {
                scenemaster.record = scenemaster.timer;
                for (int i = 0; i < fireworks.Length; i++)
                {
                    Instantiate(firework, fireworks[i].transform.position, Quaternion.identity);
                }
            }
            else if (scenemaster.timer < scenemaster.record)
            {
                scenemaster.record = scenemaster.timer;

                for (int i = 0; i < fireworks.Length; i++)
                {
                    Instantiate(firework, fireworks[i].transform.position, Quaternion.identity);
                }
            }

            if (gamestats.LevelS.ContainsKey(Application.loadedLevel) == true)
            {
                if (gamestats.LevelS[Application.loadedLevel] < ((scenemaster.score + 1) * (100 / scenemaster.timer)) * 10)
                {
                    gamestats.LevelS[Application.loadedLevel] = ((scenemaster.score + 1) * (100 / scenemaster.timer)) * 10;
                }
                else
                {


                }


            }
            else
            {
                gamestats.LevelS[Application.loadedLevel] = ((scenemaster.score + 1) * (100 / scenemaster.timer)) * 10;

            }
            Debug.Log(gamestats.LevelS[Application.loadedLevel]);

            if (scenemaster.timer <= scenemaster.time1 && scenemaster.timer <= scenemaster.record)
            {

                gameGUI.record = true;
                gamestats.LevelScore[Application.loadedLevel] = 3;
                Debug.Log("Found Score 3!");



            }

            else if (scenemaster.timer <= scenemaster.time2 && scenemaster.timer <= scenemaster.record)
            {
                gameGUI.record = true;

                gamestats.LevelScore[Application.loadedLevel] = 2;
                Debug.Log("Found score 2");


            }
            else if (scenemaster.timer <= scenemaster.time3 && scenemaster.timer <= scenemaster.record)
            {


                gamestats.LevelScore[Application.loadedLevel] = 1;
                Debug.Log("Found score 1");
                gameGUI.record = true;

            }



            saveload.SaveLevel(gamestats.profile_name, Application.loadedLevel, scenemaster.record);
            saveload.Save(gamestats.profile_name);

            if (collision.gameObject.tag == "Player")
            {
                gameGUI.nextlevel = true;
            }
        }
        else Application.LoadLevel(Application.loadedLevel);
    }
}
