using UnityEngine;
using System.Collections;

public class Grabbable : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Override to implement different grab points
    public virtual Vector3 GrabPoint()
    {
        return transform.position;
    }

    // Override this method to include actions that happen when the object is grabbed
    public virtual void GrabAction(Transform grabber)
    {

    }

    // Override this method to include actions that fire on a trigger
    public virtual void TriggerAction()
    {

    }
}
