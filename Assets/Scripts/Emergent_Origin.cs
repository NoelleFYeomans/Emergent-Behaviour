using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Emergent_Origin : MonoBehaviour
{
    public GameObject plane;
    public GameObject prefabCube;
    public GameObject[] cubeArray;
    //create array of transform.position to compare against for the cube array maybe?

    private int maxCubes = 100;

    private float xMin;
    private float xMax;
    private float zMin;
    private float zMax;
    private float xRand;
    private float zRand;

    // Start is called before the first frame update
    void Start()
    {
        cubeArray = new GameObject[maxCubes];

        //temp, make spawn space relative to plane size
        xMin = -24;
        xMax = 24;
        zMin = -24;
        zMax = 24;
        
        dispenseCubes();
    }

    // Update is called once per frame
    void Update()
    {
        //I want to dispense cubes over time rather than all at once

        //I want the cubes to move away from one another, move to different script
    }

    public void dispenseCubes() //this dispenses the cubes
    {
        for (int i = 0; i < maxCubes; i++)
        {
            cubeArray[i] = Instantiate(prefabCube);

            cubeArray[i].transform.position = genRandomCoords();//protect spawn positions
            
            //protect spawn position(cannot spawn within 1 x/z of one another
        }
    }

    public Vector3 genRandomCoords() //gens random x/z coordinates, might mess with Y later into project
    {
        xRand = Random.Range(xMin, xMax);
        zRand = Random.Range(zMin, zMax);

        return new Vector3(xRand, 0, zRand);
    }
}
