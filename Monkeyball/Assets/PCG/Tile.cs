using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public enum tileType
    {
        Path,
        Start,
        Goal,
        Obstacle,
        None
    }

    public tileType type;
}
