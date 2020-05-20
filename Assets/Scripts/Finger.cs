using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger: MonoBehaviour
{
    //private List<List<GameObject>> fingers;
    public List<Phalanx> phalanges;    //order is important (finger1, finger2, finger3)

    public float speed = 5f;
    private bool touch = false;

    public void OpenClose(float direction)
    {

        //Debug.Log("Close Hand: " + touch);
        foreach (var phalanx in phalanges)
        {
            //Debug.Log("Close Hand: " + touch);
            if (direction > 0 && !touch)
            {
                touch = phalanx.close(direction*speed);
            }
            else if (direction < 0)
            {
                phalanx.open(direction*speed);
                touch = false;
            }
        }
    }

}
