using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeitself : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("des", 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void des()
    {
        Destroy(this.gameObject);
    }
}
