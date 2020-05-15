using UnityEngine;
using System.Collections;

public class HandGun : Grabbable
{
    public Transform barrelDirection;
    public float maximumRange = 100;
    public float impactForce = 100;

    public Transform casingExit;
    public GameObject casingPrefab;

    public GameObject muzzleFlashPrefab;

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
        rigidbody.AddForceAtPosition(barrelDirection.forward * -20, barrelDirection.position, ForceMode.Impulse);

        // Drop casing
        if (casingPrefab)
        {
            GameObject casing = Instantiate(casingPrefab, casingExit.position, barrelDirection.rotation);
            casing.GetComponent<Rigidbody>().AddForce((barrelDirection.right + barrelDirection.up) * 100);
        }

        // Display muzzle flash
        if (muzzleFlashPrefab)
        {
            Instantiate(muzzleFlashPrefab, barrelDirection.position, barrelDirection.rotation);
        }
    }
}
