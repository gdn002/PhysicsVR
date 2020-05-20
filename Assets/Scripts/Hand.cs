﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Finger> fingers;    //order is important (index, middle,...)

    public void Operate(float input)
    {
        foreach (Finger f in fingers)
        {
            f.OpenClose(input);
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
