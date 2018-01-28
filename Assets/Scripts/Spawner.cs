﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Vector4[] MinMaxPosition;

    public GameObject npcPrefab;
    int totalNpc;

    int totalBigRoom;
    int currentBigRoom;
    int totalRoom1;
    int currentRoom1;
    int totalRoom2;
    int currentRoom2;

    // Use this for initialization
    void SpawnNpc () {
        currentBigRoom = 0;
        currentRoom1 = 0;
        currentRoom2 = 0;
        totalNpc = Random.Range(27,36);
        totalBigRoom = totalNpc / 2;
        totalRoom1 = totalBigRoom / 2;
        totalRoom2 = totalRoom1;

        for (int i = 0; i < totalNpc; i++)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    if (currentBigRoom < totalBigRoom)
                    {
                        SpawnNpcAt(new Vector3(Random.Range(MinMaxPosition[0].x, MinMaxPosition[0].z), 0.8f, Random.Range(MinMaxPosition[0].y, MinMaxPosition[0].w)));
                        currentBigRoom++;
                    }
                    break;
                case 1:
                    if ( currentRoom1 < totalRoom1)
                    {
                        SpawnNpcAt(new Vector3(Random.Range(MinMaxPosition[1].x, MinMaxPosition[1].z), 0.8f, Random.Range(MinMaxPosition[1].y, MinMaxPosition[1].w)));
                        currentRoom1++;
                    }
                    break;
                case 2:
                    if (currentRoom2 < totalRoom2)
                    {
                        SpawnNpcAt(new Vector3(Random.Range(MinMaxPosition[2].x, MinMaxPosition[2].z), 0.8f, Random.Range(MinMaxPosition[2].y, MinMaxPosition[2].w)));
                        currentRoom2++;
                    }
                    break;

            }

        }
	}

    void SpawnNpcAt(Vector3 pos)
    {
        Instantiate(npcPrefab,pos,Quaternion.identity);
    }
	
	
}
