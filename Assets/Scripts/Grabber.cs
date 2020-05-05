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

    public SteamVR_Action_Boolean grabAction;   //Instantiated in unity to the input that should trigger the event (e.g. GrabGrip)
    public SteamVR_Input_Sources handType;      //Instantiated in unity to either Right or Left Hand, defines on which controller the button (e.g. GrabGrip) is pressed 

    void Start()
    {
        //Add listeners to the input pressed and released to call the respective functions
        grabAction.AddOnStateDownListener(GrabObject, handType);
        grabAction.AddOnStateUpListener(ReleaseObject, handType);

    }
    
    //sets the grabbed object as child of the controller
    private void GrabObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
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
