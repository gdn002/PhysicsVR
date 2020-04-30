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

    private void ApplyTorque()
    {
        // TODO: The current torque implementation is not working very well
        // We could review it, or just keep using MoveRotation for now

        // Determine Quaternion 'difference'
        // The conversion to euler demands we check each axis
        Vector3 torqueF = Geometry.OrientTorque(Quaternion.FromToRotation(transform.forward, controller.forward).eulerAngles);
        Vector3 torqueR = Geometry.OrientTorque(Quaternion.FromToRotation(transform.right, controller.transform.right).eulerAngles);
        Vector3 torqueU = Geometry.OrientTorque(Quaternion.FromToRotation(transform.up, controller.transform.up).eulerAngles);

        float magF = torqueF.magnitude;
        float magR = torqueR.magnitude;
        float magU = torqueU.magnitude;

        // Here we pick the axis with the least amount of rotation to use as our torque.
        Vector3 torque = magF < magR ? (magF < magU ? torqueF : torqueU) : (magR < magU ? torqueR : torqueU);

        // Multiply direction to amplify the forces applied to the hand
        //torque *= forceMultiplier;

        // Cap the maximum force that can be applied to the virtual hand
        if (torque.magnitude > maximumForce)
        {
            torque = torque.normalized * maximumForce;
        }

        // Reset previous velocity, otherwise forces will compound with each call of FixedUpdate
        rigidbody.angularVelocity = Vector3.zero;

        // Finally, apply force to hand
        rigidbody.AddTorque(torqueU * forceMultiplier);
    }

    void OnCollisionEnter(Collision collision)
    {
        Haptics.Execute(0, 0, 50, 1, handType);
    }
}
