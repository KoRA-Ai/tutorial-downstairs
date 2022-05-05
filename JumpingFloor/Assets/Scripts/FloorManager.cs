using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour 
{
    // this is for creating floors

    [SerializeField] GameObject[] floorPrefabs;

    

   public void SpawnFloor()
    {
        int r = Random.Range(0, floorPrefabs.Length);
        GameObject floor = Instantiate(floorPrefabs[r], transform);
        //random x, spawn at -6f (bottom ground)
        floor.transform.position = new Vector3(Random.Range(-3.8f, 3.8f), -6f, 0f);
    }
}
