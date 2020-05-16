using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grab : MonoBehaviour
{
    private float input;

    public Hand hand;
    public SteamVR_Action_Boolean grabAction;   //Instantiated in unity to the input that should trigger the event (e.g. GrabGrip)
    public SteamVR_Input_Sources handType;      //Instantiated in unity to either Right or Left Hand, defines on which controller the button (e.g. GrabGrip) is pressed

    // Start is called before the first frame update
    void Start()
    {
        //Add listeners to the input pressed and released to call the respective functions
        grabAction.AddOnStateDownListener(GrabObject, handType);
        grabAction.AddOnStateUpListener(ReleaseObject, handType);

    }

    // Update is called once per frame
    void Update()
    {
        //input = Input.GetAxis("Vertical");

        hand.OpenClose(input);
    }

    private void FixedUpdate()
    {
    }

    private void GrabObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        input = 1f;

    }

    //unsets parent-child relationship
    private void ReleaseObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        input = -1f;

    }
}
