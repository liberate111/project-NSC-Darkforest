using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class sang : NetworkBehaviour {
    [SerializeField]
     Light light_fire;
    public bool light_trigger;
    public Transform par;
    // Use this for initialization
    void Start () {
        light_fire.enabled = false;
        if (!isLocalPlayer)
        {
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //print("sang");
        /*if (Input.GetMouseButtonDown(0))
        {
            light_trigger = true;
        }*/

        if(light_trigger==true)
        {
            Cmdtrigger(0);
            StartCoroutine(sangdelay());
            light_trigger = false;
        }
	}
    private IEnumerator sangdelay()
    {
        yield return new WaitForSeconds(0.08f);
        // print("WaitAndPrint " + Time.time);
        Cmdtrigger(1);
    }
    [Command]
    void Cmdtrigger(int  x)
    {
        Rpcstartlight(x);

    }
    [ClientRpc]
    void Rpcstartlight(int x)
    {
        
        if (x == 0)
        {
            par.GetComponent<ParticleSystem>().Play();
            // print("turn on");
            light_fire.enabled = true;
        }
        else
        {
            light_fire.enabled = false;
        }
      
    }
 
}
