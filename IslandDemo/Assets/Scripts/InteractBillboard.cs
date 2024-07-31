using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;
using System;
using System.Threading.Tasks;

public class InteractBillboard : MonoBehaviour
{
    public GameObject cam;
    public GameObject board;
    public GameObject activator;
    public GameObject camLoc;
    private float walk;
    private float run;
    private float jump;

    // Start is called before the first frame update
    void Start()
    {
        //original walk and run speed and jump height
        walk = SpatialBridge.actorService.localActor.avatar.walkSpeed;
        run = SpatialBridge.actorService.localActor.avatar.runSpeed;
        jump = SpatialBridge.actorService.localActor.avatar.jumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
        //if teh activator is active then
        if (activator.activeSelf)
        {
            //call the interact function and set activator to inactive
            Interact();
            activator.SetActive(!activator.activeSelf);
            
        }
        else if(!cam.activeSelf)
        {
            //if the cam is inactive then set the player's walk and run speed and jump height to the original saved speed and jump height
            SpatialBridge.actorService.localActor.avatar.walkSpeed = walk;
            SpatialBridge.actorService.localActor.avatar.runSpeed = run;
            SpatialBridge.actorService.localActor.avatar.jumpHeight = jump;
        }
    }

    void Interact()
    {
        //get the pos of the camLoc object
        Vector3 pos = camLoc.transform.position;

        //set the active state of the virtual cam to opposite of the current state
        cam.SetActive(!cam.activeSelf);

        //set the player walk and run speed and jump height to 0 and set they're position to the camLoc object's position while maintaining the player's y axis.
        SpatialBridge.actorService.localActor.avatar.walkSpeed = 0;
        SpatialBridge.actorService.localActor.avatar.runSpeed = 0;
        SpatialBridge.actorService.localActor.avatar.jumpHeight = 0;
        SpatialBridge.actorService.localActor.avatar.position = new Vector3(pos.x, SpatialBridge.actorService.localActor.avatar.position.y, pos.z);

        //add a delay so the camera transiton is a little more smooth
        Task.Delay(500);

        //set the postiion of the virtual camera to the camLoc's object
        cam.transform.position = pos;

    }
}
