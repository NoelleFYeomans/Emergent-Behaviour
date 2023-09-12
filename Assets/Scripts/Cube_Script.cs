using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Script : MonoBehaviour
{
    public GameObject origin;
    public GameObject[] Array;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move away from the nearest cube only

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ActiveCube") //this will create 2 cubes on collision.
        {
            //create new cube at collision coordinates, perhaps send signal to origin, since that's its job.
        }
    }
}
