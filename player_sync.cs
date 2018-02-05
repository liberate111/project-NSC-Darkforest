using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class player_sync : NetworkBehaviour {
    [SyncVar]
    private Vector3 Syncpos;
    [SerializeField]
    Transform mytranform;
    [SerializeField]
    float lerpRate = 15;
    float x;
	// Use this for initialization

	
	// Update is called once per frame
	void FixedUpdate () {
        
     
            TranmitPos();
            lerppos();
   
       
	}
    void lerppos()
    {
        if (!isLocalPlayer)
        {
            mytranform.position = Vector3.Lerp(mytranform.position, Syncpos, Time.
                deltaTime * lerpRate);
        }
    }
    [Command]
    void Cmdsync(Vector3 pos)
    {
        SetDirtyBit(1u);
        Syncpos = pos;
    }
    [ClientCallback]
    void TranmitPos()
    {
        if(isLocalPlayer)
        Cmdsync(mytranform.position);
    }
}
