using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Header("Posing")]
    public Transform hand;
    public Finger thumb;
    public List<Finger> fingers;    //order is important (index, middle,...)

    public void Operate(float input)
    {
        foreach (Finger f in fingers)
        {
            f.OpenClose(input);
        }
        if (input > 0 || (input < 0 && fingers[1].IsTouching()))
        {
            thumb.OpenClose(input);
        }

    }

    public void Operate(float thumb, float index, float grip)
    {
        fingers[0].OpenClose(index);
        fingers[1].OpenClose(grip);
        fingers[2].OpenClose(grip);
        fingers[3].OpenClose(grip);

        if (thumb > 0 || (thumb < 0 && fingers[1].IsTouching()))
        {
            this.thumb.OpenClose(thumb);
        }
    }

    public void EnableColliders(bool enable)
    {
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = enable;
        }
        if (!enable)
        {
            foreach (Phalanx p in GetComponentsInChildren<Phalanx>())
            {
                p.Contact = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
