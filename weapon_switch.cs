using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class weapon_switch : NetworkBehaviour {
    public int W_lock;
    private bool ready_toswitch;
    public GameObject gun_pistol;
    public GameObject gun_rifle;
    public Animator ani;
    GameObject tt;
    private bool wait_change;
   
	// Use this for initialization
	void Start () {
        gun_pistol.GetComponent<Animator>().SetBool("away", true);
        tt = GameObject.Find("medic_count");
        if (!isLocalPlayer)
        {
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!wait_change)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2)){
             if (gun_rifle.GetComponent<rifle>().reloading == false && gun_rifle.GetComponent<rifle>().shooting == false)
                {
                  
                gun_pistol.GetComponent<Animator>().SetBool("away", false);
                gun_rifle.GetComponent<Animator>().SetBool("away", true);
                StartCoroutine(wait_changing());
                Invoke("enable_pistol", 0.7f);
                gun_rifle.GetComponent<rifle>().enabled = false;
                
                
                }
            
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                
                gun_pistol.GetComponent<Animator>().SetBool("away", true);
                gun_rifle.GetComponent<Animator>().SetBool("away", false);
                StartCoroutine(wait_changing());
                Invoke("enable_rifle", 0.8f);
                gun_pistol.GetComponent<gun>().enabled = false;
                
              


            }
        }
        if (isLocalPlayer)
        {
            if (gun_rifle.GetComponent<rifle>().enabled == true)
            {
                tt.GetComponent<Text>().text = "" + gun_rifle.GetComponent<rifle>().ammo + "/" + "" + gun_rifle.GetComponent<rifle>().all_ammo;
            }
            else if (gun_pistol.GetComponent<gun>().enabled == true)
            {
                tt.GetComponent<Text>().text = "" + gun_pistol.GetComponent<gun>().o + "/" + "" + gun_pistol.GetComponent<gun>().allammo;
            }
        }

    }
    IEnumerator wait_changing()
    {
        wait_change = true;
        yield return new WaitForSeconds(1f);
        wait_change = false;
        
    }
    void enable_pistol()
    {
        gun_pistol.GetComponent<gun>().enabled = true;
    }
    void enable_rifle()
    {
        gun_rifle.GetComponent<rifle>().enabled = true;
    }
}
