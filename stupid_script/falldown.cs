using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class falldown : MonoBehaviour {

    public bool spw;
	// Use this for initialization
	void Start () {
        Invoke("trigger_spw", 30);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void trigger_spw()
    {
        spw = true;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Player" && spw)
        {
            other.GetComponent<hp_hud>().i = -10;
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<FirstPersonController>().enabled = false;
        }
    }
}
