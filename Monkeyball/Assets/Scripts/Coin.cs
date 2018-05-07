using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public int points = 1;
    public GameObject particle;
    AudioSource audiosource;
    GameObject scenemaster;
    Scenemaster scenemeister;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        audiosource = player.GetComponent<AudioSource>();
        scenemaster = GameObject.FindGameObjectWithTag("Scenemaster");
        if (scenemaster != null)
        {
            scenemeister = scenemaster.GetComponent<Scenemaster>();

        }
	}

    void OnTriggerEnter()
    {
        scenemeister.score += points;
        Instantiate(particle, gameObject.transform.position, Quaternion.identity);
        audiosource.GetComponent<AudioSource>().clip = scenemeister.sounds[Random.Range(0, scenemeister.sounds.Count)];
         audiosource.GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }
}
