using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartScan()
    {
        CoreServices.SpatialAwarenessSystem.ResumeObservers();
    }

    public void StopScan()
    {
        CoreServices.SpatialAwarenessSystem.SuspendObservers();
    }
}
