using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class zom_horde :NetworkBehaviour {
    private bool trig;
    public GameObject zombbie;
    public GameObject spwpos;
    private int i;
    public bool end;
    public bool itsEnd;
    float ti;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (trig == true)
        {
            for( i=0; i<10; i++)
            {
                 Cmdbot();
            }
        
            trig = false;
            this.enabled = false;
        }
        if (itsEnd)
        {
            if (end == true)
            {
                ti = ti + Time.deltaTime;
                if (ti > 10)
                {
                    for (i = 0; i < 10; i++)
                    {
                        Cmdbot();
                    }
                    ti = 0;
                }
            }

        }
     
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            trig = true;
            end = true;
        }
    }
    [Command]
    void Cmdbot()
    {
        GameObject x = Instantiate(zombbie, spwpos.transform.position, Quaternion.identity);
        NetworkServer.Spawn(x);
        
    }
}
