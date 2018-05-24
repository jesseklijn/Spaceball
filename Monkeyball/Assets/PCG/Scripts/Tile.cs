using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    //Define the type of tiles that exist in the level
    public enum tileType
    {
        Path,
        Start,
        Goal,
        Obstacle,
        None
    }
    //Create an instance of the tile type that can be edited externally
    public tileType type;
}
