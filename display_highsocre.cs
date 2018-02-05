using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class display_highsocre : MonoBehaviour {
    public  int Highscore_data;
    public Transform display_score;
    // Use this for initialization
    void Start () {
        Highscore_data = PlayerPrefs.GetInt("Highscore_data", 0);
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "YOUR SCORE" + score.highscore;
        if (score.highscore > Highscore_data)
        {
            Highscore_data = score.highscore;
            PlayerPrefs.SetInt("Highscore_data", Highscore_data);
            

        }
        display_score.GetComponent<Text>().text = "HIGH SCORE" + Highscore_data;
    }
}
