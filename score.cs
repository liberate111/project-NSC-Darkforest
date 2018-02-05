using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class score : NetworkBehaviour {
    [SyncVar]
    public int scr;
    private Text t;
    public static int highscore;
    
    // Use this for initialization
    void Start() {
        scr = 0;
        t = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update() {
       // Cmdscr();
        t.text = "" + scr;
        highscore = scr;
        
    }
    public void plusscore(int a)
    {
        Cmdscr(a);
    }
    [Command]
    void Cmdscr(int a)
    {
        Rpcjoke(a);
 
    }
    [ClientRpc]
    void Rpcjoke(int a)
    {
        scr = scr + a;
    }
}
