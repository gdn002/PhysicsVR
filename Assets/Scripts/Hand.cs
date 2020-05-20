using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    //private List<List<GameObject>> fingers;
    public List<Finger> fingers;    //order is important (finger1, finger2, finger3)

    public float speed = 5f;
    private bool touch = false;

    public void OpenClose(float direction)
    {

        //Debug.Log("Close Hand: " + touch);
        foreach (var finger in fingers)
        {
            //Debug.Log("Close Hand: " + touch);
            if (direction > 0 && !touch)
            {
                touch = finger.close(direction*speed);
            }
            else if (direction < 0)
            {
                finger.open(direction*speed);
                touch = false;
            }
        }
    }

}
