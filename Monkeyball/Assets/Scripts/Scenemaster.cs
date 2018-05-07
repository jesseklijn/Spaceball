using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Scenemaster : MonoBehaviour
{
    public AudioSource soundeffects;
    public AudioSource audiosource;
    public AudioClip audio;
    SaveLoad saveload;
    Gamestats gamestats;
    public int level;
    public float score;
    public float time;
    public float time1;
    public float time2;
    public float time3;
    public float record;
    public GUIText Tscore;
    public GUIText Ctime;
    public GUIText Ttime;
    public Movement movement;
    GameObject gameguiobject;
    public GameGUI gameGUI;
    bool game_started = false;
    public float timer;
    GameObject player;
    public List<AudioClip> sounds = new List<AudioClip>();
    GameObject ball;
    MainMenuGUI maingui;
    GameObject gamestatsg;
    // Use this for initialization
    void OnEnable()
    {
        gamestatsg = GameObject.FindGameObjectWithTag("gamestats");
        if (gamestatsg != null)
        {
            saveload = gamestatsg.GetComponent<SaveLoad>();
            maingui = gamestatsg.GetComponent<MainMenuGUI>();

            maingui.enabled = false;
        }
        else
        {
            game_started = true;
            movement.paused = false;
        }
        if (gamestats != null)
        {
            saveload.LoadLevel(gamestats.profile_name, Application.loadedLevel);
        }

    }
    void OnDisable()
    {
        maingui.enabled = true;
    }
    void Awake()
    {
        if (gamestats != null)
        {
            ball = GameObject.FindGameObjectWithTag("Player");
            gamestats = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Gamestats>();
            ball.GetComponent<Renderer>().material = gamestats.balls[gamestats.chosenball];
        }
    }
    void Start()
    {
        soundeffects = GameObject.FindGameObjectWithTag("Scenemaster").GetComponent<AudioSource>();
        if (audio != null)
        {
            audiosource = GameObject.FindGameObjectWithTag("gamestats").GetComponent<AudioSource>();
            audiosource.clip = audio;
            audiosource.Play();

        }
        player = GameObject.FindGameObjectWithTag("Game");
        if (player != null) { movement = player.GetComponent<Movement>(); }
        gameguiobject = GameObject.FindGameObjectWithTag("GameGUI");
        if (gameguiobject != null)
        {
            gameGUI = gameguiobject.GetComponent<GameGUI>();

        }

        Tscore = GameObject.FindGameObjectWithTag("Score").GetComponent<GUIText>();
        Ctime = GameObject.FindGameObjectWithTag("Countdown").GetComponent<GUIText>();
        Ttime = GameObject.FindGameObjectWithTag("Time").GetComponent<GUIText>();
        StartCoroutine(MyMethod());

    }

    // Update is called once per frame
    void Update()
    {

        Tscore.text = "" + score;
        if (gameGUI.nextlevel != true)
        {
            if (game_started == true)
            {
                Ttime.text = "" + System.Math.Round((timer += Time.deltaTime), 2).ToString();

            }
        }

    }
    public void a()
    {
        gamestats = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Gamestats>();
        soundeffects.clip = gamestats.a;
    }
    public void b()
    {
        gamestats = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Gamestats>();
        soundeffects.clip = gamestats.b;

    }
    IEnumerator MyMethod()
    {
       
        yield return new WaitForSeconds(0.5F);
        Ctime.text = "3";
        a();
        soundeffects.Play();
        yield return new WaitForSeconds(1);
        Ctime.text = "2";

        soundeffects.Play();
        yield return new WaitForSeconds(1);
        soundeffects.Play();
        Ctime.text = "1";
        yield return new WaitForSeconds(1);
        b();
        soundeffects.Play();
        Ctime.text = "GO!";
        yield return new WaitForSeconds(0.5F);
        Ctime.text = "";
        unpause();
    }
    private void unpause()
    {
        movement.paused = false;
        game_started = true;

    }
}

