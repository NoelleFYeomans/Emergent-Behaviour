using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Emergent_Origin : MonoBehaviour //yellow cubes destroy if they idle/stop, red cubes despawn after 15 seconds(can be respawned)
{
    public GameObject plane; //the ground
    public GameObject prefabCube; //this is the prefab being instantiated
    public GameObject repulsingCube; //the single cube that will repel all others
    public GameObject[] cubeArray; //array of the prefabs after instantiation
    public GameObject[] rCubeArray; //array that will contain the player spawned repulsing cubes

    private Renderer rend;

    public float UID; //a float for the Unique Identification value assigned to each cube.

    private int maxCubes = 100; //change after testing
    private int maxRCubes = 5; //I don't want more than this
    private int countRCubes;
    private bool noSpawn;

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
        rCubeArray = new GameObject[maxRCubes];
        UID = 0;
        countRCubes = 0;
        noSpawn = false;

        rend = plane.GetComponent<Renderer>(); //gets the plane's... dimensions?
        setBounds(rend.bounds.max, rend.bounds.min); //sets the spawning boundaries

        createCubes();
    }
    private void setBounds(Vector3 rendMax, Vector3 rendMin)
    {
        xMin = (rendMin.x + 1);
        xMax = (rendMax.x - 1);
        zMin = (rendMin.z + 1);
        zMax = (rendMax.z - 1);
        //Debug.Log(xMin + " " + xMax + " " + zMin + " " + zMax);
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Space")) && (!noSpawn))
        {
            if (countRCubes < maxRCubes)
            {
                rCubeArray[countRCubes] = Instantiate(repulsingCube);

                rerollRCoords: //goto reroll label

                rCubeArray[countRCubes].transform.position = genRandomCoords();

                for (int i = 0; i < countRCubes; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (rCubeArray[j].transform.position == cubeArray[i].transform.position) //spawn protection
                        {
                            goto rerollRCoords; //reroll pos
                        }
                    }
                }

                for (int i = 0; i < cubeArray.Length; i++)
                {
                    if (rCubeArray[countRCubes].transform.position == cubeArray[i].transform.position) //spawn protection
                    {
                        goto rerollRCoords; //reroll pos
                    }
                }

                countRCubes++;

                if (countRCubes >= 5)
                {
                    noSpawn = true;
                }
            }
        }
        //when you press space, spawn a Repulsing Cube @ random coords
    }
    public void createCubes() //this dispenses the cubes
    {
        for (int i = 0; i < maxCubes; i++)
        {
            cubeArray[i] = Instantiate(prefabCube);

            prefabCube.GetComponent<Cube_Script>().id = UID;
            UID++;

            rerollCoords:

            cubeArray[i].transform.position = genRandomCoords();

            for (int j = 0; j < i; j++)
            {
                if (cubeArray[j].transform.position == cubeArray[i].transform.position) //spawn protection
                {
                    goto rerollCoords;
                }
            }
        }
    }
    public Vector3 genRandomCoords() //gens random x/z coordinates
    {
        xRand = Mathf.Round(Random.Range(xMin, xMax));
        zRand = Mathf.Round(Random.Range(zMin, zMax));

        return new Vector3(xRand, 0, zRand);
    }
}
