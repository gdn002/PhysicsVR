using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //private List<List<GameObject>> fingers;
    public List<Finger> fingers;    //order is important (finger1, finger2, finger3)


    public float speed = 5000f;
    private bool touch = false;

    //private void FingerMovement(float clockwise, Transform target)
    //{
    //    float angle = clockwise * speed * Time.deltaTime;
    //    Rigidbody rb;
    //    Quaternion rot = Quaternion.Euler(0f, 0f, angle);

    //    foreach (var finger in fingers)
    //    {
    //        foreach (var bone in finger)
    //        {
    //            rb = bone.GetComponent<Rigidbody>();
    //            if (rb != null)
    //            {
    //                rb.MoveRotation(rb.rotation * rot);
    //            }
    //            else //finger_end
    //                if (bone.transform.position.y - target.position.x <= 0.02f)
    //            {

    //            }
    //        }
    //    }
    //}

    //public void Close(Transform target)
    //{
    //    this.FingerMovement(-1f, target);
    //}

    public void OpenClose(float direction)
    {

        //Debug.Log("Close Hand: " + touch);
        foreach (var finger in fingers)
        {
            //Debug.Log("Close Hand: " + touch);
            if (direction > 0 && !touch)
            {
                touch = finger.close(direction);
            }
            else if (direction < 0)
            {
                finger.open(direction);
                touch = false;
            }
        }
    }

}
