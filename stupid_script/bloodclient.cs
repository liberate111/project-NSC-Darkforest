using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class bloodclient :NetworkBehaviour  {
    [SerializeField]
    GameObject bloodspat;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void buildblood(Vector3 pos)
    {
        Cmdblood(pos);
    }
    [Command]
    void Cmdblood(Vector3 pos)
    {
        Rpcbloodbuild(pos);
    }
    [ClientRpc]
    void Rpcbloodbuild(Vector3 x)
    {
        Instantiate(bloodspat,x, Quaternion.identity);
    }
}
