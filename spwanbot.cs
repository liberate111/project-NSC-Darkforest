using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class spwanbot : NetworkBehaviour {
    public GameObject bot;
    private GameObject player;
    public bool lo;
    private float xti;
	// Use this for initialization
	void Start () {
        //InvokeRepeating("botbot",3,10);
       // player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
        if (lo == true)
        {
            xti = xti + Time.deltaTime;
            if (xti > 10)
            {
                botbot();
                xti = 0;
            }
        }
        //print(Input.GetAxis("Fire1"));
    }
    void botbot()
    {
        //Instantiate(bot, transform.position, Quaternion.identity) ;
        Cmdsp();

    }
    [Command]
    void Cmdsp()
    {
        GameObject x= Instantiate(bot, transform.position, Quaternion.identity) as GameObject;
        // x.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        // NetworkServer.SpawnWithClientAuthority(x,player );
       
        NetworkServer.Spawn(x);

    }
}
