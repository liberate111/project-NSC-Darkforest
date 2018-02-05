using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class network_spawn : NetworkBehaviour
{

    public GameObject[] obj;
    [SyncVar]
    public int random;
    public bool once;




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        if ( GameObject.FindGameObjectsWithTag("Player").Length==2&&once == false)
        {
            random = Random.Range(0, 3);
            //once = true;
            CmdSpawn(random);

            once = true;
        }



    }
    [Command]
    void CmdSpawn(int r1)
    {


        Rpcspawn(r1);
        if (r1 == 0)
        {

        }
        else
        {
            GameObject s= Instantiate(obj[r1], transform.position, transform.rotation) as GameObject;
            NetworkServer.Spawn(s);
        }
            
    }
    [ClientRpc]
    void Rpcspawn(int r)
    {
        print("SPAWN");
        print(r);
     

    }
}
