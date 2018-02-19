using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class gun_pistol_sound : NetworkBehaviour {
    public AudioSource au;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void pistol_soundShoot()
    {
        Cmdshoot();
    }
    [Command]
    void Cmdshoot()
    {
        Rpcshoot();
    }
    [ClientRpc]
    void Rpcshoot()
    {
        au.Play();
    }
}
