using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class removebullet : NetworkBehaviour{
    public float t=0;
    public GameObject blood;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
    
	void Update () {
        t = t + Time.deltaTime;
        if (t >2.5f)
        {
            NetworkServer.Destroy(gameObject);
        }
	}
    void OnCollisionEnter(Collision col)
    {
        NetworkServer.Destroy(gameObject);
        if(col.gameObject.tag=="Player"|| col.gameObject.tag == "deadplayer")
        {
            Instantiate(blood, col.transform.position, Quaternion.identity);
        }
    }
}
