using UnityEngine;
using System.Collections;

public class HandGunTool : BasicTool
{
    public Transform barrelDirection;
    public float maximumRange = 100;
    public float impactForce = 100;

    private new Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void TriggerAction()
    {
        // Fire a shot from the gun's barrel
        RaycastHit hit;
        if (Physics.Raycast(barrelDirection.position, barrelDirection.forward, out hit, maximumRange))
        {
            // Find which rigidbody was hit
            Rigidbody rbody = hit.collider.attachedRigidbody;
            if (rbody)
            {
                // Apply impact force at hit location
                rbody.AddForceAtPosition(barrelDirection.forward * impactForce, hit.point, ForceMode.Impulse);
            }
        }
        // Apply recoil
        rigidbody.AddForceAtPosition(barrelDirection.up, barrelDirection.position, ForceMode.Impulse);
    }
}
