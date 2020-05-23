using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phalanx : MonoBehaviour
{
    public float minRotation = 260f;
    public float maxRotation = 347f;

    //public GameObject fingerPiece;

    private Transform t;
    public bool Contact { get; private set; }

    private void Awake()
    {
        Contact = false;
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //limits
        if (t.localRotation.eulerAngles.z > maxRotation)
            t.localRotation = Quaternion.Euler(t.localRotation.eulerAngles.x, t.localRotation.eulerAngles.y, maxRotation);
        else 
        if (t.localRotation.eulerAngles.z < minRotation)
            t.localRotation = Quaternion.Euler(t.localRotation.eulerAngles.x, t.localRotation.eulerAngles.y, minRotation);

    }

    public bool Close(float direction)
    {
        if (!Contact)
            t.Rotate(new Vector3(0f, 0f, direction));
        return Contact;
    }

    public void Open(float direction)
    {
        t.Rotate(new Vector3(0f, 0f, direction));
    }

    private void OnTriggerEnter(Collider other)
    {
        Contact = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Contact = false;
    }
}
