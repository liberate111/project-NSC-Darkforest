using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shutlight : MonoBehaviour {
    private Light li;
    public int turnofftime;
	// Use this for initialization
	void Start () {
        li = GetComponent<Light>();
        Invoke("turnoff", turnofftime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void turnoff()
    {
        li.enabled = false;
    }
}
