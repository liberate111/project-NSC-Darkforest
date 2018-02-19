using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class bossspwan : NetworkBehaviour {
    private GameObject player;
    public GameObject boss;
    private bool sp;
    public Transform[] bosspos;
    private bool sawaddeekub;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (sawaddeekub == false)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                Cmdsp(0);
            }

        }
     
        if (player == null)
        {
           player= GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
          
            if (Vector3.Distance(player.transform.position, transform.position) < 65)
            {
               if(sp==false)
                Cmdsp(1);
            }
        }
	}
    [Command]
    void Cmdsp(int a)
    {
        if (a == 0)
        {
            int number = Random.Range(0, 2);
            Rpcpos(number);
            
        }
        else
        {
            GameObject x = Instantiate(boss, transform.position, Quaternion.identity) as GameObject;
            // x.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
            // NetworkServer.SpawnWithClientAuthority(x,player );
            sp = true;
            NetworkServer.Spawn(x);
        }
        
      

    }
    [ClientRpc]
    void Rpcpos(int lol)
    {
        this.transform.position = bosspos[lol].position;
        sawaddeekub = true;
    }
}
