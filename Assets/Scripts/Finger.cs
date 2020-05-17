using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    public float minRotation = 10f;
    public float maxRotation = 94f;

    public GameObject fingerPiece;

    private Transform t;
    private bool contact = false;
    private int bounces = 0;

    private void Awake()
    {
        t = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        float zoff = -0.24f;
        for (int i=0; i<4; i++)
        {
            Instantiate(fingerPiece, transform.position + new Vector3(0.2f,0f,zoff), Quaternion.Euler(0f,180f,90f), transform);
            zoff += 0.16f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (t.localRotation.eulerAngles.z > maxRotation)
            t.localRotation = Quaternion.Euler(0f,0f,maxRotation);
        else 
        if (t.localRotation.eulerAngles.z < minRotation)
            t.localRotation = Quaternion.Euler(0f, 0f, minRotation);

    }

    public void close()
    {
        if (bounces < 5)
        {
            if (!contact)
                t.Rotate(new Vector3(0f, 0f, 1));
            else
                t.Rotate(new Vector3(0f, 0f, -1));
        }
    }

    public void open()
    {
        t.Rotate(new Vector3(0f, 0f, -1));
        bounces = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        contact = true;
        bounces += 1;
    }

    private void OnTriggerExit(Collider other)
    {
        contact = false;
        bounces += 1;
    }
}
