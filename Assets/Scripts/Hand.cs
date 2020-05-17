using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private List<List<GameObject>> fingers;
    public Finger fingers1, fingers2, fingers3;


    public float speed = 5000f;

    private void FingerMovement(float clockwise, Transform target)
    {
        float angle = clockwise * speed * Time.deltaTime;
        Rigidbody rb;
        Quaternion rot = Quaternion.Euler(0f, 0f, angle);

        foreach (var finger in fingers)
        {
            foreach (var bone in finger)
            {
                rb = bone.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.MoveRotation(rb.rotation * rot);
                }
                else //finger_end
                    if (bone.transform.position.y - target.position.x <= 0.02f) {

                }
            }
        }
    }

    public void Close(Transform target)
    {
        this.FingerMovement(-1f, target);
    }

    public void OpenClose(float direction)
    {
        if (direction > 0)
        {
            fingers1.close();
            fingers2.close();
            fingers3.close();
        }
        else if (direction < 0)
        {
            fingers1.open();
            fingers2.open();
            fingers3.open();
        }
    }

}
