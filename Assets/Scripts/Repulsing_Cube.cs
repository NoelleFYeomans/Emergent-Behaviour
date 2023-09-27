using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Repulsing_Cube : MonoBehaviour
{
    private int speed;
    private float changeDirTime;
    private float untilDeletion;

    private RaycastHit[] hitList;
    private RaycastHit[] priorHitList;

    private Vector3 randDestination;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        changeDirTime = 0f;
        untilDeletion = 0f;
        speed = 10;
        rb = GetComponent<Rigidbody>();

        randDestination = GameObject.Find("Origin").GetComponent<Emergent_Origin>().genRandomCoords(); //generates initial destination, works
    }
    // Update is called once per frame
    void Update()
    {
        transform.localRotation.Set(0f,0f,0f,0f);
        randDestination = randDestination.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + randDestination);

        //RaycastHit hit;
        changeDirTime += Time.deltaTime;

        hitList = Physics.SphereCastAll(transform.position, 5, transform.forward, 5);

        if (hitList != priorHitList)
        {
            foreach (RaycastHit hit in hitList)
            {
                hit.rigidbody.velocity = Vector3.zero;
                hit.rigidbody.angularVelocity = Vector3.zero; //removes all forces currently acting on cube
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(((hit.transform.position - transform.position) * speed) * speed); //this should add a repelling force
            }
        }

        priorHitList = hitList;
        if (untilDeletion >= 5)
        {
            gameObject.SetActive(false);
            untilDeletion = 0;
        }
        if (changeDirTime > 3f) //generate new destination every 3 seconds
        {
            randDestination = GameObject.Find("Origin").GetComponent<Emergent_Origin>().genRandomCoords(); //generates new destination

            changeDirTime = 0f;
            untilDeletion++;
        }

    }
}
