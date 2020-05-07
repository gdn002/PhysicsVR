using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VirtualHandBehaviour : MonoBehaviour
{
    [Header("VR Links")]
    public Transform controller;
    public SteamVR_Action_Vibration Haptics;
    public SteamVR_Input_Sources handType;

    [Header("Physics")]
    public float forceMultiplier = 50;
    public float maximumForce = 100;

    new Rigidbody rigidbody;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        // Instantly match the controller's position on startup
        transform.position = controller.position;
        transform.rotation = controller.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        ApplyForce();
        rigidbody.MoveRotation(controller.rotation);
    }

    private void ApplyForce()
    {
        // TODO: Consider implementing PID

        // Get direction vector that points from the virtual hand to the controller's position
        Vector3 forceDirection = controller.position - transform.position;

        // Multiply direction to amplify the forces applied to the hand
        forceDirection *= forceMultiplier;

        // Cap the maximum force that can be applied to the virtual hand
        if (forceDirection.magnitude > maximumForce)
        {
            forceDirection = forceDirection.normalized * maximumForce;
        }

        // Reset previous velocity, otherwise forces will compound with each call of FixedUpdate
        rigidbody.velocity = Vector3.zero;

        // Finally, apply force to hand
        rigidbody.AddForce(forceDirection, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        Haptics.Execute(0, 0, 50, 1, handType);
    }
}
