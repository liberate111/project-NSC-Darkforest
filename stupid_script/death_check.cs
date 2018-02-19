using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class death_check :NetworkBehaviour {
    public Transform scr;
    public float time_score;
    private bool l;
    private float score_temp;
    private int ToInt;
    // Use this for initialization

    void Start () {
        time_score = 0;
 

    }
	
	// Update is called once per frame
	void Update () {
        
        time_score = time_score+Time.deltaTime;
        
    }

    public void findplayer()
    {
       if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Cmdscencedeath(0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            if (l == false)
            {
                scr = GameObject.Find("text_score").transform;
                scr.GetComponent<score>().plusscore(2500);
                score_temp = ((1 / time_score) )* 100000;
                ToInt = Mathf.RoundToInt(score_temp);
                ToInt += 2000;
                scr.GetComponent<score>().plusscore(ToInt);
                Invoke("LOL",2);
                l = true;
            }
           


        }
    }
    void LOL()
    {
        Cmdscencedeath(1);
    }
    [Command]
    void Cmdscencedeath(int a)
    {
        Rpcdeadth(a);
    }
    [ClientRpc]
    void Rpcdeadth(int x)
    {
        if(x==0)
            SceneManager.LoadScene("gameover");     // GAME OVER
        else
        {
            SceneManager.LoadScene("win",LoadSceneMode.Single);  // WIN
            
        }
    }
}
