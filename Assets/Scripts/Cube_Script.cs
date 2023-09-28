using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cube_Script : MonoBehaviour 
{
    private GameObject[] array;

    private Rigidbody rb;

    public float id;
    private float speed = 500;

    private float closest; //max possible distance/default
    private float distance;
    private GameObject closestObject;
    private GameObject priorClosestObject;

    // Start is called before the first frame update
    void Start()
    {
        array = GameObject.Find("Origin").GetComponent<Emergent_Origin>().cubeArray; //gets array of cubes from origin
        rb = GetComponent<Rigidbody>(); 
        closestObject = GetClosestObject();
        speed = 500;

        moveCubeAway(); //starts the chain of movement
    }
    // Update is called once per frame
    void Update() //perhaps every other cube should attract rather than repel?
    {
        //transform.localRotation.Set(0f, 0f, 0f, 0f);
        priorClosestObject = closestObject; //sets what the last closest object was
        closestObject = GetClosestObject(); //checks to see if a new object is closer

        if (closestObject != priorClosestObject) //if closest object has changed, change movement direction
        {
            resetPool(rb);

            moveCubeAway();
        }

    }
    public void resetPool(Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    } //resets all forces acting on cube, prevents having multiple forces on a cube at once.
    public void moveCubeAway()
    {
        Vector3 direction = (transform.position - closestObject.transform.position); //obtains the direction to the nearest cube //this is empty
        direction.y = 0; //no need to move on the y axis
        direction = Vector3.Normalize(direction); //makes vector have magnitude of 1
        transform.rotation = Quaternion.Euler(direction); //this, *should* turn the cube away

        //cube moves forward (the calculations above calculate the direction away from the closest)
        rb.AddForce(direction * speed);
    } //actual code that moves the cube
    public GameObject GetClosestObject()
    {
        closest = float.MaxValue;

        for (int i = 0; i < array.Length; i++) //finds nearest cube object.
        {
            if (array[i].transform.position == transform.position) continue;

            distance = Vector3.Distance(array[i].transform.position, transform.position);

            if (distance < closest)
            {
                closest = distance;
                closestObject = array[i];
            }
        }
        return closestObject; //this is the cube in the array that is closest to *this* cube.
    }
    private void OnCollisionStay(Collision collision) //STAY AWAY FROM THE DAMN WALL
    {
        //if (collision.collider.tag == "Wall")
        //{   
        //    rb.AddForce((transform.position + GameObject.Find("Origin").GetComponent<Transform>().transform.position) * speed);
        //}
    }
}
