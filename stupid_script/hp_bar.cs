﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_bar : MonoBehaviour {
    public GameObject[] hud;
    private int i = 0;
    private int startplayer = 0;
    private int tmp;
    public Transform[] playername;
    float ti;
    public GameObject[] xxxxx;
    string popprapas;
    bool kok;
    string[] name = { "NSC1", "NSC2", "NSC3", "NSC4" };

	// Use this for initialization
	void Start () {
        tmp = 0;
     
	}
	
	// Update is called once per frame
	void Update () {
      
 





        // print(xxxxx.GetComponent<hp_hud>().playerName);
        if (GameObject.FindGameObjectsWithTag("Player").Length != startplayer)
        {
            
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player"))
            {
              
                if (item.GetComponent<red_barcal>().red_bar == null)
                {
                    item.GetComponent<red_barcal>().red_bar = hud[i];
                    hud[i].transform.tag = "used_redbar";
                    playername[i].GetComponent<Text>().text = name[i];
                    item.GetComponent<hp_hud>().playerName = name[i];
                    //print(item.GetComponent<hp_hud>().playerName);
                 
                    i++;
                }
            }
            startplayer++;
      
        }

     

        ti = ti + Time.deltaTime;
        if (ti > 35)
        {
            foreach (GameObject item in hud)
            {
                if (item.transform.tag != "used_redbar")
                {
                    GameObject a = item.transform.parent.gameObject;
                    Destroy(a);
                }
            }
            ti = 0;
            
            this.enabled = false;
        }
    }
}
