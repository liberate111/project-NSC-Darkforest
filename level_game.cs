using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class level_game : NetworkBehaviour {
    [SyncVar]
    public int hit_tmp;
    public int zom_hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(zom_hit > 0)
        {

        }
	}
    [Command]
    void Cmdlevel(int hit)
    {
        hit_tmp = hit;
        Rpclevel(hit_tmp);
    }
    [ClientRpc]
    void Rpclevel(int hittmp)
    {
        zom_hit = hittmp;
    }
}
