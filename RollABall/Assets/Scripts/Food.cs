using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DateTime now = DateTime.Now;
        long timestamp = (long) (now.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        if (timestamp % 5 ==0)
        {
            transform.Rotate(Vector3.back);
        }
    }
}