using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    public bool up = false;
    public bool forward = true;
    public bool left = false;
    public float movespeed = 5;
    public float movedistance = 5;
    public GameObject cube;
    private float xStartPosition;
    private float yStartPosition;
    private float zStartPosition;
    // Use this for initialization
    void Start()
    {
        xStartPosition = transform.position.x;
        yStartPosition = transform.position.y;
        zStartPosition = transform.position.z;
    }

    void Update()
    {


        if (forward)
        {
            if (transform.position.x < xStartPosition || transform.position.x > xStartPosition + movedistance)
            {
                movespeed *= -1;
            }
            transform.position = new Vector3(transform.position.x + movespeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (up)
        {
            if (transform.position.y < yStartPosition || transform.position.y > yStartPosition + movedistance)
            {
                movespeed *= -1;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y + movespeed * Time.deltaTime, transform.position.z);

        }
        if (left)
        {
            if (transform.position.z < zStartPosition || transform.position.z > zStartPosition + movedistance)
            {
                movespeed *= -1;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + movespeed * Time.deltaTime);
        }

    }

}
