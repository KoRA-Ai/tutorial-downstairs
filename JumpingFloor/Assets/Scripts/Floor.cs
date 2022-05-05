using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    //this is for making floors move/dissappear
    
    [SerializeField] float movespeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        transform.Translate(0, movespeed * Time.deltaTime, 0);

        //
        if (transform.position.y > 6f)
        {
            //delete floor
            Destroy(gameObject);

            //Floor.cs is child
            //FloorManager is parent
            //to call method from FloorManager 
            transform.parent.GetComponent<FloorManager>().SpawnFloor();
        }
    }
}
