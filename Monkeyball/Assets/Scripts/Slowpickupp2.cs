using UnityEngine;
using System.Collections;

public class Slowpickupp2 : MonoBehaviour {

	// Use this for initialization
    public Slowpickup pickup;
    void OnTriggerEnter()
    {
        pickup.pickedup = true;
        Destroy(gameObject);

    }
}
