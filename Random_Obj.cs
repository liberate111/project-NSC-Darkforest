using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Random_Obj : NetworkBehaviour
{
    public GameObject[] obj;
    [SyncVar]
    public int random;
    public bool once;
    
   


	// Use this for initialization
	void Start () {
         
	}
	
	// Update is called once per frame
    
	void Update () {
       //if (GameObject.FindGameObjectsWithTag("Player").Length == 2 && once == false)
       if (once == false)
       {
          
            //once = true;
            
            random = Random.Range(0, 10);
            once = true;
            CmdSpawn(random);
        }    
        

		
	}
    [Command]
    void CmdSpawn(int random)
    {
     
        
        if (obj[random] == null)
        {

        }
        else
        {

            Rpcspawn(random);
            GameObject ga =  Instantiate(obj[random], transform.position, transform.rotation) as GameObject;
            NetworkServer.Spawn(ga);

        }
          

    }

    
        
    
    [ClientRpc]
    void Rpcspawn(int random)
    {
        //print("SPAWN");
        //print(r);
        //random = Random.Range(0, 10);
        if (obj[random] == null)
        {

        }
        else
        {
           
           
        }
            

    }
}
