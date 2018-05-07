using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Gamestats : MonoBehaviour {
    public string profile_name;
    public float speed = 20;
    public static Gamestats gamestats;
    public float money;
    public int lives;
    public int level;
    public int chosenball;
    public float moneypoints;
    public Dictionary<int, int> LevelScore = new Dictionary<int, int>();
    public List<Material> balls = new List<Material>();
    public Dictionary<int, float> LevelS = new Dictionary<int, float>();
    public Dictionary<int, string> profiles = new Dictionary<int, string>();
    public string lastplayed;
    public AudioClip a;
    public AudioClip b;
	// Use this for initialization
	void Awake () {
        
        if (gamestats == null)
        {

            DontDestroyOnLoad(gameObject);
            gamestats = this;
        }
        else if (gamestats != this)
        {
            Destroy(gameObject);
        }

	}
	
	// Update is called once per frame
	void Update () {

	}
}
