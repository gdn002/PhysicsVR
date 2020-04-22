using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VirtualHandBehaviour : MonoBehaviour
{
    public Transform parent;

    public SteamVR_Action_Vibration Haptics;
    public SteamVR_Input_Sources handType;

    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(parent.position);
        rigidbody.MoveRotation(parent.rotation);
    }

    void OnCollisionEnter(Collision collision)
    {
        Haptics.Execute(0, 0, 50, 1, handType);

        Debug.Log("Impact relative velocity: " + collision.relativeVelocity.magnitude);
    }
}
