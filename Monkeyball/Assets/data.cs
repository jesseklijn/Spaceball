using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class data : MonoBehaviour {
    public int currwaypoint = 0;
    public List<GameObject> Waypoints = new List<GameObject>();
    GameObject[] waypoint;
	// Use this for initialization
	void Start () {
        waypoint = GameObject.FindGameObjectsWithTag("point").OrderBy( go => go.name ).ToArray();
        foreach (var wp in waypoint)
        {
            Waypoints.Add(wp);

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
