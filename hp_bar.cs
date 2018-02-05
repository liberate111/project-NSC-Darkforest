using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_bar : MonoBehaviour {
    public GameObject[] hud;
    private int i = 0;
    private int startplayer = 0;
    private int tmp;
    public Transform[] playername;
	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectsWithTag("Player").Length != startplayer)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (item.GetComponent<red_barcal>().red_bar == null)
                {
                    item.GetComponent<red_barcal>().red_bar = hud[i];

                    playername[i].GetComponent<Text>().text = item.GetComponent<hp_hud>().playerName;
                    i++;
                }
            }
            startplayer++;
        }
      
    }
}
