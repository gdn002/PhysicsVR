using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger: MonoBehaviour
{
    //private List<List<GameObject>> fingers;
    public List<Phalanx> phalanges;    //order is important (digit1, digit2, digit3)

    public float speed = 5f;
    private bool touch = false;

    public void OpenClose(float direction)
    {

        //Debug.Log("Close Hand: " + touch);
        foreach (var phalanx in phalanges)
        {
            //Debug.Log("Close Hand: " + touch);
            if (direction < 0 && !touch)
            {
                touch = phalanx.Close(direction*speed);
            }
            else if (direction > 0)
            {
                phalanx.Open(direction*speed);
                touch = false;
            }
        }
    }

}
