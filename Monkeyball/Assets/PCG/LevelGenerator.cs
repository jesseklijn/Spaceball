using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public List<Material> materials;
    public int seed = 1;

    private float ballSpeed = 3, timeGoal = 60;

    private const float DIVIDER = 4;
    private const int POSSIBLE_ADJACENT = 4;

    public float lengthOfPath = 0;
    private int sizeX, sizeZ;

    public Vector3 startPos, goalPos;


    public bool isSolved = false;

    private int[] rowDelta = { -1, 1, 0, 0 };
    private int[] colDelta = { 0, 0, -1, 1 };

    public int[,] tilePos;

    //public List<Tile> tileGrid = new List<Tile>();

    // Use this for initialization
    private void Awake()
    {
        //Calculate length of path
        lengthOfPath = timeGoal / ballSpeed;


        sizeX = sizeZ = (int)lengthOfPath;
        tilePos = new int[sizeX, sizeZ];
        //Set seed
        Random.InitState(seed);

        //Assign start position
        startPos = new Vector3(lengthOfPath / DIVIDER, 0, lengthOfPath / DIVIDER);
        goalPos = new Vector3(lengthOfPath / DIVIDER, 0, lengthOfPath / DIVIDER);


        do {
            startPos = GenerateStartPosition(startPos);

            ////Assign goal position 
            while (goalPos == new Vector3(lengthOfPath / DIVIDER, 0, lengthOfPath / DIVIDER) | goalPos == startPos)
            {
                goalPos = GenerateStartPosition(goalPos);

            }
        } while (((goalPos.x - startPos.x) + (goalPos.z - startPos.z)) % 2 != 0 || AccrossFromGoal(startPos));

        //Add start and goal position to position list
        AddPositionToList(startPos);
        //AddPositionToList(goalPos);
        tilePos[(int) goalPos.x, (int) goalPos.z] = 3;

        //Algorithm - Create Path from start to goal position
        FindPath(1, (int)startPos.x, (int)startPos.z);

        //If solved generate path
        if (isSolved)
        {
            GenerateTiles();
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
        tilePos[(int)position.x, (int)position.z] = 1;
        return true;
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
        if (Random.Range(0,10) >= 5)
        {
            rowDelta = new [] { 1, 0, 0, -1 };
            colDelta = new [] { 0, 1, -1, 0 };
        }
        else
        {
            rowDelta = new [] { -1, 1, 0, 0 };
            colDelta = new [] { 0, 0, -1, 1 };
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
        for (int i = 0; i < POSSIBLE_ADJACENT; i++)
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
                if (newX == goalPos.x && newZ == goalPos.z)
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
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                GameObject instance = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                if (tilePos[x, z] == 1)
                {
                    instance.GetComponent<MeshRenderer>().material = materials[0];
                }
                else
                {
                    instance.GetComponent<MeshRenderer>().material = materials[3];
                }

                if (startPos.x == x && startPos.z == z)
                {
                    instance.GetComponent<MeshRenderer>().material = materials[1];
                }
                if (tilePos[x, z] == 3)
                {
                    instance.GetComponent<MeshRenderer>().material = materials[2];
                }

            }
        }
    }
}
