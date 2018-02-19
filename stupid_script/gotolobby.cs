using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotolobby : MonoBehaviour {
    public GameObject x;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void nextsc()
    {
        Object.DontDestroyOnLoad(x);
        SceneManager.LoadScene("handmadelobby");
        
    }
}
