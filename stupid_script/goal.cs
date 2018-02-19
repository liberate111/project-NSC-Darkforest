using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	} 
    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            // print("goal");
            col.transform.gameObject.GetComponent<build_bullet>().enabled = false;
            SceneManager.LoadScene("win", LoadSceneMode.Additive);
        }
    }
}
