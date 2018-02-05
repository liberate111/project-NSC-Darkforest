using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class rotation_sync : NetworkBehaviour {
    [SyncVar]
    private Quaternion syncplayerRotation;
    [SyncVar]
    private Quaternion synccamRotation;

    [SerializeField]
    private Transform playertrans;
    [SerializeField]
    private Transform camtrans;
    [SerializeField]
    private float lerpRate = 15;

    float x;
 	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       
            TransmitRotation();
            lerpRotation();

       
	}
    void lerpRotation()
    {
        if (!isLocalPlayer)
        {
            playertrans.rotation = Quaternion.Lerp(playertrans.rotation,syncplayerRotation, Time.deltaTime * lerpRate);
            camtrans.rotation = Quaternion.Lerp(playertrans.rotation, synccamRotation, Time.deltaTime * lerpRate);

        }
    }
    [Command]
    void CmdprovideRotation(Quaternion playerRot,Quaternion camRot)
    {
        SetDirtyBit(1u);
        syncplayerRotation = playerRot;
        synccamRotation = camRot;
    }
    [Client]
    void TransmitRotation()

    {   if(isLocalPlayer)
        CmdprovideRotation(playertrans.rotation,camtrans.rotation);
    }
}
