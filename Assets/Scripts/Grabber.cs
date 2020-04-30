using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grabber : MonoBehaviour
{
    private GameObject CollidingObject;
    public GameObject objectInHand;

    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Input_Sources handType;

    // Start is called before the first frame update
    void Start()
    {
        grabAction.AddOnStateDownListener(GrabObject, handType);
        grabAction.AddOnStateUpListener(ReleaseObject, handType);

    }

    private void ReleaseObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (objectInHand)
        {
            objectInHand.GetComponent<Rigidbody>().isKinematic = false;
            objectInHand.transform.SetParent(null);
            objectInHand = null;

        }
    }

    private void GrabObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (CollidingObject)
        {
            objectInHand = CollidingObject;
            objectInHand.transform.SetParent(this.transform);
            objectInHand.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            CollidingObject = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        CollidingObject = null;
    }

}
