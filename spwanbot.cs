using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class spwanbot : NetworkBehaviour {
    public GameObject bot;
    private GameObject player;
    public bool lo;
    private float xti;
    [SerializeField]
    private int time_spw_bot;
    public bool force_LO;
	// Use this for initialization
	void Start () {
        //InvokeRepeating("botbot",3,10);
        // player = GameObject.FindGameObjectWithTag("Player");
        if (force_LO)
        {
            Invoke("IOawake", 4);
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (lo == true)
        {
            xti = xti + Time.deltaTime;
            if (xti > time_spw_bot)
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
    void IOawake()
    {
        lo = true;
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
