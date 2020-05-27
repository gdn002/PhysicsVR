using UnityEngine;
using Valve.VR;
using System.Collections.Generic;

public class VirtualHandManager : Hand
{
    private struct Grabbable
    {
        public Rigidbody rbody;
        public global::Grabbable obj;

        public void Clear()
        {
            rbody = null;
            obj = null;
        }
    }

    [Header("VR Links")]
    public ControllerManager assignedController;
    
    public SteamVR_Action_Boolean gripAction;   // Middle, ring and pinky fingers; grab and release
    public SteamVR_Action_Boolean indexAction;  // Index finger, trigger press and pinching
    public SteamVR_Action_Boolean thumbAction;  // Thumb
    public SteamVR_Input_Sources handType;

    //public GameObject model;

    private List<Grabbable> grabArray;
    private Grabbable currentGrab;
    public bool IsGrabbing { get { return currentGrab.rbody != null; } }

    private float thumbInput = 1;
    private float indexInput = 1;
    private float gripInput = 1;

    void Start()
    {
        grabArray = new List<Grabbable>();

        //Add listeners to the input pressed and released to call the respective functions
        gripAction.AddOnStateDownListener(GripDown, handType);
        gripAction.AddOnStateUpListener(GripUp, handType);
        indexAction.AddOnStateDownListener(IndexDown, handType);
        indexAction.AddOnStateUpListener(IndexUp, handType);
        thumbAction.AddOnStateDownListener(ThumbDown, handType);
        thumbAction.AddOnStateUpListener(ThumbUp, handType);
    }

    private void FixedUpdate()
    {
        Operate(thumbInput, indexInput, gripInput);
    }

    void Update()
    {
        // For debugging purposes only
        // Do note that when using the WMR emulator, you might have to switch focus to the editor window in order for this key press to register
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrabbing) GripUp(gripAction, handType);
            else GripDown(gripAction, handType);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            IndexDown(indexAction, handType);
        }
    }

    private void GripDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        gripInput = -1f;

        int i = FindGrabbable();
        if (i < 0) return;

        Grabbable gb = grabArray[i];
        if (gb.obj != null) // Is the object an useable tool?
        {
            gb.obj.GrabAction(transform);
            assignedController.GrabObject(gb.obj);

            currentGrab.rbody = gb.rbody;
            currentGrab.obj = gb.obj;

            // Clear the grab array at this point
            grabArray.Clear();

            // Hide the hand model and colliders
            //model.SetActive(false);
            
        }
    }

    private void GripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        gripInput = 1f;

        if (IsGrabbing)
        {
            assignedController.GrabObject(null);
            currentGrab.Clear();
            //model.SetActive(true);
        }
    }

    private void IndexDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        indexInput = -1f;

        if (currentGrab.obj)
        {
            currentGrab.obj.TriggerAction();
        }
    }

    private void IndexUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        indexInput = 1f;
    }

    private void ThumbDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        thumbInput = -1f;
    }

    private void ThumbUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        thumbInput = 1f;
    }

    private int FindGrabbable()
    {
        // No objects in grab array, return -1
        if (grabArray.Count == 0)
            return -1;

        // Otherwise, prioritize latest grabbable
        return grabArray.Count - 1;
    }

    // Adds a rigidbody to the grab array (if it hasn't been already)
    private void AddGrabbable(GameObject obj)
    {
        Grabbable gb;
        gb.rbody = obj.GetComponentInParent<Rigidbody>(); // Some objects have their colliders in their children, so we must search the parents as well
        gb.obj = obj.GetComponentInParent<global::Grabbable>();  // Ditto

        if (gb.obj != null) // Must be a grabbable
        {
            // Only add the rigidbody if it isn't already in the array
            if (FindGrabbable(gb.rbody) < 0)
            {
                Debug.Log("Added");
                grabArray.Add(gb);
            }
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
        Debug.Log("Gotcha");
    }

    public void OnTriggerExit(Collider other)
    {
        RemoveGrabbable(other.gameObject);
    }
}
