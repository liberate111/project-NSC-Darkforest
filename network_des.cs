using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class network_des : NetworkBehaviour {
    private bool start_des;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
   
    void Update () {
        if (start_des)
        {
            
        }
	}
    
    public void des()
    {
        start_des = true;
    }
    
   


}
