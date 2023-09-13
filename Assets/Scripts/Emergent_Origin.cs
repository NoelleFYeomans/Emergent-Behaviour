using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Emergent_Origin : MonoBehaviour
{
    public GameObject plane; //the ground
    public GameObject prefabCube; //this is the prefab being instantiated
    public GameObject[] cubeArray; //array of the prefabs after instantiation

    public float UID; //a float for the Unique Identification value assigned to each cube.

    private int maxCubes = 100; //change after testing

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
        UID = 0;

        //temp, make spawn space relative to plane size
        xMin = -24;
        xMax = 24;
        zMin = -24;
        zMax = 24;
        
        createCubes();
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to update here
    }

    public void createCubes() //this dispenses the cubes
    {
        for (int i = 0; i < maxCubes; i++) //goto sucks btw
        {
            cubeArray[i] = Instantiate(prefabCube);

            prefabCube.GetComponent<Cube_Script>().id = UID;
            UID++;

            cubeArray[i].transform.position = genRandomCoords();
            
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
