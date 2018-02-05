using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class raytocheck : NetworkBehaviour {
    RaycastHit ray;
    GameObject img_circle;
    private bool holdto_revive;
    private float ti;
    public GameObject player_x;
    [SyncVar]
    GameObject ppp;
	// Use this for initialization
	void Start () {
        img_circle = GameObject.Find("LoadingRe");
        if (!isLocalPlayer)
        {
            this.enabled = false; 
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.tag == "Player")
        {
            if (holdto_revive && GetComponent<lovelive>().getholdtoheal() == false)
            {
                //print("outside");

                //GetComponentInChildren<gun>().enabled = false;
                GetComponent<FirstPersonController>().enabled = false;
                img_circle.SetActive(true);
                ti = ti + Time.deltaTime;
                img_circle.GetComponent<Image>().fillAmount = ti / 7;
                img_circle.GetComponentInChildren<Text>().text = "" + Mathf.RoundToInt(ti);
                if (ti > 7)
                {
                    //print("swasaw");
                    ti = 0;
                    Cmdreviesync(player_x);
                    holdto_revive = false;
                }
            }
            
            else
            {
                if (GetComponent<lovelive>().getholdtoheal() == false)
                {
                  //  GetComponentInChildren<gun>().enabled = true;
                    GetComponent<FirstPersonController>().enabled = true;
                    img_circle.SetActive(false);
                }


            }

            if (Physics.Raycast(transform.position, transform.forward, out ray))
            {
                if (ray.transform.tag == "deadplayer")
                {
                    player_x = ray.transform.gameObject;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        holdto_revive = true;


                    }
                    else if (Input.GetKeyUp(KeyCode.E))
                    {
                        holdto_revive = false;
                    }
                }
                else if (ray.transform.tag == "Player")
                {
                    //print("foundplayer");
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            holdto_revive = false;
            ti = 0;
        }

    }
    [Command]
   void Cmdreviesync(GameObject P)
    {
   
        Rpcrevieready(P);
    }
    [ClientRpc]
    void Rpcrevieready(GameObject P)
    {
     
      P.GetComponent<hp_hud>().revive();
        //print("s;ss;s;");
    }
   
}
