using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class playermultiplayer : NetworkBehaviour {
    public Camera cam1;
    public Camera cam2;
    public GameObject gui_follow;
    public GameObject player_model;
    public GameObject gun_model;
    public GameObject gun_pistol;
    // Use this for initialization
    void Start () {
        if (!isLocalPlayer)
        {
            gun_model.SetActive(false);
            gun_pistol.SetActive(false);
            GetComponentInChildren<animation_player>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
            GetComponent<FirstPersonController>().enabled = false;
            cam1.enabled = false;
            cam2.enabled = false;
            GameObject C = Instantiate(gui_follow,new Vector3(transform.position.x,transform.position.y+1,transform.position.z),Quaternion.identity);
            C.transform.parent = this.transform;
        }
        else
        {
            //GetComponent<NetworkAnimator>().enabled = false;
            // player_model.layer = LayerMask.NameToLayer("playermodel");
            //  NetworkServer.Destroy(player_model);
            Invoke("des", 1);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void des()
    {
            player_model.SetActive(false);
    }
}
