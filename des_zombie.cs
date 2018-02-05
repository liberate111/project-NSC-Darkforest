using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class des_zombie : NetworkBehaviour {
    public bool desit;
    private basic_zombie bz;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (desit == true)
        {
            Cmdstartdes();
            desit = false;
        }
	}
    [Command]
    void Cmdstartdes()
    {
        Rpcdes();
    }
    [Client]
    void Rpcdes()
    {
      
        GetComponent<basic_zombie>().check_dy = true;
    }
}
