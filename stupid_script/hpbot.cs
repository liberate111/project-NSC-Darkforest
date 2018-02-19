using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class hpbot : NetworkBehaviour {
    public UnityEngine.AI.NavMeshAgent nav;
    // Use this for initialization
    void Start () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void dead()
    {
        Cmddead();
    }
    [Command]
    void Cmddead()
    {
        Rpcdead();
    }
    [ClientRpc]
    void Rpcdead()
    {
        nav.speed = 0;
        GetComponent<Animator>().SetBool("dead", true);
    }
}
