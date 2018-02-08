using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class hp_update : NetworkBehaviour {
    [SyncVar]
    float a;

    public float x;
	// Use this for initialization
	void Start () {
        
       
	}

    // Update is called once per frame
    void Update() {
        
        CmdSynchp();

    }
    [Command]
    void CmdSynchp()
    {
        //x = GetComponent<hp_hud>().i;
        RpcHp(x);
    }
    [ClientRpc]
    void RpcHp(float x)
    {
        //GetComponent<hp_hud>().i = x;
    }
}
