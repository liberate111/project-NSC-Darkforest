using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class expert : NetworkBehaviour
{
    public level_game l;
    public Transform diff;
    public Transform diff1;
    public Transform diff2;
    public Transform bg;
    // Use this for initialization
    void Start()
    {
        if (!isServer)
        {
            NetworkServer.Destroy(diff.gameObject);
            NetworkServer.Destroy(diff1.gameObject);
            NetworkServer.Destroy(diff2.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void cc()
    {
        CmdLevel();
    }
    [Command]
    void CmdLevel()
    {
        RpcLevel();
    }
    [ClientRpc]
    void RpcLevel()
    {
        l.zom_hit = 20;
        if (diff != null && diff1 != null && diff2 != null)
        {
            NetworkServer.Destroy(diff.gameObject);
            NetworkServer.Destroy(diff1.gameObject);
            NetworkServer.Destroy(diff2.gameObject);
        }
        NetworkServer.Destroy(bg.gameObject);
    }
}
