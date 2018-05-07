using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
    public float rotatespeed = 20;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(-(rotatespeed * Time.deltaTime), 0, 0);
	}
}
