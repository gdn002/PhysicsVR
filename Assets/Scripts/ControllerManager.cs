using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerManager : MonoBehaviour
{
    [Header("VR Links")]
    public SteamVR_Action_Vibration Haptics;
    public SteamVR_Input_Sources handType;

    [Header("Objects")]
    public Rigidbody defaultHand;
    private Rigidbody activeHand;

    [Header("Physics")]
    public float forceMultiplier = 50;
    public float maximumForce = 100;


    public void SetActiveHand(Rigidbody rbody)
    {
        // Are we setting a new active hand or returning to the default hand?
        if (rbody == null)
        {
            // Instantly warp the default hand into position
            WarpDefaultHandToPosition();
        }
        // Activate/deactivate the default hand
        activeHand = rbody;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Instantly warp the default hand into position on startup
        WarpDefaultHandToPosition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        ApplyForce();
        MatchRotation();
    }

    // Instantly warps the default hand to the controller's position, bypassing physics
    private void WarpDefaultHandToPosition()
    {
        defaultHand.transform.position = transform.position;
        defaultHand.transform.rotation = transform.rotation;
    }

    // Returns the currently active hand rigidbody
    private Rigidbody CurrentRigidbody()
    {
        if (activeHand != null) return activeHand;
        return defaultHand;
    }

    // Moves the hand rigibody to match the controller's position, using forces
    private void ApplyForce()
    {
        Rigidbody currentRigidbody = CurrentRigidbody();

        // Get direction vector that points from the virtual hand to the controller's position
        Vector3 forceDirection = transform.position - currentRigidbody.transform.position;

        // Multiply direction to amplify the forces applied to the hand
        forceDirection *= forceMultiplier;

        // Cap the maximum forces that can be applied to the virtual hand
        if (forceDirection.magnitude > maximumForce)
        {
            forceDirection = forceDirection.normalized * maximumForce;
        }

        // Reset previous velocity, otherwise forces will compound with each call of FixedUpdate
        currentRigidbody.velocity = Vector3.zero;

        // Finally, apply force to hand
        currentRigidbody.AddForce(forceDirection, ForceMode.VelocityChange);
    }

    private void MatchRotation()
    {
        Rigidbody currentRigidbody = CurrentRigidbody();
        float mass = currentRigidbody.mass;
        float angleDelta = Quaternion.Angle(currentRigidbody.transform.rotation, transform.rotation);

        // The heavier the hand object, the slower the rate at which it tries to match the controller
        float rate = forceMultiplier / mass;
        if (angleDelta <= rate)
        {
            // If the angle difference is less than the turn rate, simply match the rotation
            currentRigidbody.MoveRotation(transform.rotation);
        }
        else
        {
            // Otherwise we interpolate between the two rotations according to the rate
            float t = rate / angleDelta;
            currentRigidbody.MoveRotation(Quaternion.Lerp(currentRigidbody.transform.rotation, transform.rotation, t));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Haptics.Execute(0, 0, 50, 1, handType);
    }
}
