using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //sets the camera's position and roation to the player's
        transform.position = new Vector3(SpatialBridge.actorService.localActor.avatar.position.x, SpatialBridge.actorService.localActor.avatar.position.y + 1.7f, SpatialBridge.actorService.localActor.avatar.position.z);
        transform.rotation = SpatialBridge.actorService.localActor.avatar.rotation;
        
    }
}
