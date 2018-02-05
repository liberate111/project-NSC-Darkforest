using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class sync_playername : NetworkBehaviour {
    
    public string name_;
	// Use this for initialization
	void Start () {

        if (!isLocalPlayer)
        {
            this.enabled = false;
        }
        Invoke("disenable", 2);
	}
	
	// Update is called once per frame
	void Update () {
        Cmdname();
	}
    [Command]
    void Cmdname()
    {
        Rpcname();
    }
    [ClientRpc]
    void Rpcname()
    {
        name_ = GetComponent<hp_hud>().playerName;
    }
    void disenable()
    {
        this.enabled = false;
    }
}
