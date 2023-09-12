using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Script : MonoBehaviour
{
    public GameObject origin;
    public GameObject[] Array;

    public float id;

    private float closest; //max possible distance/default
    private float distance;
    private GameObject closestObject;

    // Start is called before the first frame update
    void Start()
    {
        Array = origin.GetComponent<Emergent_Origin>().cubeArray; 
    }

    // Update is called once per frame
    void Update()
    {
        //move away from get closest object
        
    }

    public GameObject GetClosestObject()
    {
        closest = float.MaxValue;

        for (int i = 0; i < Array.Length; i++) //finds nearest cube object.
        {
            if (id == i) continue; //guard clause to prevent being the closest to yourself

            distance = Vector3.Distance(Array[i].transform.position, this.transform.position);

            if (distance < closest)
            {
                closest = distance;
                closestObject = Array[i];
            }
        }

        return closestObject; //this is the cube in the array that is closest to *this* cube.

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ActiveCube") //this will create 2 cubes on collision.
        {
            //create new cube at collision coordinates, perhaps send signal to origin, since that's its job.
        }
    }
}
