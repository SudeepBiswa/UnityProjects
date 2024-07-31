using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpatialBridge.cameraService.forceFirstPerson = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }
}
