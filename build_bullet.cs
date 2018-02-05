using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class build_bullet : NetworkBehaviour {
    [SerializeField]
    Transform[] bullet_Spwan;
    [SerializeField]
   gun gun_script;
    [SerializeField]
    axe axe_script;
    public GameObject bullet;
    private int bullet_type;
	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            
        
            this.enabled = false;
            //  axe_script.enabled = false;

        }
	}
	
	// Update is called once per frame
	void Update () {
        
        if(GetComponentInChildren<rifle>().enabled == true)
        {
            bullet_type = 0;
        }
        else if(GetComponentInChildren<gun>().enabled == true)
        {
            bullet_type = 1;
        }
		
	}
    public void build()
    {
        CmdSpawnshell();
    }
    [Command]
    void CmdSpawnshell()
    {
        Rpcbullet();
    
        
       // buller.GetComponent<Rigidbody>().AddForce(transform.forward * 2500);
    }
    [ClientRpc]
    void Rpcbullet()
    {
        Rigidbody shot = Instantiate(bullet.GetComponent<Rigidbody>(), bullet_Spwan[bullet_type].position, Quaternion.identity) as Rigidbody;
        shot.velocity = bullet_Spwan[bullet_type].forward * 200f;
    }
}
