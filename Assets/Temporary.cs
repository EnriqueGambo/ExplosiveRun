using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Temporary : MonoBehaviour
{
    // Start is called before the first frame update
    Stopwatch sw = new Stopwatch();
    void Start()
    {
        sw.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(sw.ElapsedMilliseconds > 1000) { Destroy(gameObject); }
    }
}
