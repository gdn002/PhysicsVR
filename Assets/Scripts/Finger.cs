using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    public float minRotation = 10f;
    public float maxRotation = 120f;

    private Transform t;

    private void Awake()
    {
        t = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void rotate(float direction)
    {
            t.Rotate(new Vector3(0f, 0f, direction));
    }

}
