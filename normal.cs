using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class normal : NetworkBehaviour {
    public level_game l;
    public Transform diff;
    public Transform diff1;
    public Transform diff2;
    public Transform bg;

    //private bool check_click;

    // Use this for initialization
    void Start () {
        //l = GameObject.Find("Level").GetComponent<level_game>();
        if (!isServer)
        {
            NetworkServer.Destroy(diff.gameObject);
            NetworkServer.Destroy(diff1.gameObject);
            NetworkServer.Destroy(diff2.gameObject);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        //if(check_click)
		 //CmdLevel();
        
	}
    
    public void cc()
    {
        CmdLevel();
        //check_click = true;
    }
    [Command]
    void CmdLevel()
    {
        RpcLevel();
    }
    [ClientRpc]
    void RpcLevel()
    {
        //print("zombiehit" + hp_hud.zombie_hit);  // not print
        //hp_hud.zombie_hit = 1;  //change
        l.zom_hit = 1;
        if (diff != null && diff1 != null && diff2 != null)
        {
            NetworkServer.Destroy(diff.gameObject);
            NetworkServer.Destroy(diff1.gameObject);
            NetworkServer.Destroy(diff2.gameObject);
        }
        
        NetworkServer.Destroy(bg.gameObject);
    }
}
