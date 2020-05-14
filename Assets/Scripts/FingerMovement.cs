using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerMovement : MonoBehaviour
{
    public void OnTriggerEnter(Collider finger)
    {
        finger.attachedRigidbody.isKinematic = true;
        Debug.Log("NO!");
    }
}
