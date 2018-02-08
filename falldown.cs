using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class falldown : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<hp_hud>().i = -500;
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<FirstPersonController>().enabled = false;
        }
    }
}
