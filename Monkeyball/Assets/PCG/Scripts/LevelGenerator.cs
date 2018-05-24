using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    #region fields
    private GameObject tileParent;
    public GameObject tilePrefab;
    public List<Material> materials;
    public List<GameObject> path;
    public bool useSeed = false;
    public int seed = 1;

    public float ballSpeed = 3, timeGoal = 60, maxObstacles = 3;

    private const float DIVIDER = 4;
    private const int POSSIBLE_ADJACENT = 4;

    private float lengthOfPath = 0;
    private int sizeX, sizeZ;

    public Vector3 startPos, goalPos;


    private bool isSolved = false;

    private int[] rowDelta = { -1, 1, 0, 0 };
    private int[] colDelta = { 0, 0, -1, 1 };

    private int[,] tilePos;
    #endregion
   
    public void ClearLevel()
    {
        if (tileParent == null)
        {
            tileParent = GameObject.Find("TileMap");
        }
        DestroyImmediate(tileParent);

    }
    public void Generate()
    {
        #region Initializing
        //Calculate length of path
        lengthOfPath = timeGoal / ballSpeed;


        sizeX = sizeZ = (int)lengthOfPath;
        tilePos = new int[sizeX, sizeZ];
        //Set seed by inspector or randomly.
        if (useSeed == true)
            Random.InitState(seed);
        else
        {
            seed = Random.Range(0, 36000);
            Random.InitState(seed);
        }

        //Assign start position
        startPos = new Vector3(lengthOfPath / DIVIDER, 0, lengthOfPath / DIVIDER);
        goalPos = new Vector3(lengthOfPath / DIVIDER, 0, lengthOfPath / DIVIDER);


        float distance;
        startPos = GenerateStartPosition(startPos);
        do
        {
            goalPos = GenerateStartPosition(goalPos);
            distance = Math.Abs(goalPos.x - startPos.x) + Math.Abs(goalPos.z - startPos.z);

        } while (distance % 2 != 0 || distance < 3 || distance >= 19 || OutOfBounds(goalPos) || OutOfBounds(startPos)); //|| AccrossFromGoal(startPos));
        #endregion
        //Add start to position list and assign right property
        AddPositionToList(startPos);
        tilePos[(int)goalPos.x, (int)goalPos.z] = 3;

        //Algorithm - Create Path from start to goal position
        FindPath(1, (int)startPos.x, (int)startPos.z);

        //If solved generate path
        if (isSolved)
        {
            GenerateTiles();
            GenerateObstacle(Random.Range(0, (int)maxObstacles));
        }
    }

    private Vector3 GenerateStartPosition(Vector3 position)
    {
        var vReturn = Vector3.zero;
        //Random X
        vReturn.x = (int)Random.Range(position.x - lengthOfPath / DIVIDER, position.x + lengthOfPath / DIVIDER);
        //StartPos Y
        vReturn.y = (int)position.y;
        //Random Z
        vReturn.z = (int)Random.Range(position.z - lengthOfPath / DIVIDER, position.z + lengthOfPath / DIVIDER);

        return vReturn;
    }

    private bool AddPositionToList(Vector3 position)
    {
        if (!OutOfBounds(new Vector3(position.x, 0, position.z)))
        {
            tilePos[(int)position.x, (int)position.z] = 1;
            return true;

        }
        else return false;
    }

    void FindPath(int moveNr, int x, int z)
    {
        if (moveNr == lengthOfPath - 1 && x == goalPos.x & z == goalPos.z)
        {
            isSolved = true;
            return;
        }
        else if (moveNr == lengthOfPath - 1)
            return;
        isSolved = false;


        //Not Solved so try
        if (Random.Range(0, 10) >= 5)
        {
            rowDelta = new[] { 1, 0, 0, -1 };
            colDelta = new[] { 0, 1, -1, 0 };
        }
        else
        {
            rowDelta = new[] { -1, 1, 0, 0 };
            colDelta = new[] { 0, 0, -1, 1 };
        }
        for (int i = 0; i < POSSIBLE_ADJACENT; i++)
        {
            int newX = x + rowDelta[i];
            int newZ = z + colDelta[i];

            if (OutOfBounds(new Vector3(newX, 0, newZ)))
                continue;
            else if (tilePos[newX, newZ] == 1 || (tilePos[x, z] == 3 && moveNr != lengthOfPath - 2))
                continue;
            else if (AdjacentCheck(newX, newZ, moveNr))
                continue;


            if (tilePos[newX, newZ] != 3)
            {
                tilePos[newX, newZ] = 1;
            }
            FindPath(moveNr + 1, newX, newZ);
            if (isSolved) return;
            tilePos[newX, newZ] = 0;


        }
    }

    bool AdjacentCheck(int x, int z, int moveNr)
    {
        int adjacentTiles = 0;
        for (int i = 0; i < rowDelta.Length; i++)
        {
            int newX = x + rowDelta[i];
            int newZ = z + colDelta[i];

            if (!OutOfBounds(new Vector3(newX, 0, newZ)))
            {
                if (tilePos[newX, newZ] == 1 || (tilePos[newX, newZ] == 3 && moveNr < lengthOfPath - 3))
                {
                    adjacentTiles++;
                }
            }
        }

        return adjacentTiles > 1 || AccrossFromGoal(new Vector3(x, 0, z));
    }

    bool AccrossFromGoal(Vector3 vector)
    {
        int[] otherRowDelta = { 1, -1, 1, -1 };
        int[] otherColDelta = { 1, -1, -1, 1 };
        for (int i = 0; i < POSSIBLE_ADJACENT; i++)
        {
            int newX = (int)vector.x + otherRowDelta[i];
            int newZ = (int)vector.z + otherColDelta[i];

            if (!OutOfBounds(new Vector3(newX, 0, newZ)))
            {
                if (tilePos[newX, newZ] == 3)
                {
                    return true;
                }
            }
        }

        return false;
    }

    bool OutOfBounds(Vector3 vector)
    {
        return vector.x > sizeX - 1 | vector.x < 0 || vector.z > sizeZ - 1 | vector.z < 0;
    }

    void GenerateTiles()
    {
        //Clear previous path collection
        path.Clear();

        GameObject parent = new GameObject("TileMap");
        tileParent = parent;
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {

                //Spawn all tiles, based on their appropiate role. Being either a path, goal, start or.. Nothing, by default.
                GameObject instance = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity, parent.transform);
                if (tilePos[x, z] == 1)
                {
                    instance.GetComponent<MeshRenderer>().material = materials[0];
                    instance.GetComponent<Tile>().type = Tile.tileType.Path;
                    path.Add(instance);
                }
                else
                {
                    instance.GetComponent<MeshRenderer>().material = materials[3];
                   //Is by default none
                    path.Add(instance);
                }

                if (startPos.x == x && startPos.z == z)
                {
                    instance.GetComponent<MeshRenderer>().material = materials[1];
                    instance.GetComponent<Tile>().type = Tile.tileType.Start;
                    path.Add(instance);
                }
                if (tilePos[x, z] == 3)
                {
                    instance.GetComponent<MeshRenderer>().material = materials[2];
                    instance.GetComponent<Tile>().type = Tile.tileType.Goal;
                }

            }
        }
    }

    void GenerateObstacle(int amountOfObstacles)
    {
    
        if (amountOfObstacles > 0)
        {
            int obstaclesInPath = 0;
            for (int i = 0; i < amountOfObstacles; i++)
            {
                while (obstaclesInPath != amountOfObstacles)
                {
                    int randomIndex = Random.Range(0, path.Count - 1);
                    if (path[randomIndex].GetComponent<Tile>().type == Tile.tileType.Path)
                    {
                        path[randomIndex].GetComponent<Tile>().type = Tile.tileType.Obstacle;
                        path[randomIndex].GetComponent<MeshRenderer>().material = materials[4];
                        obstaclesInPath++;
                    }
                }
            }
        }

    }
}
