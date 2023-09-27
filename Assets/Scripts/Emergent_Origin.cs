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
        maxCubes = 100;
        maxRCubes = 5;

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
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Space"))
        {
            foreach (GameObject rCube in rCubeArray)
            {
                if (!rCube.activeSelf)
                {
                    rCube.transform.position = genRandomCoords(); //spawn protect this ig?
                    rCube.SetActive(true);
                    return;
                }
            }

        }

    }
    public void createCubes() //this dispenses the cubes
    {
        for (int i = 0; i < maxCubes; i++) //YELLOW CUBES
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


        } //yellow cubes

        for (int j = 0; j < maxRCubes; j++) //RED CUBES
        {
            rCubeArray[j] = Instantiate(repulsingCube); //creates all the cubes

            rerollRCoords: //reroll coordinates label

            rCubeArray[j].transform.position = genRandomCoords();

            for (int i = 0; i < cubeArray.Length; i++) //compare vs all yellow cube position
            {
                if (rCubeArray[j].transform.position == cubeArray[i].transform.position)
                {
                    goto rerollRCoords; //will reroll coords if this cube matches any existing yellow cube coord
                }
            }

            for (int l = 0; l < (j - 1); l++) //compare vs all other red cubes
            {
                if (rCubeArray[j].transform.position == rCubeArray[l].transform.position)
                {
                    goto rerollRCoords; //reroll pos
                }
            }

            rCubeArray[j].SetActive(false); //makes it so they dont exist on spawn
        } //red cubes

    }
    public Vector3 genRandomCoords() //gens random x/z coordinates
    {
        xRand = Mathf.Round(Random.Range(xMin, xMax));
        zRand = Mathf.Round(Random.Range(zMin, zMax));

        return new Vector3(xRand, 0, zRand);
    }
}
