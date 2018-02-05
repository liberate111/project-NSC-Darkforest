using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class bulletui : MonoBehaviour {
    [SerializeField]
     gun gun;
    private Text te;

    public int canuse;
    public int max;
	// Use this for initialization
	void Start () {
        gun = null;
        te = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
       
    
            te.text = "" + canuse + "/" + max;
        
      
	}
}
