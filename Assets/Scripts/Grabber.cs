using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grabber : MonoBehaviour
{
   // public GameObject CollidingObject;
   // public GameObject objectInHand;

    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Input_Sources handType;
    public GameObject Sphere;

    // Start is called before the first frame update
    void Start()
    {
        grabAction.AddOnStateDownListener(GrabObject, handType);
        grabAction.AddOnStateUpListener(ReleaseObject, handType);

    }

    private void ReleaseObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up");
        Sphere.GetComponent<MeshRenderer>().enabled = false;
    }

    private void GrabObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        Sphere.GetComponent<MeshRenderer>().enabled = true;
    }

    void Update()
    {
        //if(Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger") > 0.2f && CollidingObject)
        {
            //GrabObject();
        }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponent<Rigidbody>())
    //    {
    //        CollidingObject = other.gameObject;
    //    }
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    CollidingObject = null;
    //}

}
