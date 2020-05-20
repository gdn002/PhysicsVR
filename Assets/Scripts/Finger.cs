using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    public float minRotation = 260f;
    public float maxRotation = 347f;

    //public GameObject fingerPiece;

    private Transform t;
    private bool contact = false;

    private void Awake()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //limits
        if (t.localRotation.eulerAngles.z > maxRotation)
            t.localRotation = Quaternion.Euler(0f,0f,maxRotation);
        else 
        if (t.localRotation.eulerAngles.z < minRotation)
            t.localRotation = Quaternion.Euler(0f, 0f, minRotation);

    }

    public bool close(float direction)
    {
        if (!contact)
            t.Rotate(new Vector3(0f, 0f, direction));
        return contact; //return detected collisions to parent to stop al fingers
    }

    public void open(float direction)
    {
        t.Rotate(new Vector3(0f, 0f, direction));
    }

    private void OnTriggerEnter(Collider other)
    {
        contact = true;
    }

    private void OnTriggerExit(Collider other)
    {
        contact = false;
    }
}
