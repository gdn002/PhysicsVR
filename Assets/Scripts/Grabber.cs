using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

//Add this script as component to the controller/hand
public class Grabber : MonoBehaviour
{
    private GameObject collidingObject;         //stores reference to object near hand that can be grabbed
    private GameObject objectInHand;            //stores reference to grabbed object (in hand)
    private Rigidbody[] fingers;                //parts of the fingers (3 per finger)

    public SteamVR_Action_Boolean grabAction;   //Instantiated in unity to the input that should trigger the event (e.g. GrabGrip)
    public SteamVR_Input_Sources handType;      //Instantiated in unity to either Right or Left Hand, defines on which controller the button (e.g. GrabGrip) is pressed
    public Transform fingerTip;
    public Transform restPosition;
    
    private float grab=1f;
    public float speed = 5000f;


    private void Awake()
    {
        fingers = GetComponentsInChildren<Rigidbody>();
    }

    void Start()
    {
        //Add listeners to the input pressed and released to call the respective functions
        grabAction.AddOnStateDownListener(GrabObject, handType);
        grabAction.AddOnStateUpListener(ReleaseObject, handType);
        
    }

    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        //FingerMovement();
        
    }

    //private void OpenHand()
    //{
    //    Vector3 distance = fingerTip.position - restPosition.position;
    //    if (distance.magnitude > 0.02) {
    //        float angle = -grab * speed * Time.deltaTime;
    //        Quaternion rot = Quaternion.Euler(0f, 0f, angle);
    //        foreach (var finger in GetComponentsInChildren<Rigidbody>())
    //        {
    //            if (finger.tag == "Finger")
    //                finger.MoveRotation(finger.rotation * rot);
    //        }
    //    }
    //}

    private void FingerMovement()
    {
        //float angle = -grab * speed * Time.deltaTime;
        //Quaternion rot = Quaternion.Euler(0f, 0f, angle);

        //foreach (var finger in GetComponentsInChildren<Rigidbody>())
        //{
        //    if (finger.tag == "Finger" )
        //        finger.MoveRotation(finger.rotation * rot);
        //}
    }

    //sets the grabbed object as child of the controller
    private void GrabObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        grab = 1f;
        if (collidingObject)
        {
            objectInHand = collidingObject;
            objectInHand.transform.SetParent(this.transform);
            objectInHand.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    //unsets parent-child relationship
    private void ReleaseObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        grab = -1f;
        if (objectInHand)
        {
            objectInHand.GetComponent<Rigidbody>().isKinematic = false;
            objectInHand.transform.SetParent(null);
            objectInHand = null;

        }
    }

    //collisions catchers
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            collidingObject = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        collidingObject = null;
    }

}
