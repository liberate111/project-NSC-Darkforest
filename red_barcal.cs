using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class red_barcal : NetworkBehaviour {
    public GameObject red_bar;
    public float a;
    public float x;
    public float x_tmp;
    // Use this for initialization
    void Start () {
        x= GetComponent<hp_hud>().i;
        x_tmp = x;
	}
	
	// Update is called once per frame
	void Update () {
        x = GetComponent<hp_hud>().i;
        if (x != x_tmp)
        {
            Cmd_Redbar();
            x_tmp = x;
        }
      
    }
    [Command]
    void Cmd_Redbar()
    {
        a = GetComponent<hp_hud>().i;
        if (a < 0)
        {
            a = 0;
        }
        Rpc_Redbar(a);
    }
    [ClientRpc]
    void Rpc_Redbar(float hp)

    {
        red_bar.transform.localScale = new Vector3(hp / 100, red_bar.transform.localScale.y, red_bar.transform.localScale.z);
    }
}
