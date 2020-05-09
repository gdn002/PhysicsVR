using UnityEngine;
using Valve.VR;
using System.Collections.Generic;

public class VirtualHandManager : MonoBehaviour
{
    private struct Grabbable
    {
        public Rigidbody rbody;
        public BasicTool tool;
    }

    [Header("VR Links")]
    public ControllerManager assignedController;

    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Input_Sources handType;

    private List<Grabbable> grabArray;
    private Rigidbody currentGrabbedRbody;
    public bool IsGrabbing { get { return currentGrabbedRbody != null; } }

    void Start()
    {
        grabArray = new List<Grabbable>();

        //Add listeners to the input pressed and released to call the respective functions
        grabAction.AddOnStateDownListener(GrabObject, handType);
        grabAction.AddOnStateUpListener(ReleaseObject, handType);

    }

    void Update()
    {
        // For debugging purposes only
        // Do note that when using the WMR emulator, you might have to switch focus to the editor window in order for this key press to register
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrabbing) ReleaseObject(grabAction, handType);
            else GrabObject(grabAction, handType);
        }
    }

    private void GrabObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        int i = FindGrabbable();
        if (i < 0) return;

        Grabbable gb = grabArray[i];
        if (gb.tool != null) // Is the object an useable tool?
        {
            assignedController.SetActiveHand(gb.rbody);
            currentGrabbedRbody = gb.rbody;

            // Clear the grab array at this point
            grabArray.Clear();
        }
    }

    private void ReleaseObject(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (IsGrabbing)
        {
            assignedController.SetActiveHand(null);
            currentGrabbedRbody = null;
        }
    }

    private int FindGrabbable()
    {
        // No objects in grab array, return -1
        if (grabArray.Count == 0)
            return -1;

        // Prioritize tools, return the first tool found
        for (int i = 0; i < grabArray.Count; i++)
        {
            if (grabArray[i].tool != null)
                return i;
        }

        // Otherwise return the first available object
        return 0;
    }

    // Adds a rigidbody to the grab array (if it hasn't been already)
    private void AddGrabbable(GameObject obj)
    {
        Grabbable gb;
        gb.rbody = obj.GetComponentInParent<Rigidbody>(); // Some objects have their colliders in their children, so we must search the parents as well
        gb.tool = obj.GetComponentInParent<BasicTool>();  // Ditto

        if (gb.rbody != null) // All grabbables MUST have a rigidbody
        {
            // Only add the rigidbody if it isn't already in the array
            if (FindGrabbable(gb.rbody) < 0)
                grabArray.Add(gb);
        }
    }

    // Searches for a specific rigidbody in the grab array
    private int FindGrabbable(Rigidbody rbody)
    {
        for (int i = 0; i < grabArray.Count; i++)
        {
            // Find the rigidbody in the grab array
            if (grabArray[i].rbody == rbody)
            {
                return i;
            }
        }

        // Return -1 if not found
        return -1;
    }

    // Removes a specific rigidbody from the grab array
    private void RemoveGrabbable(GameObject obj)
    {
        Rigidbody rbody = obj.GetComponentInParent<Rigidbody>();
        if (rbody != null) // Objets without rigidbodies can be discarded
        {
            int i = FindGrabbable(rbody);
            if (i >= 0)
            {
                grabArray.RemoveAt(i);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        AddGrabbable(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        RemoveGrabbable(other.gameObject);
    }
}
